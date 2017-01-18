using System;
using System.Drawing;

namespace OOP_PJ
{
    // 오각형
    public class CPenta : Shape
    {
        public CPenta(Rectangle recParam) : base(recParam) { }
        public override void Draw(Graphics g)
        {
            PointF[] pts = new PointF[6];

            int centerX = MyRectangle.Width / 2;
            int centerY = MyRectangle.Height / 2;
            int radius = MyRectangle.Width / 2;

            Point location = new Point(centerX, centerY);

            for (int i = 0; i < 5; ++i)
            {
                double radian = (0.8 * Math.PI * i) + (0.7 * Math.PI);
                pts[i] = location + new Size((int)(radius * Math.Cos(radian)),
                    (int)(radius * Math.Sin(radian)));
            }

            PointF point1 = new PointF(MyRectangle.X + pts[0].X, MyRectangle.Y + pts[0].Y);
            PointF point2 = new PointF(MyRectangle.X + pts[2].X, MyRectangle.Y + pts[2].Y);
            PointF point3 = new PointF(MyRectangle.X + pts[4].X, MyRectangle.Y + pts[4].Y);
            PointF point4 = new PointF(MyRectangle.X + pts[1].X, MyRectangle.Y + pts[1].Y);
            PointF point5 = new PointF(MyRectangle.X + pts[3].X, MyRectangle.Y + pts[3].Y);


            PointF[] curvePoints = { point1, point2, point3, point4, point5 };

            if (base.HasFill)
            {
                SolidBrush brush = new SolidBrush(base.FillColor);
                g.FillPolygon(brush, curvePoints);
            }
            if (base.HasLine)
            {
                Pen pen = new Pen(base.LineColor, base.Thickness);
                g.DrawPolygon(pen, curvePoints);
            }

            #region MyRegion


            if (IsSelected)
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
