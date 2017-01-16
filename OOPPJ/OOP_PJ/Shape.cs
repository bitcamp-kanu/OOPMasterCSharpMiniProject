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
        private int thickness; // 펜 굵기
        private Color lineColor; // 선 색
        private Color fillColor; // 도형 채우기 색
        private bool hasLine; // 라인 그리기 여부
        private bool hasFill; // 채우기 여부
        private int sequenceNumber; // 상태 자동 저장 순서 번호(신경 쓸필요없음)

        public bool HasFill { set { hasFill = value; } get { return hasFill; } }
        public bool HasLine { set { hasLine = value; } get { return hasLine; } }
        public int Thickness { set { thickness = value; } get { return thickness; } }
        public Color LineColor { set { lineColor = value; } get { return lineColor; } }
        public Color FillColor { set { fillColor = value; } get { return fillColor; } }
        public int SequenceNumber { set { sequenceNumber = value; } get { return sequenceNumber; } }

        abstract public void Draw(Graphics g);
        abstract public Rectangle GetRenctangle();
        abstract public void SetRenctangle(Rectangle recParam);
        abstract public bool Undo();
        abstract public void Save();
        abstract public int GetStackCount();
        abstract public bool IsMyRange(Point selectedPoint);    // 선택 되었는지 체크
    }

    public class CCircle : Shape
    {
        private Stack<CCircle> mHistory;
        private Rectangle mRec;
        private int baseListIndex;

        public CCircle(Rectangle recParam)
        {
            mHistory = new Stack<CCircle>();
            mRec = new Rectangle();
            mRec = recParam;
            baseListIndex = 0;
            SequenceNumber = 0;
        }

        public int ListIndex
        {
            set { baseListIndex = value; }
            get { return baseListIndex; }
        }

        public override int GetStackCount()
        {
            return mHistory.Count;
        }

        public override void Draw(Graphics g)
        {
            if (base.HasFill)
            {
                SolidBrush brush = new SolidBrush(base.FillColor);
                g.FillEllipse(brush, mRec);
            }

            if (base.HasLine)
            {
                Pen pen = new Pen(base.LineColor, base.Thickness);
                g.DrawEllipse(pen, mRec);
            }

        }

        public override Rectangle GetRenctangle()
        {
            return mRec;
        }

        public override void SetRenctangle(Rectangle recParam)
        {
            mRec = recParam;
        }

        public override void Save()
        {
            CCircle tmp = new CCircle(this.mRec);
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

        public override bool Undo() // 실행 취소
        {
            if (mHistory.Count > 0)
            {
                CCircle tmp = mHistory.Pop();
                if (tmp.SequenceNumber == this.SequenceNumber)
                {
                    if (mHistory.Count == 0)
                        return false;
                    tmp = mHistory.Pop();
                }

                this.mRec = tmp.mRec;
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

        public override bool IsMyRange(Point selectedPoint)
        {
            if (WIUtility.InEllipsePt(mRec, selectedPoint.X, selectedPoint.Y))
                return true;
            else
                return false;
        }
    }


    public class CRectangle : Shape
    {
        private Stack<CRectangle> mHistory;
        private Rectangle mRec;
        private int baseListIndex;

        public CRectangle(Rectangle recParam)
        {
            mHistory = new Stack<CRectangle>();
            mRec = new Rectangle();
            mRec = recParam;
            baseListIndex = 0;
        }

        public int ListIndex
        {
            set { baseListIndex = value; }
            get { return baseListIndex; }
        }

        public override int GetStackCount()
        {
            return mHistory.Count;
        }

        public override void Draw(Graphics g)
        {
            if (base.HasFill)
            {
                SolidBrush brush = new SolidBrush(base.FillColor);
                g.FillRectangle(brush, mRec);
            }
            if (base.HasLine)
            {
                Pen pen = new Pen(base.LineColor, base.Thickness);
                g.DrawRectangle(pen, mRec);
            }

        }

        public override Rectangle GetRenctangle()
        {
            return mRec;
        }

        public override void SetRenctangle(Rectangle recParam)
        {
            mRec = recParam;
        }

        public override void Save()
        {
            CRectangle tmp = new CRectangle(this.mRec);
            tmp.baseListIndex = this.baseListIndex;
            tmp.Thickness = this.Thickness;
            tmp.LineColor = this.LineColor;
            tmp.FillColor = this.FillColor;
            tmp.HasLine = this.HasLine;
            tmp.HasFill = this.HasFill;
            this.SequenceNumber++;
            tmp.SequenceNumber = this.SequenceNumber;

            mHistory.Push(tmp);
        }


        public override bool Undo() // 실행 취소
        {
            if (mHistory.Count > 0)
            {
                CRectangle tmp = mHistory.Pop();
                if (tmp.SequenceNumber == this.SequenceNumber)
                {
                    if (mHistory.Count == 0)
                        return false;
                    tmp = mHistory.Pop();
                }

                this.mRec = tmp.mRec;
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

        public override bool IsMyRange(Point selectedPoint)
        {
            if (WIUtility.InRectanglePt(mRec, selectedPoint.X, selectedPoint.Y))
                return true;
            else
                return false;

        }
    }

    public class CTriagle : Shape
    {
        private Stack<CTriagle> mHistory;
        private Rectangle mRec;
        private int baseListIndex;

        public CTriagle(Rectangle recParam)
        {
            mHistory = new Stack<CTriagle>();
            mRec = new Rectangle();
            mRec = recParam;
            baseListIndex = 0;
        }

        public int ListIndex
        {
            set { baseListIndex = value; }
            get { return baseListIndex; }
        }

        public override int GetStackCount()
        {
            return mHistory.Count;
        }

        public override void Draw(Graphics g)
        {

            PointF point1 = new PointF(mRec.X + ((mRec.Width)/2), mRec.Y);
            PointF point2 = new PointF(mRec.X, mRec.Y + mRec.Height);
            PointF point3 = new PointF(mRec.Width + mRec.X, mRec.Y + mRec.Height);
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

        public override Rectangle GetRenctangle()
        {
            return mRec;
        }

        public override void SetRenctangle(Rectangle recParam)
        {
            mRec = recParam;
        }

        public override void Save()
        {
            CTriagle tmp = new CTriagle(this.mRec);
            tmp.baseListIndex = this.baseListIndex;
            tmp.Thickness = this.Thickness;
            tmp.LineColor = this.LineColor;
            tmp.FillColor = this.FillColor;
            tmp.HasLine = this.HasLine;
            tmp.HasFill = this.HasFill;
            this.SequenceNumber++;
            tmp.SequenceNumber = this.SequenceNumber;

            mHistory.Push(tmp);
        }


        public override bool Undo() // 실행 취소
        {
            if (mHistory.Count > 0)
            {
                CTriagle tmp = mHistory.Pop();
                if (tmp.SequenceNumber == this.SequenceNumber)
                {
                    if (mHistory.Count == 0)
                        return false;
                    tmp = mHistory.Pop();
                }

                this.mRec = tmp.mRec;
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

        public override bool IsMyRange(Point selectedPoint)
        {
            if (WIUtility.InRectanglePt(mRec, selectedPoint.X, selectedPoint.Y))
                return true;
            else
                return false;

        }
    }
}