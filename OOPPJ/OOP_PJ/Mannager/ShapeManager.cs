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
        //List<Shape> shape Property
        public List<Shape> Shapes
        {
            get { return shape; }
        }
        public ShapeManager()
        {
            shape = new List<Shape>();
        }

        public void AddShape(Shape newShape)
        {
            newShape.CurListIndex = shape.Count;
            shape.Add(newShape);
        }

        public int GetListCount()
        {
            return shape.Count;
        }

        public void Undo(Shape cancelShape)
        {
            UndoInfo undoInfo = cancelShape.Undo().CloneEX();

            if (!undoInfo.Success)
            {
                shape.Remove(cancelShape);
            }
            else if (undoInfo.OneStepMovement)
            {
                Shape tmp = shape[undoInfo.PreIndex];
                shape[undoInfo.PreIndex] = shape[undoInfo.CurIndex];
                shape[undoInfo.CurIndex] = tmp;
            }

        }

        public Shape ChoicedShape(Infomation newInfomation)
        {
            Shape theShape = null;

            for (int i = shape.Count - 1; i >= 0; i--)
            {
                if (shape[i].IsMyRange(newInfomation.Point))
                {
                    theShape = shape[i];
                    return theShape;
                }
            }
            return theShape;
        }

        public void Draw(Graphics g)
        {
            foreach (Shape cs in shape)
            {
                if (!cs.IsDeleted)
                    cs.Draw(g);
            }
        }


        public bool MoveShapeBackOneStep(Shape movedShape)  // 도형 뒤로 보내기
        {
            for (int i = shape.Count - 1; i >= 0; i--)
            {
                if (movedShape == shape[i])
                {
                    if (i == 0)
                    {
                        return false;
                    }
                    else
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (!shape[j].IsDeleted)
                            {
                                shape[i].PreListIndex = i;
                                shape[i].CurListIndex = j;

                                shape[j].PreListIndex = j;
                                shape[j].CurListIndex = i;

                                Shape tmp = shape[i];
                                shape[i] = shape[j];
                                shape[j] = tmp;

                                return true;
                            }

                        }

                    }
                }
            }
            return false;
        }

        public bool MoveShapeFrontOneStep(Shape movedShape)  // 도형 뒤로 보내기
        {
            for (int i = 0; i < shape.Count; i++)
            {
                if (movedShape == shape[i])
                {
                    if (i == shape.Count - 1)
                    {
                        return false;
                    }
                    else
                    {
                        for (int j = i + 1; j <= shape.Count; j++)
                        {
                            if (!shape[j].IsDeleted)
                            {
                                shape[i].PreListIndex = i;
                                shape[i].CurListIndex = j;

                                shape[j].PreListIndex = j;
                                shape[j].CurListIndex = i;

                                Shape tmp = shape[i];
                                shape[i] = shape[j];
                                shape[j] = tmp;

                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void DeleteShape(Shape deleteshape)
        {
            deleteshape.IsDeleted = true;
        }

        public List<Shape> Magnification()
        {
            Rectangle magRect;

            foreach (Shape s in shape)
            {
                if (!s.IsDeleted)
                {

                    magRect = new Rectangle(s.MyRectangle.X, s.MyRectangle.Y, (int)(s.MyRectangle.Width * Shape.Magnification), (int)(s.MyRectangle.Height * Shape.Magnification));

                    s.MyRectangle = magRect;

                    s.Save();
                }
            }
            return shape;
        }
    }
}
