using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace OOP_PJ
{
    [Serializable]
    public class CPen : Shape
    {
        
        public CPen(Rectangle recParam)
            : base(recParam)
        {
        }
        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(base.LineColor, base.Thickness);
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
