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
        
        public bool HasFill { set { hasFill = value; } get { return hasFill; } }
        public bool HasLine { set { hasLine = value; } get { return hasLine; } }
        public int Thickness { set { thickness = value; } get { return thickness; } }
        public Color LineColor { set { lineColor = value; } get { return lineColor; } }
        public Color FillColor { set { fillColor = value; } get { return fillColor; } }

        abstract public void Draw(Graphics g);
        abstract public Rectangle GetRenctangle();
        abstract public void SetRenctangle(Rectangle recParam);
        abstract public bool Undo();
        abstract public void Save();
        abstract public int GetStackCount();
        abstract public bool IsMyRange(Point selectedPoint);    // 선택 되었는지 체크
    }

    public class CCircle : Shape, ICloneable
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
            
            if(base.HasLine)
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
            CCircle tmp = (CCircle)this.Clone();
            mHistory.Push(tmp);
        }

        public override bool Undo() // 실행 취소
        {
            bool result = false;

            if (mHistory.Count - 1 > 0)
            {
                result = true;
                mHistory.Pop();
                CCircle tmp = mHistory.Pop();
                this.FillColor = tmp.FillColor;
                this.Thickness = tmp.Thickness;
                this.LineColor = tmp.LineColor;
                this.HasLine = tmp.HasLine;
                this.HasFill = tmp.HasFill;
                this.ListIndex = tmp.ListIndex;

                for (int i = 0; i < mHistory.Count; i++)
                {
                    this.mHistory.Push(tmp.mHistory.ElementAt(i));
                }

                MessageBox.Show("history stack에 값이 들어있음" + mHistory.Count);
                return true;
            }

            return result;
        }

        public override bool IsMyRange(Point selectedPoint)
        {
            if (WIUtility.InEllipsePt(mRec, selectedPoint.X, selectedPoint.Y))
                return true;
            else
                return false;
        }

        public object Clone()
        {
            CCircle clone = new CCircle(mRec);
            clone.FillColor = base.FillColor;
            clone.Thickness = base.Thickness;
            clone.LineColor = base.LineColor;
            clone.HasLine = base.HasLine;
            clone.HasFill = base.HasFill;
            clone.ListIndex = this.ListIndex;

            for (int i = 0; i < mHistory.Count; i++)
            {
                clone.mHistory.Push(mHistory.ElementAt(i));
            }

            return clone;
        }
    }


    public class CRectangle : Shape, ICloneable
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
            if(base.HasLine)
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
            CRectangle tmp = (CRectangle)this.Clone();
            mHistory.Push(tmp);
        }


        public override bool Undo() // 실행 취소
        {
            bool result = false;

            if (mHistory.Count - 1 > 0)
            {
                result = true;
                //mHistory.Pop();
                CRectangle tmp = mHistory.Pop();
                this.FillColor = tmp.FillColor;
                this.Thickness = tmp.Thickness;
                this.LineColor = tmp.LineColor;
                this.HasLine = tmp.HasLine;
                this.HasFill = tmp.HasFill;
                this.ListIndex = tmp.ListIndex;

                for (int i = 0; i < mHistory.Count; i++)
                {
                    this.mHistory.Push(tmp.mHistory.ElementAt(i));
                }

                MessageBox.Show("history stack에 값이 들어있음" + mHistory.Count);
                return true;
            }

            return result;
        }

        public override bool IsMyRange(Point selectedPoint)
        {
            if (WIUtility.InRectanglePt(mRec, selectedPoint.X, selectedPoint.Y))
                return true;
            else
                return false;

        }

        public object Clone()
        {
            CRectangle clone = new CRectangle(mRec);
            clone.FillColor = base.FillColor;
            clone.Thickness = base.Thickness;
            clone.LineColor = base.LineColor;
            clone.HasLine = base.HasLine;
            clone.HasFill = base.HasFill;
            clone.ListIndex = this.ListIndex;
            
            for(int i=0;i<mHistory.Count;i++)
            {
                clone.mHistory.Push(mHistory.ElementAt(i));
            }

            return clone;
        }
    }
}
