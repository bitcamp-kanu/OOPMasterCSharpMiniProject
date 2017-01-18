using System;
using System.Drawing;


namespace OOP_PJ
{
    public class CPaint : Shape
    {
        public CPaint(Rectangle recParam) : base(recParam) { }

        public override void Draw(Graphics g) { }

        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }
}
