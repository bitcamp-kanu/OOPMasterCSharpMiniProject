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
            theInfomation.Thickness = 1;
            theInfomation.LineColor = Color.Black; // 선 색 Default Black
            theInfomation.FillColor = Color.White; // 채우기 색 Default White
            this.AllowDrop = true;
            

            //////// 테스트용 삭제예정///////
            FillCombobox.SelectedIndex = 0;
            PenColorCombox.SelectedIndex = 0;
            
            //// 수정후 사용 예정 //////
             theInfomation.LineColor = WIUtility.colorSet[0];
             theInfomation.FillColor = WIUtility.colorSet[5];
            ///////////////////////////////////
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            label4.Text = "X: " + e.X.ToString() + ", Y: " + e.Y.ToString(); // 삭제예정
            // 마우스 다운
            if (e.X >= 0 && e.X <= this.Width && e.Y >= 0 && e.Y <= this.Height) // 폼안에서 이벤트 시작
            {
                // TODO : 수정....알고리즘....
                if (ChoiceChk.Checked)
                {
                    // ........ 추후 변경해야함......... 임시 UI 사용중
                    theInfomation.Drag = Constant.DragType.Drag;
                }
                
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (theInfomation.ActionType.Equals(Constant.ActionType.Draw))   // 그리기 이벤트 ON
                    {
                        theInfomation.Point = new Point(e.X, e.Y);
                        myCommandManager.CreateMain(theInfomation);
                    }
                    else if (theInfomation.Drag.Equals(Constant.DragType.Drag))   // 선택 Mode ON
                    {
                        theInfomation.Point = new Point(e.X, e.Y);
                        myCommandManager.ChoiceShape(theInfomation);
                        Invalidate();
                    }

                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    // 마우스 우측 클릭시 ContextMenu 사용
                }
            }
        }

        // TODO : 중복 기능 확인하기
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = "X: " + e.X.ToString() + ", Y: " + e.Y.ToString(); // 삭제 예정
            if (theInfomation.ActionType == Constant.ActionType.Draw)
            {
                theInfomation.Point = new Point(e.X, e.Y);
                myCommandManager.MoveMouse(theInfomation);
                
            }
            else if (theInfomation.Drag.Equals(Constant.DragType.Drag))
            {
                theInfomation.Point = new Point(e.X, e.Y);
                myCommandManager.MoveMouse(theInfomation);
            }
            Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // 마우스 업
            if (theInfomation.ActionType == Constant.ActionType.Draw)
            {
                theInfomation.Point = new Point(e.X, e.Y);
                myCommandManager.MoveMouse(theInfomation);
                myCommandManager.CreateComeplete(theInfomation);
            }
            else if (theInfomation.ActionType == Constant.ActionType.Select)
            {
                theInfomation.Drag = Constant.DragType.None;
                myCommandManager.CreateComeplete(theInfomation);
            }


            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 패인터
            // 안티 에일리어스 아직 미적용 상황봐서... g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            myCommandManager.testDraw(e.Graphics, theInfomation);
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
            theInfomation.ActionType = Constant.ActionType.Draw;
            theInfomation.ShapeType = Constant.ShapeType.Circle;

        }

        private void BtnRec_Click(object sender, EventArgs e)
        {
            // 사각형 그리기
            theInfomation.ActionType = Constant.ActionType.Draw;
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
            // 선 색 Default Black
            

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

        private void UpDownLine_SelectedItemChanged(object sender, EventArgs e)
        {
            // 펜 굵기
            theInfomation.Thickness = Convert.ToInt32(UpDownLine.SelectedItem.ToString());
        }
        #endregion



        /// <summary>
        /// Test Form 용입니다. 메뉴들 차후 삭제 예정이오니 삭제 금지!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // Undo Test용 차후 삭제 예정
            myCommandManager.Undo();

            Invalidate();
        }

        // TODO : 색 선택 함수로 빼자
        private void PenColorCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*       Black, Red, Blue         */
            if (PenColorCombox.SelectedIndex == 0)
            {
                theInfomation.LineColor = WIUtility.colorSet[0];
            }
            else if (PenColorCombox.SelectedIndex == 1)
            {
                theInfomation.LineColor = WIUtility.colorSet[1];
            }
            else if (PenColorCombox.SelectedIndex == 2)
            {
                theInfomation.LineColor = WIUtility.colorSet[2];
            }
            
            ComboBox tmp = (ComboBox)sender;
        }
<<<<<<< HEAD

        private void FillCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*While,Blue,Red,yello*/
            if (FillCombobox.SelectedIndex == 0)
            {
                theInfomation.FillColor = WIUtility.colorSet[5];
            }
            else if (FillCombobox.SelectedIndex == 1)
            {
                theInfomation.FillColor = WIUtility.colorSet[2];
            }
            else if (FillCombobox.SelectedIndex == 2)
            {
                theInfomation.FillColor = WIUtility.colorSet[1];
            }
            else if (FillCombobox.SelectedIndex == 3)
            {
                theInfomation.FillColor = WIUtility.colorSet[4]; ;
            }
        }

        private void penChk_CheckedChanged(object sender, EventArgs e)
        {
            // Line 사용
            if(penChk.Checked)
                theInfomation.UseLine = true;
            else
                theInfomation.UseLine = false;
        }

        private void FillChk_CheckedChanged(object sender, EventArgs e)
        {
            // 채우기 사용
            if (FillChk.Checked)
                theInfomation.UseFill = true;
            else
                theInfomation.UseFill = false;
        }

        private void ChoiceChk_CheckedChanged(object sender, EventArgs e)
        {
            if (ChoiceChk.Checked)
                theInfomation.ActionType = Constant.ActionType.Select;
            else
                theInfomation.ActionType = Constant.ActionType.None;
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
=======
>>>>>>> 3aa222297057a16661ca3e505e83521025147b03
    }
}
