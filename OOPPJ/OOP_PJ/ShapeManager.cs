using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace OOP_PJ
{

    public class ShapeManager
    {
        List<Shape> shape;

        public ShapeManager()
        {
            shape = new List<Shape>();
        }

        public void AddShape(Shape newShape)
        {
            newShape.CurListIndex = GetListCount() - 1;
            shape.Add(newShape);
        }

        public int GetListCount()
        {
            return shape.Count;
        }

        public void Undo(Shape cancelShape)
        {
            // 여기서 인덱스 관련 처리 해줘야함..
            if (!cancelShape.Undo())
            {
                shape.Remove(cancelShape);
            }
        }

        public Shape ChoicedShape(Infomation newInfomation)
        {
            Shape theShape = null;

            for (int i = shape.Count-1; i >= 0; i--)
            {
                if(shape[i].IsMyRange(newInfomation.Point))
                {
                    theShape = shape[i];
                    return theShape;
                }
            }
            return theShape;
        }

        public void RemoveShape(int index) // 삭제 다시 구현해야함 선택후 삭제 기능으로
        {
            shape.RemoveAt(index);
        }

        public void Draw(Graphics g)
        {
            foreach (Shape cs in shape)
            {
                cs.Draw(g);
            }
        }

        public bool MoveShapeBackOneStep(Shape movedShape)  // 도형 뒤로 보내기
        {
            for (int i = 0; i < shape.Count; i++)
            {
                if (movedShape == shape[i])
                {
                    if (i == 0)
                        return false;

                    Shape tmp = shape[i];
                    shape[i] = shape[i - 1];
                    shape[i - 1] = tmp;
                    shape[i].CurListIndex--;
                    shape[i - 1].CurListIndex++;
                }
            }
            return true;
        }

        public bool MoveShapeFrontOneStep(Shape movedShape)  // 도형 뒤로 보내기
        {
            for (int i = 0; i < shape.Count; i++)
            {
                if (movedShape == shape[i])
                {
                    if (i == shape.Count - 1)
                        return false;

                    Shape tmp = shape[i];
                    shape[i] = shape[i + 1];
                    shape[i + 1] = tmp;
                    shape[i].CurListIndex++;
                    shape[i + 1].CurListIndex--;
                }
            }
            return true;
        }
    }
}
