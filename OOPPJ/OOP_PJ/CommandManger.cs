using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_PJ
{

    // 명령 관리자
    public class CommandAdmin
    {
        int shapeCount = 0;
        List<PositionInfo> positionList;
        PositionInfo state;
        ShapeManager shapeManager;

        public CommandAdmin()
        {
            positionList = new List<PositionInfo>();
            shapeManager = new ShapeManager();
            state = new PositionInfo();
        }

        public void SaveCmd()
        {
            positionList.Add(state);
        }

        public void ExcuterCommand(PositionInfo infoParam)
        {
            int cmd = infoParam.Command;

            switch (cmd)
            {
                case 1:
                    //CreateDummy(infoParam);
                    CreateShape(infoParam);

                    break;
                case 2:

                    break;
                case 3:
                    break;
                case 4:
                    Undo();
                    break;

            }
        }

        public void DrawAll(Graphics e)
        {

            shapeManager.Draw(e);
        }

        public void Undo()
        {
            // 저장하고 커맨드가 마지막 앞 커맨드가 생성이면 삭제if()
            shapeManager.RemoveShape(shapeCount - 1);
            shapeCount--;
            // 이동잉어
        }

        private void CreateShape(PositionInfo infoParam)
        {
            Rectangle myRectangle;
            if (infoParam.complete)
            {
                int x = infoParam.PointList[0].X;
                int y = infoParam.PointList[0].Y;
                int width = infoParam.PointList[infoParam.PointList.Count - 1].X - x;
                int height = infoParam.PointList[infoParam.PointList.Count - 1].Y - y;

                myRectangle = new Rectangle(x, y, width, height);
                shapeManager.AddRectangle(myRectangle);
                shapeCount++;
            }
            else
            {
                int x = infoParam.PointList[0].X;
                int y = infoParam.PointList[0].Y;
                int width = infoParam.PointList[infoParam.PointList.Count - 1].X - x;
                int height = infoParam.PointList[infoParam.PointList.Count - 1].Y - y;

                myRectangle = new Rectangle(x, y, width, height);
                shapeManager.ChageRectanglePostion(myRectangle);
            }

            state.myRectangle = myRectangle;
        }
    }


   

}
