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

        public void AddRectangle(Rectangle recParam)
        {
            shape.Add(new CCircle(recParam));
        }

        public void ChageRectanglePostion(Rectangle recParam)
        {
            shape[shape.Count - 1].SetRenctangle(recParam);
        }

        public void RemoveShape(int index)
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

    public abstract class Shape
    {
        abstract public void Draw(Graphics g);
        abstract public Rectangle GetRenctangle();
        abstract public void SetRenctangle(Rectangle recParam);

    }

    public class CCircle : Shape
    {
        private Rectangle mRec;
        private Pen mPen;

        public CCircle(Rectangle recParam)
        {
            mRec = new Rectangle();
            mRec = recParam;
        }

        public override void Draw(Graphics g)
        {
            mPen = new Pen(Color.DarkBlue, 1);
            g.DrawEllipse(mPen, mRec);
        }

        public override Rectangle GetRenctangle()
        {
            return mRec;
        }

        public override void SetRenctangle(Rectangle recParam)
        {
            mRec = recParam;
        }


    }
}
