using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OOP_PJ
{
    // 명령 관리자
    public class CommandManager
    {
        Stack<Shape> backup;
        Shape dummyShape;
        ShapeManager shapeManager;
        int startX;
        int startY;

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

        private Shape GetShape(Infomation newInfomation) // 도형 얻어오기
        {
            if (newInfomation.ShapeType.Equals(Constant.ShapeType.Circle))
            {
                startX = newInfomation.Point.X;
                startY = newInfomation.Point.Y;
                Rectangle theRectangle = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                return new CCircle(theRectangle);
            }
            else if (newInfomation.ShapeType.Equals(Constant.ShapeType.Rectangle))
            {
                startX = newInfomation.Point.X;
                startY = newInfomation.Point.Y;
                Rectangle theRectangle = new Rectangle(newInfomation.Point.X, newInfomation.Point.Y, 0, 0);
                return new CRectangle(theRectangle);
            }
            else
            {
                return null;
            }
        }

        public void MoveMouse(Infomation newInfomation)
        {
            if (dummyShape != null)
            {
                Rectangle myRectangle = WIUtility.GetPositiveRectangle( new Point(startX, startY), newInfomation.Point);
                
                dummyShape.SetRenctangle(myRectangle);
            }
        }

        public void CreateComeplete(Infomation newInformation)
        {
            if (dummyShape != null)
            {
                shapeManager.AddShape(dummyShape);
                backup.Push(dummyShape);
                dummyShape = null;
            }
        }

        public void testDraw(Graphics g)    // Test용 차후 삭제
        {
            if (shapeManager.GetListCount() > 0)
            {
                shapeManager.Draw(g);
            }

            if (dummyShape != null)
            {
                dummyShape.Draw(g);
            }
        }

        public void Undo()
        {
            if (backup.Count > 0)
            {
                shapeManager.Undo(backup.Pop());
            }
        }

    }   // commandManger
}   // namespace
