using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace OOP_PJ
{
    public abstract class Shape
    {

        protected Stack<Shape> mHistory;

        public bool HasFill { get; set; }
        public bool HasLine { get; set; }
        public int Thickness { get; set; }
        public Color LineColor { get; set; }
        public Color FillColor { get; set; }
        public int SequenceNumber { get; set; }
        public int ListIndex { get; set; }
        public Rectangle MyRectangle { get; set; }
     

        public Shape(Rectangle recParam)
        {
            mHistory = new Stack<Shape>();
            MyRectangle = new Rectangle();
            MyRectangle = recParam;


            ListIndex = 0;
            SequenceNumber = 0;

        }

        //public Rectangle GetRenctangle() { return mRec; }
        //public void SetRenctangle(Rectangle recParam) { mRec = recParam; }


        public bool Undo() // 실행 취소
        {
            if (mHistory.Count > 0)
            {
                Shape tmp = mHistory.Pop();
                if (tmp.SequenceNumber == this.SequenceNumber)
                {
                    if (mHistory.Count == 0)
                        return false;
                    tmp = mHistory.Pop();
                }


                this.MyRectangle = tmp.MyRectangle;
                this.FillColor = tmp.FillColor;
                this.Thickness = tmp.Thickness;
                this.LineColor = tmp.LineColor;
                this.HasLine = tmp.HasLine;
                this.HasFill = tmp.HasFill;
                this.ListIndex = tmp.ListIndex;

                return true;
            }
            return false;
        }

        public void Save()
        {
            //Shape tmp = new Shape(this.mRec);
            Shape tmp = this.clone();
            tmp.ListIndex = this.ListIndex;
            tmp.Thickness = this.Thickness;
            tmp.LineColor = this.LineColor;
            tmp.FillColor = this.FillColor;
            tmp.HasLine = this.HasLine;
            tmp.HasFill = this.HasFill;
            this.SequenceNumber++;
            tmp.SequenceNumber = this.SequenceNumber;

            mHistory.Push(tmp);
        }

        public int GetStackCount()
        {
            return mHistory.Count;
        }

        public bool IsMyRange(Point selectedPoint)
        {
            // if (WIUtility.InEllipsePt(mRec, selectedPoint.X, selectedPoint.Y))    // 컨셉변경 사각형으로 선택 하기로 결정
            if (WIUtility.InRectanglePt(MyRectangle, selectedPoint.X, selectedPoint.Y))
                return true;
            else
                return false;
        }

        public abstract Shape clone();
        public abstract void Draw(Graphics g);
    }

    public class CCircle : Shape
    {
        public CCircle(Rectangle recParam) : base(recParam) {  }

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
        }

        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }
    
    public class CRectangle : Shape
    {
        public CRectangle(Rectangle recParam): base(recParam) {  }
    
        public override void Draw(Graphics g)
        {
            if (base.HasFill)
            {
                SolidBrush brush = new SolidBrush(base.FillColor);
                g.FillRectangle(brush, MyRectangle);
            }
            if(base.HasLine)
            {
                Pen pen = new Pen(base.LineColor, base.Thickness);
                g.DrawRectangle(pen, MyRectangle);
            }
        }

        public override Shape clone()
        {

            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }
    
    public class CTriagle : Shape
    {
        public CTriagle(Rectangle recParam) : base(recParam) { }
        
        public override void Draw(Graphics g)
        {
            PointF point1 = new PointF(MyRectangle.X + ((MyRectangle.Width) / 2), MyRectangle.Y);
            PointF point2 = new PointF(MyRectangle.X, MyRectangle.Y + MyRectangle.Height);
            PointF point3 = new PointF(MyRectangle.Width + MyRectangle.X, MyRectangle.Y + MyRectangle.Height);
            PointF[] curvePoints = { point1, point2, point3 };

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