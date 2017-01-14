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
        abstract public void Draw(Graphics g);
        abstract public Rectangle GetRenctangle();
        abstract public void SetRenctangle(Rectangle recParam);
        abstract public bool Undo();
        abstract public int GetStackCount();
    }

    public class CCircle : Shape
    {
        private Stack<CCircle> mHistory; 
        private Rectangle mRec;
        private Pen mPen;
        private int listIndex;
        
        public CCircle(Rectangle recParam)
        {
            mHistory = new Stack<CCircle>();
            mRec = new Rectangle();
            mRec = recParam;
            listIndex = 0;
        }

        public int ListIndex
        {
            set { listIndex = value; }
            get { return listIndex; }
        }

        public override int GetStackCount()
        {
            return mHistory.Count;   
        }

        public override void Draw(Graphics g)
        {
            mPen = new Pen(Color.DarkBlue, 1);
            g.DrawEllipse(mPen, mRec);
        }

        public override Rectangle GetRenctangle()
        {
            return mRec;
        }

        public override void SetRenctangle(Rectangle recParam)
        {
            mRec = recParam;
        }

        public override bool Undo() // 실행 취소
        {
            bool result = false;

            MessageBox.Show("history stack에 값이 들어있음");

            return result;
        }
    }


    public class CRectangle : Shape
    {
        private Stack<CCircle> mHistory;
        private Rectangle mRec;
        private Pen mPen;
        private int listIndex;

        public CRectangle(Rectangle recParam)
        {
            mHistory = new Stack<CCircle>();
            mRec = new Rectangle();
            mRec = recParam;
            listIndex = 0;
        }

        public int ListIndex
        {
            set { listIndex = value; }
            get { return listIndex; }
        }

        public override int GetStackCount()
        {
            return mHistory.Count;
        }

        public override void Draw(Graphics g)
        {
            mPen = new Pen(Color.DarkBlue, 1);
            g.DrawRectangle(mPen, mRec);
        }

        public override Rectangle GetRenctangle()
        {
            return mRec;
        }

        public override void SetRenctangle(Rectangle recParam)
        {
            mRec = recParam;
        }

        public override bool Undo() // 실행 취소
        {
            bool result = false;

            MessageBox.Show("history stack에 값이 들어있음");

            return result;
        }
    }
}
