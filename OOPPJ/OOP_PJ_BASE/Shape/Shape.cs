using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace OOP_PJ
{ 
    [Serializable]
    public abstract class Shape
    {
        protected Stack<Shape> mHistory;

        public static float Magnification;

        public bool HasFill { get; set; }
        public bool HasLine { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDeleted { get; set; }
        public Color LineColor { get; set; }
        public Color FillColor { get; set; }
        public int Thickness { get; set; }
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
            IsDeleted = false;
        }

        public abstract Shape clone();

        public abstract void Draw(Graphics g);

        public UndoInfo Undo() // 실행 취소
        {
            UndoInfo undoinfo = new UndoInfo();

            if (mHistory.Count > 0)
            {
                Shape tmp = mHistory.Pop();

                #region
                //if (tmp.CurListIndex == -1) // -1 화면 제일 뒤로 이동
                //{
                //    undoinfo.DirectFrontMovement = true;
                //    undoinfo.DirectBackMovement = false;
                //    undoinfo.PreIndex = tmp.PreListIndex;

                //    tmp = mHistory.Pop();

                //    this.MyRectangle = tmp.MyRectangle;
                //    this.FillColor = tmp.FillColor;
                //    this.Thickness = tmp.Thickness;
                //    this.LineColor = tmp.LineColor;
                //    this.HasLine = tmp.HasLine;
                //    this.HasFill = tmp.HasFill;
                //    this.IsDeleted = tmp.IsDeleted;
                //    this.PreListIndex = tmp.PreListIndex;
                //    this.CurListIndex = tmp.CurListIndex;
                //    List<Point> pointTmp = new List<Point>();

                //    foreach (Point p in tmp.pointList)
                //    {
                //        pointTmp.Add(p);
                //    }
                //    this.pointList = pointTmp;


                //    return undoinfo;
                //}
                #endregion

                if (tmp.SequenceNumber == this.SequenceNumber) // 현재상태면 이전껄로
                {
                    if (mHistory.Count == 0)
                    {
                        undoinfo.Success = false;
                
                        return undoinfo;
                    }
                    
                    tmp = mHistory.Pop();
                }

                // 이전 상태로
                this.MyRectangle = tmp.MyRectangle;
                this.FillColor = tmp.FillColor;
                this.Thickness = tmp.Thickness;
                this.LineColor = tmp.LineColor;
                this.HasLine = tmp.HasLine;
                this.HasFill = tmp.HasFill;
                this.IsDeleted = tmp.IsDeleted;

                List<Point> pointTmp2 = new List<Point>();
                
                foreach (Point p in tmp.pointList)
                {
                    pointTmp2.Add(p);
                }
                this.pointList = pointTmp2;

                if (this.CurListIndex != tmp.CurListIndex) // 앞 뒤로 한 스탭 이동
                {
                    undoinfo.PreIndex = tmp.CurListIndex;
                    undoinfo.CurIndex = this.CurListIndex;
                    undoinfo.OneStepMovement = true;
                    this.CurListIndex = tmp.CurListIndex;
                    this.PreListIndex = tmp.PreListIndex;
                }
                undoinfo.Success = true;

                return undoinfo;
            }
            undoinfo.Success = false;

            return undoinfo;
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

        // 마우스 도형 위치 체크
        public bool IsMyRange(Point selectedPoint)
        {
            if (IsDeleted)
                return false;
            else if (WIUtility.InRectanglePt(MyRectangle, selectedPoint.X, selectedPoint.Y))
                return true;
            else
                return false;
        }
        public bool IsRectangleShape(Rectangle rect, int x, int y)
        {
            if (IsDeleted)
                return false;
            else if (x >= rect.X && x <= rect.X + rect.Width && y >= rect.Y && y <= rect.Y + rect.Height)
                return true;
            else
                return false;
        }

        // 도형 셋팅
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
                        pointList.Add(new Point(MyRectangle.X,MyRectangle.Y));
                    }
                    Point point = new Point( newInfomation.Point.X, newInfomation.Point.Y);
                    
                    pointList.Add(point);
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

        // TODO : 무브
        public void MoveRectangle(Point start, Point startMouse, Point info, Point penStart, Point penEnd)
        {
            Rectangle moveChageRectangle = new Rectangle(
                            start.X + (info.X - startMouse.X),
                            start.Y + (info.Y - startMouse.Y),
                            MyRectangle.Width,
                            MyRectangle.Height);
            MyRectangle = moveChageRectangle;

            if (this is CLine)
            {
                Point p1 = new Point(penStart.X + (info.X - startMouse.X), penStart.Y + (info.Y - startMouse.Y));
                Point p2 = new Point(penEnd.X + (info.X - startMouse.X), penEnd.Y + (info.Y - startMouse.Y));

                pointList[0] = p1;
                pointList[1] = p2;
            }
            else if (this is CPen)
            {

            }
            
        }
        // 라인 체크
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
        public bool IsRctangleLeftUpPointLine(Point newPoint)
        {
            // 좌측 상단
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
        public bool IsRctangleLeftDwonPointLine(Point newPoint)
        {
            // 좌측 하단
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
        public bool IsRctangleRightUpPointLine(Point newPoint)
        {
            // 우측 상단
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
        public bool IsRctangleRightDownPointLine(Point newPoint)
        {
            // 우측 하단
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
        public void ResizeUpSide(Point start, Point info, Point penStart, Point penEnd)
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
}