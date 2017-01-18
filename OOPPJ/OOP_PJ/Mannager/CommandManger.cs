using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SocketBase;


namespace OOP_PJ
{
    // 명령 관리자
    public class CommandManager
    {
        SocketBase.UDPServerEx _serverEx = new UDPServerEx();

        Stack<Shape> backup;
        Shape dummyShape;
        ShapeManager shapeManager;
        Point startPoint;
        Point mousePoint;

        int startWidth;
        int startHeight;


        public CommandManager()
        {
            backup = new Stack<Shape>();
            shapeManager = new ShapeManager();
            startPoint.X = 0;
            startPoint.Y = 0;
            _serverEx.InitSocket();
            _serverEx.StartRecevie();
            _serverEx.StartQueue();
        }

        public void CreateMain(Infomation newInfomation)
        {
            newInfomation.MoveType = Constant.MoveType.DrawDrag;
            dummyShape = GetShape(newInfomation);
        }



        // TODO : 따로 파일로 빼던지 다른방법을 찾던지 너무 커짐 모든 도형별 모드 추가 해야함....
        private Shape GetShape(Infomation newInfomation) // 도형 얻어오기
        {
           
            if (newInfomation.ShapeType.Equals(Constant.ShapeType.Circle))  // 원
            {
                startPoint = newInfomation.Point;
                Rectangle rec = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                CCircle newCircle = new CCircle(rec);
                newCircle.FillColor = newInfomation.FillColor;
                newCircle.LineColor = newInfomation.LineColor;

                newCircle.Thickness = newInfomation.Thickness;

                if (newInfomation.UseLine)
                    newCircle.HasLine = true;
                if (newInfomation.UseFill)
                    newCircle.HasFill = true;

                return newCircle;
            }
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Rectangle))  // 사각 형
            {
                startPoint = newInfomation.Point;
                Rectangle rec = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                CRectangle newRectangle = new CRectangle(rec);
                newRectangle.FillColor = newInfomation.FillColor;
                newRectangle.LineColor = newInfomation.LineColor;

                newRectangle.Thickness = newInfomation.Thickness;

                if (newInfomation.UseLine)
                    newRectangle.HasLine = true;
                if (newInfomation.UseFill)
                    newRectangle.HasFill = true;

                return newRectangle;
            }
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Triangle)) //삼각형
            {
                startPoint = newInfomation.Point;
                Rectangle rec = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                CTriagle newRectangle = new CTriagle(rec);
                newRectangle.FillColor = newInfomation.FillColor;
                newRectangle.LineColor = newInfomation.LineColor;
                newRectangle.Thickness = newInfomation.Thickness;

                if (newInfomation.UseLine)
                    newRectangle.HasLine = true;
                if (newInfomation.UseFill)
                    newRectangle.HasFill = true;

                return newRectangle;
            }
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Pentagon)) //오각형
            {
                startPoint.X = newInfomation.Point.X;
                startPoint.Y = newInfomation.Point.Y;
                Rectangle rec = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                CPenta newRectangle = new CPenta(rec);
                newRectangle.FillColor = newInfomation.FillColor;
                newRectangle.LineColor = newInfomation.LineColor;
                newRectangle.Thickness = newInfomation.Thickness;

                if (newInfomation.UseLine)
                    newRectangle.HasLine = true;
                if (newInfomation.UseFill)
                    newRectangle.HasFill = true;

                return newRectangle;
            }
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Hexagon)) //육각형
            {
                startPoint.X = newInfomation.Point.X;
                startPoint.Y = newInfomation.Point.Y;
                Rectangle rec = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                CHexa newRectangle = new CHexa(rec);
                newRectangle.FillColor = newInfomation.FillColor;
                newRectangle.LineColor = newInfomation.LineColor;
                newRectangle.Thickness = newInfomation.Thickness;

                if (newInfomation.UseLine)
                    newRectangle.HasLine = true;
                if (newInfomation.UseFill)
                    newRectangle.HasFill = true;

                return newRectangle;
            }
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Star)) //별그리기
            {
                startPoint.X = newInfomation.Point.X;
                startPoint.Y = newInfomation.Point.Y;
                Rectangle rec = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                CStar newRectangle = new CStar(rec);
                newRectangle.FillColor = newInfomation.FillColor;
                newRectangle.LineColor = newInfomation.LineColor;
                newRectangle.Thickness = newInfomation.Thickness;

                if (newInfomation.UseLine)
                    newRectangle.HasLine = true;
                if (newInfomation.UseFill)
                    newRectangle.HasFill = true;

                return newRectangle;
            }
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Line)) // 라인그리기
            {
                startPoint.X = newInfomation.Point.X;
                startPoint.Y = newInfomation.Point.Y;
                Rectangle rec = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                CLine newRectangle = new CLine(rec);
                newRectangle.FillColor = newInfomation.FillColor;
                newRectangle.LineColor = newInfomation.LineColor;
                newRectangle.Thickness = newInfomation.Thickness;

                if (newInfomation.UseLine)
                    newRectangle.HasLine = true;
                if (newInfomation.UseFill)
                    newRectangle.HasFill = true;

                return newRectangle;
            }
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Pen)) // 자유곡선
            {
                startPoint.X = newInfomation.Point.X;

                startPoint.Y = newInfomation.Point.Y;
                Rectangle rec = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                CPen newRectangle = new CPen(rec);
                newRectangle.FillColor = newInfomation.FillColor;
                newRectangle.LineColor = newInfomation.LineColor;
                newRectangle.Thickness = newInfomation.Thickness;

                if (newInfomation.UseLine)
                    newRectangle.HasLine = true;
                if (newInfomation.UseFill)
                    newRectangle.HasFill = true;


                return newRectangle;
            }
            else // 기타 도형 추가
            {
                return null;
            }
        }

        public void ChoiceShape(Infomation newInfomation)
        {
            if (dummyShape != null)
            {
                dummyShape.IsSelected = false;
                dummyShape = null;
            }

            // 선택 모드 도형 얻어오기
            dummyShape = shapeManager.ChoicedShape(newInfomation);

            if (dummyShape != null)
            {
                dummyShape.IsSelected = true;

                if (dummyShape.IsRctangleUpLine(newInfomation.Point)) { newInfomation.MoveType = Constant.MoveType.UpResize; }
                else if (dummyShape.IsRctangleDownLine(newInfomation.Point)) { newInfomation.MoveType = Constant.MoveType.DownResize; }
                else if (dummyShape.IsRctangleLeftLine(newInfomation.Point)) { newInfomation.MoveType = Constant.MoveType.LeftResize; }
                else if (dummyShape.IsRctangleRightLine(newInfomation.Point)) { newInfomation.MoveType = Constant.MoveType.RightResize; }
                else if (dummyShape.IsRctangleLeftUpPointLine(newInfomation.Point)) { newInfomation.MoveType = Constant.MoveType.LeftUpResize; }
                else if (dummyShape.IsRctangleLeftDwonPointLine(newInfomation.Point)) { newInfomation.MoveType = Constant.MoveType.LeftDownResize; }
                else if (dummyShape.IsRctangleRightUpPointLine(newInfomation.Point)) { newInfomation.MoveType = Constant.MoveType.RightUpResize; }
                else if (dummyShape.IsRctangleRightDownPointLine(newInfomation.Point)) { newInfomation.MoveType = Constant.MoveType.RightDownResize; }
                else if (dummyShape.IsMyRange(newInfomation.Point)) { newInfomation.MoveType = Constant.MoveType.Moveshape; }
                else { newInfomation.MoveType = Constant.MoveType.None; }

                // 도형 이동을 위한 초기 위치 저장
                mousePoint = newInfomation.Point;
                startPoint.X = dummyShape.MyRectangle.X;  // 도형 초기위치
                startPoint.Y = dummyShape.MyRectangle.Y;
                startWidth = dummyShape.MyRectangle.Width;  // 도형 초기 크기
                startHeight = dummyShape.MyRectangle.Height;
            }
            else
            {
                newInfomation.MoveType = Constant.MoveType.None;
            }
        }

        public void CursorTypeDecision(Infomation newInfomation, object obj)
        {
            Form myForm = obj as Form;

            switch (newInfomation.ActionType)
            {
                case Constant.ActionType.Draw:
                    myForm.Cursor = Cursors.Cross;
                    break;

                case Constant.ActionType.Fill:
                    myForm.Cursor = Cursors.WaitCursor;
                    // 페인트 아이콘
                    break;

                default:
                    if (dummyShape != null)
                        dummyShape.MouseTypeDecision(newInfomation, obj);
                    break;
            }
        }

        public void MoveMouse(Infomation newInfomation, object obj)
        {
            if (newInfomation.MoveType.Equals(Constant.MoveType.DrawDrag)) { dummyShape.SetPositiveRectangle(startPoint, newInfomation); }
            else if (newInfomation.MoveType.Equals(Constant.MoveType.Moveshape)) { dummyShape.MoveRectangle(startPoint, mousePoint, newInfomation.Point); }
            else if (newInfomation.MoveType.Equals(Constant.MoveType.UpResize)) { dummyShape.ResizeUpSide(startPoint, newInfomation.Point); }
            else if (newInfomation.MoveType.Equals(Constant.MoveType.DownResize)) { dummyShape.ResizeDownSide(startPoint, newInfomation.Point); }
            else if (newInfomation.MoveType.Equals(Constant.MoveType.LeftResize)) { dummyShape.ResizeLeftSide(startPoint, newInfomation.Point); }
            else if (newInfomation.MoveType.Equals(Constant.MoveType.RightResize)) { dummyShape.ResizeRightSide(startPoint, newInfomation.Point); }
            else if (newInfomation.MoveType.Equals(Constant.MoveType.LeftUpResize)) { dummyShape.ResizeUpLeftSide(startPoint, newInfomation.Point); }
            else if (newInfomation.MoveType.Equals(Constant.MoveType.LeftDownResize)) { dummyShape.ResizeLeftDownSide(startPoint, newInfomation.Point); }
            else if (newInfomation.MoveType.Equals(Constant.MoveType.RightUpResize)) { dummyShape.ResizeRihgtUpSide(startPoint, newInfomation.Point); }
            else if (newInfomation.MoveType.Equals(Constant.MoveType.RightDownResize)) { dummyShape.ResizeRightDownSide(startPoint, newInfomation.Point); }
                
        }

        public void CreateComeplete(Infomation newInformation) // Form이벤트 완료
        {
            if (dummyShape != null)
            {
                if (newInformation.ActionType.Equals(Constant.ActionType.Draw)) // 그리기 모드
                {
                    shapeManager.AddShape(dummyShape);
                    backup.Push(dummyShape);
                    dummyShape.Save();
                    dummyShape = null;
                    PublishData();
                }
                else if (newInformation.ActionType.Equals(Constant.ActionType.Select)) // 선택 이동 모드
                {
                    backup.Push(dummyShape);
                    dummyShape.Save();
                    PublishData();
                }
            }
        }

        public void testDraw(Graphics g, Infomation newInfomation)    // Test용 차후 삭제
        {
            if (dummyShape != null && dummyShape.IsSelected)
            {
                dummyShape.Draw(g);

                Pen pen = new Pen(Color.Gray, 1);
                pen.DashStyle = DashStyle.Dash;

                g.DrawRectangle(pen, dummyShape.MyRectangle);
            }

            if (shapeManager.GetListCount() > 0)
            {
                shapeManager.Draw(g);

            }


            if (dummyShape != null && newInfomation.ActionType.Equals(Constant.ActionType.Draw))
            {
                dummyShape.Draw(g);

                if (!(dummyShape is CPen))
                {
                    Pen pen = new Pen(Color.Gray, 1);
                    pen.DashStyle = DashStyle.Dash;

                    g.DrawRectangle(pen, dummyShape.MyRectangle);
                }
            }

            //if (dummyShape != null && newInfomation.ActionType.Equals(Constant.ActionType.Draw))
            //{
            //    dummyShape.Draw(g);

            //    Pen pen = new Pen(Color.Gray, 1);
            //    pen.DashStyle = DashStyle.Dash;

            //    g.DrawRectangle(pen, dummyShape.MyRectangle);
            //}


            #region MyRegion
            //    if (dummyShape.IsSelected)
            //    {
            //        int miniRec = 10;
            //        int offset = 5;
            //        // up left 
            //        Rectangle tmp = new Rectangle(dummyShape.MyRectangle.X - offset, dummyShape.MyRectangle.Y - offset, miniRec, miniRec);

            //        g.DrawRectangle(new Pen(Color.White, 2), tmp);
            //        g.FillRectangle(Brushes.Gray, tmp);

            //        // up middle
            //        tmp.X = (dummyShape.MyRectangle.X + dummyShape.MyRectangle.Width / 2 - miniRec / 2);
            //        tmp.Y = dummyShape.MyRectangle.Y - offset;
            //        g.FillRectangle(Brushes.Gray, tmp);
                    
            //        // up right
            //        tmp.X = dummyShape.MyRectangle.X + dummyShape.MyRectangle.Width - miniRec + offset;
            //        tmp.Y = dummyShape.MyRectangle.Y - offset;
            //        g.FillRectangle(Brushes.Gray, tmp);

            //        // left middle
            //        tmp.X = dummyShape.MyRectangle.X - offset;
            //        tmp.Y = dummyShape.MyRectangle.Y + dummyShape.MyRectangle.Height / 2 - miniRec / 2;
            //        g.FillRectangle(Brushes.Gray, tmp);

            //        // right middle
            //        tmp.X = dummyShape.MyRectangle.X + dummyShape.MyRectangle.Width - miniRec + offset;
            //        tmp.Y = dummyShape.MyRectangle.Y + dummyShape.MyRectangle.Height / 2 - miniRec / 2 - offset;
            //        g.FillRectangle(Brushes.Gray, tmp);

            //        // left down
            //        tmp.X = dummyShape.MyRectangle.X - offset;
            //        tmp.Y = dummyShape.MyRectangle.Y + dummyShape.MyRectangle.Height - miniRec + offset;
            //        g.FillRectangle(Brushes.Gray, tmp);

            //        // down middle
            //        tmp.X = (dummyShape.MyRectangle.X + dummyShape.MyRectangle.Width / 2 - miniRec / 2);
            //        tmp.Y = dummyShape.MyRectangle.Y + dummyShape.MyRectangle.Height - miniRec + offset;
            //        g.FillRectangle(Brushes.Gray, tmp);

            //        // right down
            //        tmp.X = dummyShape.MyRectangle.X + dummyShape.MyRectangle.Width - miniRec + offset;
            //        tmp.Y = dummyShape.MyRectangle.Y + dummyShape.MyRectangle.Height - miniRec + offset;
            //        g.FillRectangle(Brushes.Gray, tmp);
            //    }
            //}
            #endregion
        }

        public void Undo()
        {
            dummyShape = null;
            if (backup.Count > 0)
            {
                shapeManager.Undo(backup.Pop());
            }
        }

        public void ChangeLineColor(Infomation theInfomation)   // 라인 색 변경시 이벤트
        {
            if (dummyShape != null)
            {
                if (dummyShape.HasLine)
                    dummyShape.HasLine = false;
                else
                    dummyShape.HasLine = true;

                dummyShape.LineColor = theInfomation.LineColor;
                backup.Push(dummyShape);
                dummyShape.Save();
                PublishData();
            }
        }

        public void ChangeFillColor(Infomation theInfomation)   // 채우기 색 변경시 이벤트
        {
            if (dummyShape != null)
            {
                if (dummyShape.HasFill)
                    dummyShape.HasFill = false;
                else
                    dummyShape.HasFill = true;

                dummyShape.FillColor = theInfomation.FillColor;
                backup.Push(dummyShape);
                dummyShape.Save();
                PublishData();
            }
        }

        public void MoveShapeBackOneStep()  // 화면 뒤쪽으로 도형 이동
        {
            if (dummyShape != null)
            {
                if (shapeManager.MoveShapeBackOneStep(dummyShape))
                {
                    backup.Push(dummyShape);
                    dummyShape.Save();
                    PublishData();
                }
            }
        }

        public void MoveShapeFrontOneStep() // 화면 앞쪽으로 도형 이동
        {
            if (dummyShape != null)
            {
                if (shapeManager.MoveShapeFrontOneStep(dummyShape))
                {
                    backup.Push(dummyShape);
                    dummyShape.Save();
                    PublishData();
                }
            }
        }

        public void DeleteShape()   // 도형 삭제
        {
            if (dummyShape != null)
            {
                shapeManager.DeleteShape(dummyShape);
                backup.Push(dummyShape);
                dummyShape.Save();
                dummyShape = null;
                PublishData();
            }
            
        }
        //public void MoveShapeFrontDirect()  // 도형 제일 앞으로
        //{
        //    if (dummyShape != null)
        //    {
        //        if (shapeManager.MoveShapeFrontDirect(dummyShape))
        //        {
        //            backup.Push(dummyShape);
        //            dummyShape.Save();
        //        }

        //    }
        //}

        //public void MoveShapeBackDirect()   // 도형 제일 뒤로
        //{
        //    if (dummyShape != null)
        //    {
        //        if (shapeManager.MoveShapeBackDirect(dummyShape))
        //        {
        //            backup.Push(dummyShape);
        //            dummyShape.Save();
        //        }

        //    }
            
        //}


        public bool ShowContextBox(Infomation newInfomation)
        {
             dummyShape = shapeManager.ChoicedShape(newInfomation);

             if (dummyShape != null)
             {
                 dummyShape.IsSelected = true;

                 return true;
             }
             return false;
        }

        //PublishData 함수 추가 각각의 클라이언트에 데이터를 전송 한다.
        public void PublishData()
        {
            SocketBase.StartPacket sta = new SocketBase.StartPacket();
            sta.totolCnt = shapeManager.GetListCount();
            _serverEx.AddSendData(Packet.Serialize(new SocketBase.StartPacket()));
            foreach (Shape sh in shapeManager.Shapes)
            {
                _serverEx.AddSendData(Packet.Serialize(sh));
            }
            _serverEx.AddSendData(Packet.Serialize(new SocketBase.LastPacket()));
        }

    }   // commandManger
}   // namespace
