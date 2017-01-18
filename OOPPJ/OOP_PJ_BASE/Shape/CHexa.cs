using System;
using System.Drawing;


namespace OOP_PJ
{
    // 육각형 만들자
    [Serializable]
    public class CHexa : Shape
    {
        public CHexa(Rectangle recParam) : base(recParam) { }

        public override void Draw(Graphics g)
        {
            int HexaWidthSide = MyRectangle.Width / 4;
            PointF point1 = new PointF(MyRectangle.X + HexaWidthSide, MyRectangle.Y);
            PointF point2 = new PointF(MyRectangle.X + (MyRectangle.Width - HexaWidthSide), MyRectangle.Y);
            PointF point3 = new PointF(MyRectangle.X + MyRectangle.Width, MyRectangle.Y + (MyRectangle.Height / 2));
            PointF point4 = new PointF(MyRectangle.X + (MyRectangle.Width - HexaWidthSide), MyRectangle.Y + MyRectangle.Height);
            PointF point5 = new PointF(MyRectangle.X + HexaWidthSide, MyRectangle.Y + MyRectangle.Height);
            PointF point6 = new PointF(MyRectangle.X, MyRectangle.Y + (MyRectangle.Height / 2));

            PointF[] curvePoints = { point1, point2, point3, point4, point5, point6 };

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
