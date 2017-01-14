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
            shape.Add(newShape);
        }

        public int GetListCount()
        {
            return shape.Count;
        }

        public void Undo(Shape cancelShape)
        {
            if (cancelShape.GetStackCount() == 0)
            {
                shape.Remove(cancelShape);
            }
            else
            {
                cancelShape.Undo();
            }
        }


        // Shape 형태로 저장해보고 되면 지우기
        //public void AddRectangle(Rectangle recParam)
        //{
        //    shape.Add(new CCircle(recParam));
        //}

        //public void ChageRectanglePostion(Rectangle recParam)
        //{
        //    shape[shape.Count - 1].SetRenctangle(recParam);
        //}
        //-----------------------------------------------


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
    }

}
