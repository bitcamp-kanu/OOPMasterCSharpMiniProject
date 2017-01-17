using System;
using System.Drawing;

namespace OOP_PJ
{
    // 별 만들자
    public class CStar : Shape
    {
        public CStar(Rectangle recParam) : base(recParam) { }

        public override void Draw(Graphics g)
        {

            PointF[] pts = new PointF[6];
            PointF[] pts2 = new PointF[6];

            int centerX = MyRectangle.Width / 2;
            int centerY = MyRectangle.Height / 2;
            int radius = MyRectangle.Width / 2;
            int miniradius = MyRectangle.Width / 4;

            Point location = new Point(centerX, centerY);

            for (int i = 0; i < 5; ++i)
            {
                double radian = (0.8 * Math.PI * i) + (0.7 * Math.PI);
                pts[i] = location + new Size((int)(radius * Math.Cos(radian)),
                    (int)(radius * Math.Sin(radian)));
            }

            for (int i = 0; i < 5; ++i)
            {
                double radian = (0.8 * Math.PI * i) + (0.5 * Math.PI);
                pts2[i] = location + new Size((int)(miniradius * Math.Cos(radian)),
                    (int)(miniradius * Math.Sin(radian)));
            }

            PointF point1 = new PointF(MyRectangle.X + pts[0].X, MyRectangle.Y + pts[0].Y);
            PointF point2 = new PointF(MyRectangle.X + pts2[0].X, MyRectangle.Y + pts2[0].Y);
            PointF point3 = new PointF(MyRectangle.X + pts[2].X, MyRectangle.Y + pts[2].Y);
            PointF point4 = new PointF(MyRectangle.X + pts2[2].X, MyRectangle.Y + pts2[2].Y);
            PointF point5 = new PointF(MyRectangle.X + pts[4].X, MyRectangle.Y + pts[4].Y);
            PointF point6 = new PointF(MyRectangle.X + pts2[4].X, MyRectangle.Y + pts2[4].Y);
            PointF point7 = new PointF(MyRectangle.X + pts[1].X, MyRectangle.Y + pts[1].Y);
            PointF point8 = new PointF(MyRectangle.X + pts2[1].X, MyRectangle.Y + pts2[1].Y);
            PointF point9 = new PointF(MyRectangle.X + pts[3].X, MyRectangle.Y + pts[3].Y);
            PointF point10 = new PointF(MyRectangle.X + pts2[3].X, MyRectangle.Y + pts2[3].Y);

            PointF[] curvePoints = { point1, point2, point3, point4, point5, 
                                   point6, point7, point8, point9, point10 };

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
        }

        public override Shape clone()
        {

            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }
}
