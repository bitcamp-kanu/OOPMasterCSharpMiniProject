using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OOP_PJ
{
    public partial class Form1 : Form
    {
        Infomation theInfomation;
        CommandManager myCommandManager;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 폼 로드
            theInfomation = new Infomation();
            myCommandManager = new CommandManager();

        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // 마우스 다운
            if (e.X >= 0 && e.X <= this.Width && e.Y >= 0 && e.Y <= this.Height) // 폼안에서 이벤트 시작
            {
                switch (theInfomation.ShapeType) // 도형 타입이 선택 되었을때 그리기 시작
                {
                    case Constant.ShapeType.Circle:
                    case Constant.ShapeType.Rectangle:
                        theInfomation.ActionType = Constant.ActionType.Draw;
                        break;
                }

                if (theInfomation.ActionType.Equals(Constant.ActionType.Draw))   // 그리기 이벤트 ON
                {
                    //switch (theInfomation.ShapeType) // 그리기 형태
                   // {
                     //   case Constant.ShapeType.Circle:

                            theInfomation.Point = new Point(e.X, e.Y);

                            myCommandManager.CreateMain(theInfomation);

                       //     break;

                       // case Constant.ShapeType.Rectangle:
                         //   break;

                  //  }
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (theInfomation.ActionType == Constant.ActionType.Draw)
            {
                theInfomation.Point = new Point(e.X, e.Y);
                myCommandManager.MoveMouse(theInfomation);
                
                Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // 마우스 업
            if (theInfomation.ActionType == Constant.ActionType.Draw)
            {
                
                theInfomation.ActionType = Constant.ActionType.None;
                theInfomation.Point = new Point(e.X, e.Y);
                myCommandManager.MoveMouse(theInfomation);
                myCommandManager.CreateComeplete(theInfomation);

                Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 패인터

            myCommandManager.testDraw(e.Graphics);
        }


        // 버튼 이벤트들 펼쳐서 사용하세요
        #region MyRegion
        
       
        private void BtnLine_Click(object sender, EventArgs e)
        {
            // 선 그리기
        }

        private void BtnEllipse_Click(object sender, EventArgs e)
        {
            // 타원그리기
            theInfomation.ShapeType = Constant.ShapeType.Circle;

        }

        private void BtnRec_Click(object sender, EventArgs e)
        {
            // 사각형 그리기
            theInfomation.ShapeType = Constant.ShapeType.Rectangle;

        }

        private void BtnTri_Click(object sender, EventArgs e)
        {
            // 삼각형 그리기
        }

        private void BtnPentagon_Click(object sender, EventArgs e)
        {
            // 오각형 그리기
        }

        private void BtnHexa_Click(object sender, EventArgs e)
        {
            // 육각형 그리기
        }

        private void BtnStar_Click(object sender, EventArgs e)
        {
            // 별 그리기
        }

        private void BtnText_Click(object sender, EventArgs e)
        {
            // 텍스트 추가
        }

        private void BtnChoice_Click(object sender, EventArgs e)
        {
            // 도형 선택툴
        }

        private void BtnFill_Click(object sender, EventArgs e)
        {
            // 채우기
        }

        private void BtnImg_Click(object sender, EventArgs e)
        {
            // 이미지 삽입
        }

        private void BtnPencil_Click(object sender, EventArgs e)
        {
            // 펜툴
        }

        private void BtnLineColor_Click(object sender, EventArgs e)
        {
            // 선 색
        }

        private void BtnBgColor_Click(object sender, EventArgs e)
        {
            // 전경색
        }

        private void BtnColor01_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 01번
        }

        private void BtnColor02_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 02번
        }

        private void BtnColor03_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 03번
        }

        private void BtnColor04_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 04번
        }

        private void BtnColor05_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 05번
        }

        private void BtnColor06_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 06번
        }

        private void BtnColor07_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 07번
        }

        private void BtnColor08_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 08번
        }

        private void BtnColor09_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 09번
        }

        private void BtnColor10_Click(object sender, EventArgs e)
        {
            // 컬러파레트 01열 10번
        }

        private void BtnColor11_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 01번
        }

        private void BtnColor12_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 02번
        }

        private void BtnColor13_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 03번
        }

        private void BtnColor14_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 04번
        }

        private void BtnColor15_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 05번
        }

        private void BtnColor16_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 06번
        }

        private void BtnColor17_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 07번
        }

        private void BtnColor18_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 08번
        }

        private void BtnColor19_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 09번
        }

        private void BtnColor20_Click(object sender, EventArgs e)
        {
            // 컬러파레트 02열 10번
        }

        private void BtnColorCh_Click(object sender, EventArgs e)
        {
            // 컬러파레트 호출버튼
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            // Test용 차후 삭제 예정
            myCommandManager.Undo();
            Invalidate();

        }



    }
}
