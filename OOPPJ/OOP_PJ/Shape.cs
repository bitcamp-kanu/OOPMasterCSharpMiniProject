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

        public abstract Shape clone();
        public abstract void Draw(Graphics g);

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
            Shape tmp = this.clone();
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
            if (WIUtility.InRectanglePt(MyRectangle, selectedPoint.X, selectedPoint.Y))
                return true;
            else
                return false;
        }

        public bool IsRectangleShape(Rectangle rect, int x, int y)
        {
            if (x >= rect.X && x <= rect.X + rect.Width && y >= rect.Y && y <= rect.Y + rect.Height)
                return true;
            else
                return false;
        }

        public void SetPositiveRectangle(Point start, Point end)
        {
            Rectangle rect = new Rectangle
            {
                X = end.X > start.X ? start.X : end.X,
                Y = end.Y > start.Y ? start.Y : end.Y,
                Width = Math.Abs(end.X - start.X),
                Height = Math.Abs(end.Y - start.Y)
            };
            MyRectangle = rect;
        }


        public bool IsRctangleUpLine(Rectangle rect, int x, int y)
        {
            int targetWidth = 25;
            int targetHeight = 25;

            int leftX = rect.X;
            int rightX = rect.X + rect.Width;
            int middleX = (leftX + rightX) / 2;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = middleX - (targetWidth / 2);
            scopeRec.Y = rect.Y - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, x, y))
                return true;
            else
                return false;
        }

        public bool IsRctangleDownLine(Rectangle rect, int x, int y)
        {
            int targetWidth = 25;
            int targetHeight = 25;

            int leftX = rect.X;
            int rightX = rect.X + rect.Width;
            int middleX = (leftX + rightX) / 2;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = middleX - (targetWidth / 2);
            scopeRec.Y = (rect.Y + rect.Height) - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, x, y))
                return true;
            else
                return false;
        }

        public bool IsRctangleLeftLine(Rectangle rect, int x, int y)
        {
            int targetWidth = 25;
            int targetHeight = 25;

            int upY = rect.Y;
            int downY = rect.Y + rect.Height;
            int middleY = (upY + downY) / 2;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = rect.X - (targetWidth / 2);
            scopeRec.Y = middleY - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, x, y))
                return true;
            else
                return false;
        }

        public bool IsRctangleRightLine(Rectangle rect, int x, int y)
        {
            int targetWidth = 25;
            int targetHeight = 25;

            int upY = rect.Y;
            int downY = rect.Y + rect.Height;
            int middleY = (upY + downY) / 2;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = rect.X + rect.Width - (targetWidth / 2);
            scopeRec.Y = middleY - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, x, y))
                return true;
            else
                return false;
        }


        // 좌측 상단
        public bool IsRctangleLeftUpPointLine(Rectangle rect, int x, int y)
        {
            int targetWidth = 25;
            int targetHeight = 25;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = rect.X - (targetWidth / 2);
            scopeRec.Y = rect.Y - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, x, y))
                return true;
            else
                return false;
        }

        // 좌측 하단
        public bool IsRctangleLeftDwonPointLine(Rectangle rect, int x, int y)
        {
            int targetWidth = 25;
            int targetHeight = 25;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = rect.X - (targetWidth / 2);
            scopeRec.Y = rect.Y + rect.Height - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, x, y))
                return true;
            else
                return false;
        }

        // 우측 상단
        public bool IsRctangleRightUpPointLine(Rectangle rect, int x, int y)
        {
            int targetWidth = 25;
            int targetHeight = 25;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = rect.X + rect.Width - (targetWidth / 2);
            scopeRec.Y = rect.Y - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, x, y))
                return true;
            else
                return false;
        }

        // 우측 하단
        public bool IsRctangleRightDownPointLine(Rectangle rect, int x, int y)
        {
            int targetWidth = 25;
            int targetHeight = 25;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = rect.X + rect.Width - (targetWidth / 2);
            scopeRec.Y = rect.Y + rect.Height - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, x, y))
                return true;
            else
                return false;
        }

        // 마우스 포인터 모양 변경
        public void MouseTypeDecision(Infomation info, object obj, Shape shape)
        {
            Form myForm = obj as Form;

            if (shape != null)
            {
                if (IsRctangleUpLine(shape.MyRectangle, info.Point.X, info.Point.Y))
                {
                    myForm.Cursor = Cursors.SizeNS;
                }
                else if (IsRctangleDownLine(shape.MyRectangle, info.Point.X, info.Point.Y))
                {
                    myForm.Cursor = Cursors.SizeNS;
                }
                else if (IsRctangleLeftLine(shape.MyRectangle, info.Point.X, info.Point.Y))
                {
                    myForm.Cursor = Cursors.SizeWE;
                }
                else if (IsRctangleRightLine(shape.MyRectangle, info.Point.X, info.Point.Y))
                {
                    myForm.Cursor = Cursors.SizeWE;
                }
                else if (IsRctangleLeftUpPointLine(shape.MyRectangle, info.Point.X, info.Point.Y))
                {
                    myForm.Cursor = Cursors.SizeNWSE;
                }
                else if (IsRctangleLeftDwonPointLine(shape.MyRectangle, info.Point.X, info.Point.Y))
                {
                    myForm.Cursor = Cursors.SizeNESW;
                }
                else if (IsRctangleRightUpPointLine(shape.MyRectangle, info.Point.X, info.Point.Y))
                {
                    myForm.Cursor = Cursors.SizeNESW;
                }
                else if (IsRctangleRightDownPointLine(shape.MyRectangle, info.Point.X, info.Point.Y))
                {
                    myForm.Cursor = Cursors.SizeNWSE;
                }
                else if (IsRectangleShape(shape.MyRectangle, info.Point.X, info.Point.Y))
                {
                    myForm.Cursor = Cursors.SizeAll;
                }
                else
                {
                    myForm.Cursor = Cursors.Default;
                }
            }
            else
            {
                myForm.Cursor = Cursors.Default;
            }
        }

    }   // shape class


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
            if (base.HasLine) 
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