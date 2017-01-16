using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace OOP_PJ
{
    // 명령 관리자
    public class CommandManager
    {
        Stack<Shape> backup;
        Shape dummyShape;
        ShapeManager shapeManager;
//        MouseObserver mouseObserver;
        int startX; // 도형 시작 위치
        int startY;
        int startWidth;
        int startHeight;
        int mouseX; // 마우스 시작위치
        int mouseY;

        public CommandManager()
        {
            backup = new Stack<Shape>();
            shapeManager = new ShapeManager();
            startX = 0;
            startY = 0;
        }

        public void CreateMain(Infomation newInfomation)
        {
            dummyShape = GetShape(newInfomation);
        }

        public void ChoiceShape(Infomation newInfomation)
        {
            // 선택 모드 도형 얻어오기
            dummyShape = shapeManager.ChoicedShape(newInfomation);
            
            if (dummyShape != null)
            {
                // 도형 이동을 위한 초기 위치 저장
                mouseX = newInfomation.Point.X; // 마우스 초기 위치
                mouseY = newInfomation.Point.Y;
                startX = dummyShape.MyRectangle.X;  // 도형 초기위치
                startY = dummyShape.MyRectangle.Y;
                startWidth = dummyShape.MyRectangle.Width;  // 도형 초기 크기
                startHeight = dummyShape.MyRectangle.Height;
            }
        }

        // TODO : 따로 파일로 빼던지 다른방법을 찾던지 너무 커짐 모든 도형별 모드 추가 해야함....
        private Shape GetShape(Infomation newInfomation) // 도형 얻어오기
        {
            if (newInfomation.ShapeType.Equals(Constant.ShapeType.Circle))  // 원
            {
                startX = newInfomation.Point.X;
                startY = newInfomation.Point.Y;
                Rectangle rec = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                CCircle newCircle = new CCircle(rec);
                newCircle.FillColor = newInfomation.FillColor;
                newCircle.LineColor = newInfomation.LineColor;

                newCircle.Thickness = newInfomation.Thickness;
                
                if(newInfomation.UseLine)
                    newCircle.HasLine = true;
                if (newInfomation.UseFill)
                    newCircle.HasFill = true;

                return newCircle;
            }
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Rectangle))  // 사각 형
            {
                startX = newInfomation.Point.X;
                startY = newInfomation.Point.Y;
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
                startX = newInfomation.Point.X;
                startY = newInfomation.Point.Y;
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
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Pentagon)) //삼각형
            {
                startX = newInfomation.Point.X;
                startY = newInfomation.Point.Y;
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
            else // 기타 도형 추가
            {
                return null;
            }
        }

        public void MoveTypeDecision(Infomation newInfomation, object obj)
        {
            WIUtility.MouseTypeDecision(newInfomation, obj, dummyShape);
        }

        public void MoveMouse(Infomation newInfomation, object obj)
        {
            if (dummyShape != null)
            {
                if (newInfomation.ActionType.Equals(Constant.ActionType.Draw))  // 그리기
                {
                    Rectangle rect = WIUtility.GetPositiveRectangle(new Point(startX, startY), newInfomation.Point);
                    dummyShape.MyRectangle = rect;
                }
                else if (newInfomation.Drag.Equals(Constant.DragType.Drag))   // 선택후 조작
                {
                    // TODO : 수정 해야함.................................
                    if (WIUtility.IsRectangleLine(dummyShape.MyRectangle, newInfomation.Point.X, newInfomation.Point.Y))  // Line 확인후 크기 조정
                    {
                        Point newPoint = new Point(
                            dummyShape.MyRectangle.X + startWidth + (newInfomation.Point.X - mouseX),
                            dummyShape.MyRectangle.Y + startHeight + (newInfomation.Point.Y - mouseY));

                        Rectangle sizeChageRectangle = WIUtility.GetPositiveRectangle(new Point(dummyShape.MyRectangle.X, dummyShape.MyRectangle.Y), newPoint);

                        dummyShape.MyRectangle = sizeChageRectangle;

                    }
                    else if (WIUtility.IsRectangleShape(dummyShape.MyRectangle, newInfomation.Point.X, newInfomation.Point.Y)) // 도형 범위 확인후 이동
                    {

                        Rectangle moveChageRectangle = new Rectangle(
                            startX + (newInfomation.Point.X - mouseX),
                            startY + (newInfomation.Point.Y - mouseY),
                            dummyShape.MyRectangle.Width,
                            dummyShape.MyRectangle.Height);

                        dummyShape.MyRectangle = moveChageRectangle;
                    }
                }
            }
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
                }
                else if (newInformation.ActionType.Equals(Constant.ActionType.Select)) // 선택 이동 모드
                {
                    backup.Push(dummyShape);
                    dummyShape.Save();
                }
            }
        }

        public void testDraw(Graphics g, Infomation newInfomation)    // Test용 차후 삭제
        {
            if (shapeManager.GetListCount() > 0)
            {
                shapeManager.Draw(g);
            }

            if (dummyShape != null)
            {
                dummyShape.Draw(g);

                if (newInfomation.ActionType.Equals(Constant.ActionType.Select))
                {
                    Pen pen = new Pen(Color.Gray,1);
                    pen.DashStyle = DashStyle.Dash;

                    g.DrawRectangle(pen, dummyShape.MyRectangle);
                }
            }
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
                dummyShape.HasLine = true;
                dummyShape.LineColor = theInfomation.LineColor;
                backup.Push(dummyShape);
                dummyShape.Save();
            }
            
        }

        public void ChangeFillColor(Infomation theInfomation)   // 채우기 색 변경시 이벤트
        {
            if (dummyShape != null)
            {
                dummyShape.HasFill = true;
                dummyShape.FillColor = theInfomation.FillColor;
                backup.Push(dummyShape);
                dummyShape.Save();
            }
        }
    }   // commandManger
}   // namespace
