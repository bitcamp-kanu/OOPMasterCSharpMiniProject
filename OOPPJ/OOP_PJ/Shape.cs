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
        public bool IsSelected { get; set; }
        public int Thickness { get; set; }
        public Color LineColor { get; set; }
        public Color FillColor { get; set; }
        public int SequenceNumber { get; set; }
        public int CurListIndex { get; set; }
        public int PreListIndex { get; set; }
        public Rectangle MyRectangle { get; set; }
        public List<Point> pointList;

        protected object lockObj = new object();
        
        public Shape(Rectangle recParam)
        {
            pointList = new List<Point>();
            mHistory = new Stack<Shape>();
            MyRectangle = new Rectangle();
            MyRectangle = recParam;

            CurListIndex = 0;
            PreListIndex = 0;
            SequenceNumber = 0;
            IsSelected = false;
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
                this.CurListIndex = tmp.CurListIndex;
                this.PreListIndex = tmp.PreListIndex;

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

        public void SetPositiveRectangle(Point start, Infomation newInfomation)
        {
            lock (lockObj)
            {
                if (newInfomation.ShapeType.Equals(Constant.ShapeType.Line))
                {
                    // 라인을 위한 처리
                    if (pointList.Count == 0)
                    {
                        Point point = new Point(start.X, start.Y);
                        pointList.Add(point);
                        pointList.Add(point);

                    }
                    Point point1 = new Point(newInfomation.Point.X, newInfomation.Point.Y);
                    pointList[1] = point1;
                }
                else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Pen))
                {

                    // 펜을 위한 처리
                    if (pointList.Count == 0)
                    {
                        Point point = new Point(start.X, start.Y);
                        pointList.Add(point);
                        pointList.Add(point);
                        //   Console.WriteLine("X: " + point.X + " , Y: " + point.Y);
                    }
                    Point point1 = new Point(newInfomation.Point.X, newInfomation.Point.Y);
                    pointList.Add(point1);
                    //  Console.WriteLine("X: " + point1.X + " , Y: " + point1.Y);
                }
                else
                {
                    Rectangle rect = new Rectangle
                    {
                        X = newInfomation.Point.X > start.X ? start.X : newInfomation.Point.X,
                        Y = newInfomation.Point.Y > start.Y ? start.Y : newInfomation.Point.Y,
                        Width = Math.Abs(newInfomation.Point.X - start.X),
                        Height = Math.Abs(newInfomation.Point.Y - start.Y)
                    };
                    MyRectangle = rect;
                }
            
            }
       
        }

        public void MoveRectangle(Point start, Point startMouse, Point info)
        {
            Rectangle moveChageRectangle = new Rectangle(
                            start.X + (info.X - startMouse.X),
                            start.Y + (info.Y - startMouse.Y),
                            MyRectangle.Width,
                            MyRectangle.Height);

            MyRectangle = moveChageRectangle;
        }

        public bool IsRctangleUpLine(Point newPoint)
        {
            int targetWidth = Constant.GuideOffset;
            int targetHeight = Constant.GuideOffset;

            int leftX = MyRectangle.X;
            int rightX = MyRectangle.X + MyRectangle.Width;
            int middleX = (leftX + rightX) / 2;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = middleX - (targetWidth / 2);
            scopeRec.Y = MyRectangle.Y - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, newPoint.X, newPoint.Y))
                return true;
            else
                return false;
        }

        public bool IsRctangleDownLine(Point newPoint)
        {
            int targetWidth = Constant.GuideOffset;
            int targetHeight = Constant.GuideOffset;

            int leftX = MyRectangle.X;
            int rightX = MyRectangle.X + MyRectangle.Width;
            int middleX = (leftX + rightX) / 2;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = middleX - (targetWidth / 2);
            scopeRec.Y = (MyRectangle.Y + MyRectangle.Height) - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, newPoint.X, newPoint.Y))
                return true;
            else
                return false;
        }

        public bool IsRctangleLeftLine(Point newPoint)
        {
            int targetWidth = Constant.GuideOffset;
            int targetHeight = Constant.GuideOffset;

            int upY = MyRectangle.Y;
            int downY = MyRectangle.Y + MyRectangle.Height;
            int middleY = (upY + downY) / 2;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = MyRectangle.X - (targetWidth / 2);
            scopeRec.Y = middleY - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, newPoint.X, newPoint.Y))
                return true;
            else
                return false;
        }

        public bool IsRctangleRightLine(Point newPoint)
        {
            int targetWidth = Constant.GuideOffset;
            int targetHeight = Constant.GuideOffset;

            int upY = MyRectangle.Y;
            int downY = MyRectangle.Y + MyRectangle.Height;
            int middleY = (upY + downY) / 2;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = MyRectangle.X + MyRectangle.Width - (targetWidth / 2);
            scopeRec.Y = middleY - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, newPoint.X, newPoint.Y))
                return true;
            else
                return false;
        }


        // 좌측 상단
        public bool IsRctangleLeftUpPointLine(Point newPoint)
        {
            int targetWidth = Constant.GuideOffset;
            int targetHeight = Constant.GuideOffset;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = MyRectangle.X - (targetWidth / 2);
            scopeRec.Y = MyRectangle.Y - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, newPoint.X, newPoint.Y))
                return true;
            else
                return false;
        }

        // 좌측 하단
        public bool IsRctangleLeftDwonPointLine(Point newPoint)
        {
            int targetWidth = Constant.GuideOffset;
            int targetHeight = Constant.GuideOffset;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = MyRectangle.X - (targetWidth / 2);
            scopeRec.Y = MyRectangle.Y + MyRectangle.Height - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, newPoint.X, newPoint.Y))
                return true;
            else
                return false;
        }

        // 우측 상단
        public bool IsRctangleRightUpPointLine(Point newPoint)
        {
            int targetWidth = Constant.GuideOffset;
            int targetHeight = Constant.GuideOffset;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = MyRectangle.X + MyRectangle.Width - (targetWidth / 2);
            scopeRec.Y = MyRectangle.Y - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, newPoint.X, newPoint.Y))
                return true;
            else
                return false;
        }

        // 우측 하단
        public bool IsRctangleRightDownPointLine(Point newPoint)
        {
            int targetWidth = Constant.GuideOffset;
            int targetHeight = Constant.GuideOffset;

            Rectangle scopeRec = new Rectangle(0, 0, targetWidth, targetHeight);

            scopeRec.X = MyRectangle.X + MyRectangle.Width - (targetWidth / 2);
            scopeRec.Y = MyRectangle.Y + MyRectangle.Height - (targetHeight / 2);

            if (IsRectangleShape(scopeRec, newPoint.X, newPoint.Y))
                return true;
            else
                return false;
        }

        // 마우스 포인터 모양 변경
        public void MouseTypeDecision(Infomation info, object obj)
        {
            Form myForm = obj as Form;

            if (IsRctangleUpLine(info.Point))
            {
                myForm.Cursor = Cursors.SizeNS;
            }
            else if (IsRctangleDownLine(info.Point))
            {
                myForm.Cursor = Cursors.SizeNS;
            }
            else if (IsRctangleLeftLine(info.Point))
            {
                myForm.Cursor = Cursors.SizeWE;
            }
            else if (IsRctangleRightLine(info.Point))
            {
                myForm.Cursor = Cursors.SizeWE;
            }
            else if (IsRctangleLeftUpPointLine(info.Point))
            {
                myForm.Cursor = Cursors.SizeNWSE;
            }
            else if (IsRctangleLeftDwonPointLine(info.Point))
            {
                myForm.Cursor = Cursors.SizeNESW;
            }
            else if (IsRctangleRightUpPointLine(info.Point))
            {
                myForm.Cursor = Cursors.SizeNESW;
            }
            else if (IsRctangleRightDownPointLine(info.Point))
            {
                myForm.Cursor = Cursors.SizeNWSE;
            }
            else if (IsMyRange(info.Point))
            {
                myForm.Cursor = Cursors.SizeAll;
            }
            else
            {
                myForm.Cursor = Cursors.Default;
            }
        }

        // 사이즈 조절
        public void ResizeUpSide(Point start, Point info)
        {
            Rectangle rect = new Rectangle(MyRectangle.X, info.Y, MyRectangle.Width, MyRectangle.Height + (MyRectangle.Y - info.Y));
            MyRectangle = rect;
        }

        public void ResizeDownSide(Point start, Point info)
        {
            Rectangle rect = new Rectangle(MyRectangle.X, MyRectangle.Y, MyRectangle.Width, info.Y -  MyRectangle.Y);
            MyRectangle = rect;
        }
        public void ResizeLeftSide(Point start, Point info)
        {
            Rectangle rect = new Rectangle(info.X, MyRectangle.Y, MyRectangle.Width + MyRectangle.X - info.X, MyRectangle.Height);
            MyRectangle = rect;
        }
        public void ResizeRightSide(Point start, Point info)
        {
            Rectangle rect = new Rectangle(MyRectangle.X, MyRectangle.Y, info.X - MyRectangle.X, MyRectangle.Height);
            MyRectangle = rect;
        }
        public void ResizeUpLeftSide(Point start, Point info)
        {
            Rectangle rect = new Rectangle(info.X, info.Y, MyRectangle.Width + MyRectangle.X - info.X, MyRectangle.Height + (MyRectangle.Y - info.Y));
            MyRectangle = rect;
        }
        public void ResizeLeftDownSide(Point start, Point info)
        {
            Rectangle rect = new Rectangle(info.X, start.Y, MyRectangle.Width + MyRectangle.X - info.X, info.Y - start.Y);
            MyRectangle = rect;
        }
        public void ResizeRihgtUpSide(Point start, Point info)
        {
            Rectangle rect = new Rectangle(MyRectangle.X, info.Y, MyRectangle.Width + info.X - (MyRectangle.Width + MyRectangle.X), MyRectangle.Height + (MyRectangle.Y - info.Y));
            MyRectangle = rect;
        }
        public void ResizeRightDownSide(Point start, Point info)
        {
            Rectangle rect = new Rectangle(MyRectangle.X, MyRectangle.Y, info.X - MyRectangle.X, info.Y - MyRectangle.Y);
            MyRectangle = rect;
        }
        
    }   // shape class


    public class CCircle : Shape
    {
        public CCircle(Rectangle recParam) : base(recParam) { }

        public override void Draw(Graphics g)
        {
            Console.WriteLine("draw");
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

            #region MyRegion

            if (this.IsSelected)
            {
                int miniRec = 10;
                int offset = 5;
                // up left 
                Rectangle tmp = new Rectangle(MyRectangle.X - offset, MyRectangle.Y - offset, miniRec, miniRec);

                g.DrawRectangle(new Pen(Color.White, 2), tmp);
                g.FillRectangle(Brushes.Gray, tmp);

                // up middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // up right
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left middle
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2;
                g.FillRectangle(Brushes.Gray, tmp);

                // right middle
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2 - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left down
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // down middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // right down
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);
            }
            #endregion
        }

        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }

    public class CRectangle : Shape
    {
        public CRectangle(Rectangle recParam) : base(recParam) { }

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

            #region MyRegion

            if (this.IsSelected)
            {
                int miniRec = 10;
                int offset = 5;
                // up left 
                Rectangle tmp = new Rectangle(MyRectangle.X - offset, MyRectangle.Y - offset, miniRec, miniRec);

                g.DrawRectangle(new Pen(Color.White, 2), tmp);
                g.FillRectangle(Brushes.Gray, tmp);

                // up middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // up right
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left middle
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2;
                g.FillRectangle(Brushes.Gray, tmp);

                // right middle
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2 - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left down
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // down middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // right down
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);
            }
            #endregion
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

            #region MyRegion
            
            if (this.IsSelected)
            {
                int miniRec = 10;
                int offset = 5;
                // up left 
                Rectangle tmp = new Rectangle(MyRectangle.X - offset, MyRectangle.Y - offset, miniRec, miniRec);

                g.DrawRectangle(new Pen(Color.White, 2), tmp);
                g.FillRectangle(Brushes.Gray, tmp);

                // up middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // up right
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left middle
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2;
                g.FillRectangle(Brushes.Gray, tmp);

                // right middle
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2 - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left down
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // down middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // right down
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);
            }
            #endregion

        }

        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();

            return data;
        }
    }

    // 오각형
    public class CPenta : Shape
    {
        public CPenta(Rectangle recParam) : base(recParam) { }
        public override void Draw(Graphics g)
        {
            PointF [] pts = new PointF[6];

            int centerX = MyRectangle.Width / 2;
            int centerY = MyRectangle.Height / 2;
            int radius = MyRectangle.Width/2;

            Point location = new Point(centerX, centerY);

            for (int i = 0; i < 5; ++i)
            {
                double radian = (0.8 * Math.PI * i) + (0.7 * Math.PI);
                pts[i] = location + new Size((int)(radius * Math.Cos(radian)),
                    (int)(radius * Math.Sin(radian)));
            }

            PointF point1 = new PointF(MyRectangle.X + pts[0].X, MyRectangle.Y + pts[0].Y);
            PointF point2 = new PointF(MyRectangle.X + pts[2].X, MyRectangle.Y + pts[2].Y);
            PointF point3 = new PointF(MyRectangle.X + pts[4].X, MyRectangle.Y + pts[4].Y);
            PointF point4 = new PointF(MyRectangle.X + pts[1].X, MyRectangle.Y + pts[1].Y);
            PointF point5 = new PointF(MyRectangle.X + pts[3].X, MyRectangle.Y + pts[3].Y);            


            PointF[] curvePoints = { point1, point2, point3, point4, point5 };

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

            #region MyRegion


            if (IsSelected)
            {
                int miniRec = 10;
                int offset = 5;
                // up left 
                Rectangle tmp = new Rectangle(MyRectangle.X - offset, MyRectangle.Y - offset, miniRec, miniRec);

                g.DrawRectangle(new Pen(Color.White, 2), tmp);
                g.FillRectangle(Brushes.Gray, tmp);

                // up middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // up right
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left middle
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2;
                g.FillRectangle(Brushes.Gray, tmp);

                // right middle
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height / 2 - miniRec / 2 - offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // left down
                tmp.X = MyRectangle.X - offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // down middle
                tmp.X = (MyRectangle.X + MyRectangle.Width / 2 - miniRec / 2);
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);

                // right down
                tmp.X = MyRectangle.X + MyRectangle.Width - miniRec + offset;
                tmp.Y = MyRectangle.Y + MyRectangle.Height - miniRec + offset;
                g.FillRectangle(Brushes.Gray, tmp);
            }
            #endregion

        }

        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }

    // 육각형 만들자
    public class CHexa : Shape
    {
        public CHexa(Rectangle recParam) : base(recParam) { }

        public override void Draw(Graphics g)
        {
            int HexaWidthSide = MyRectangle.Width / 4;
            PointF point1 = new PointF(MyRectangle.X + HexaWidthSide, MyRectangle.Y);
            PointF point2 = new PointF(MyRectangle.X + (MyRectangle.Width - HexaWidthSide), MyRectangle.Y);
            PointF point3 = new PointF(MyRectangle.X + MyRectangle.Width, MyRectangle.Y+(MyRectangle.Height/2));
            PointF point4 = new PointF(MyRectangle.X + (MyRectangle.Width - HexaWidthSide), MyRectangle.Y+MyRectangle.Height);
            PointF point5 = new PointF(MyRectangle.X + HexaWidthSide, MyRectangle.Y + MyRectangle.Height);
            PointF point6 = new PointF(MyRectangle.X, MyRectangle.Y+(MyRectangle.Height/2));

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

    // 라인 만들자
    public class CLine : Shape
    {
       public CLine(Rectangle recParam) : base(recParam) { 
        
        }

        public override void Draw(Graphics g)
        {
            if (base.HasFill)
            {
                //SolidBrush brush = new SolidBrush(base.FillColor);
                //g.FillPolygon(brush, curvePoints);
            }
            if (base.HasLine)
            {
                Pen pen = new Pen(base.LineColor, base.Thickness);
                g.DrawLine(pen, pointList[0].X, pointList[0].Y, pointList[1].X, pointList[1].Y);
            }
        }

        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }

    // 별 만들자
    public class CStar : Shape
    {
        public CStar(Rectangle recParam) : base(recParam) { }

        public override void Draw(Graphics g)
        {

            PointF[] pts = new PointF[6];
            PointF[] pts2 = new PointF[6];

            int centerX = MyRectangle.Width / 2;
            int centerY = MyRectangle.Height / 2;
            int radius = MyRectangle.Width / 2;
            int miniradius = MyRectangle.Width / 4;

            Point location = new Point(centerX, centerY);

            for (int i = 0; i < 5; ++i)
            {
                double radian = (0.8 * Math.PI * i) + (0.7 * Math.PI);
                pts[i] = location + new Size((int)(radius * Math.Cos(radian)),
                    (int)(radius * Math.Sin(radian)));
            }

            for (int i = 0; i < 5; ++i)
            {
                double radian = (0.8 * Math.PI * i) + (0.5 * Math.PI);
                pts2[i] = location + new Size((int)(miniradius * Math.Cos(radian)),
                    (int)(miniradius * Math.Sin(radian)));
            }

            PointF point1 = new PointF(MyRectangle.X + pts[0].X, MyRectangle.Y + pts[0].Y);
            PointF point2 = new PointF(MyRectangle.X + pts2[0].X, MyRectangle.Y + pts2[0].Y);
            PointF point3 = new PointF(MyRectangle.X + pts[2].X, MyRectangle.Y + pts[2].Y);
            PointF point4 = new PointF(MyRectangle.X + pts2[2].X, MyRectangle.Y + pts2[2].Y);
            PointF point5 = new PointF(MyRectangle.X + pts[4].X, MyRectangle.Y + pts[4].Y);
            PointF point6 = new PointF(MyRectangle.X + pts2[4].X, MyRectangle.Y + pts2[4].Y);
            PointF point7 = new PointF(MyRectangle.X + pts[1].X, MyRectangle.Y + pts[1].Y);
            PointF point8 = new PointF(MyRectangle.X + pts2[1].X, MyRectangle.Y + pts2[1].Y);
            PointF point9 = new PointF(MyRectangle.X + pts[3].X, MyRectangle.Y + pts[3].Y);
            PointF point10 = new PointF(MyRectangle.X + pts2[3].X, MyRectangle.Y + pts2[3].Y);
            
            PointF[] curvePoints = { point1, point2, point3, point4, point5, 
                                   point6, point7, point8, point9, point10 };

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

    ////////////////////////////////////자유곡선////////////////////////////
    // 자유곡선 만들어볼까?
    public class CPen : Shape
    {
        Pen pen = null;
        public CPen(Rectangle recParam) : base(recParam)
        {
            pen = new Pen(base.LineColor, base.Thickness);
        }
        public override void Draw(Graphics g)
        {
            //SolidBrush brush = new SolidBrush(base.LineColor);
            //Pen pen = new Pen(base.LineColor, base.Thickness);
            //using (Pen pen = new Pen(base.LineColor, base.Thickness))
            try
            {
                {
                    if (pen != null && pointList != null)
                    {
                        lock (lockObj)
                        {
                            //pen.Width = base.Thickness;
                            Point [] ps = pointList.ToArray();

                            foreach (PointF p in ps)
                            {
                                System.Diagnostics.Trace.WriteLine(p.ToString());
                            }

                            //PointF[] ps = new PointF[] { new PointF(10, 10), new PointF(10, 10), new PointF(10, 10) };
                            g.DrawLines(pen, ps);
                        }
                        
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public override Shape clone()
        {
            Shape data = (Shape)this.MemberwiseClone();
            return data;
        }
    }
}