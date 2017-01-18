using System;
using System.Drawing;


namespace OOP_PJ
{
    // 라인 만들자
    [Serializable]
    public class CLine : Shape
    {
        public CLine(Rectangle recParam) : base(recParam) { }

        public override void Draw(Graphics g)
        {
            if (base.HasLine)
            {
                if (pointList.Count > 0)
                {
                    Pen pen = new Pen(base.LineColor, base.Thickness);
                    g.DrawLine(pen, pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
                }
            }
        }

        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }
}
