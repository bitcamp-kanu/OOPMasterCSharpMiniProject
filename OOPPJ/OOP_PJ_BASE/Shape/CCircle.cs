using System;
using System.Drawing;

namespace OOP_PJ
{
    [Serializable]
    public class CCircle : Shape
    {
        public CCircle(Rectangle recParam) : base(recParam) { }

        public override void Draw(Graphics g)
        {

            if (base.HasFill)
            {
                SolidBrush brush = new SolidBrush(base.FillColor);
                g.FillEllipse(brush, MyRectangle);
            }
            if (base.HasLine)
            {
                Pen pen = new Pen(base.LineColor, base.Thickness);
                g.DrawEllipse(pen, MyRectangle);
            }

            #region MyRegion

            if (this.IsSelected)
            {
                int miniRec = 10;
                int offset = 5;
                // up left 
                Rectangle tmp = new Rectangle(MyRectangle.X - offset, MyRectangle.Y - offset, miniRec, miniRec);

                g.DrawRectangle(new Pen(Color.White, 2), tmp);
                g.FillRectangle(Brushes.Gray, tmp);

                // up middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // up right
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left middle
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2;
                g.FillRectangle(Brushes.Gray, tmp);

                // right middle
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2 - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left down
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // down middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // right down
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);
            }
            #endregion
        }

        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }
}
