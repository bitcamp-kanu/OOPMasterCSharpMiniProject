using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace OOP_PJ
{
    public class CPen : Shape
    {
        Pen pen = null;
        public CPen(Rectangle recParam)
            : base(recParam)
        {
            pen = new Pen(base.LineColor, base.Thickness);
        }
        public override void Draw(Graphics g)
        {
            pen.Color = LineColor;
            pen.Width = Thickness;
            Point[] ps = pointList.ToArray();

            {
                if (pointList.Count != 0)
                {
                    g.DrawLines(pen, ps);
                }
            }
        }

        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();
            data.pointList = new List<Point>();

            foreach (Point p in this.pointList)
            {
                data.pointList.Add(p);
            }

            return data;
        }
    }
}
