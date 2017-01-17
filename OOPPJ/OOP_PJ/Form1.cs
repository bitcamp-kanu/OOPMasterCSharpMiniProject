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

        List<CheckBox> CBgrp01 = new List<CheckBox>();
        List<CheckBox> CBgrp02 = new List<CheckBox>();
        List<CheckBox> CBgrp03 = new List<CheckBox>();
        List<CheckBox> CBgrp04 = new List<CheckBox>();

        // List<CheckBox> _rgBth = new List<CheckBox>();

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // CheckBox Group 01
            CBgrp01.Add(BtnPencil);
            CBgrp01.Add(BtnImg);
            CBgrp01.Add(BtnFill);
            CBgrp01.Add(BtnChoice);
            CBgrp01.Add(BtnText);
            CBgrp01.Add(BtnStar);
            CBgrp01.Add(BtnHexa);
            CBgrp01.Add(BtnPentagon);
            CBgrp01.Add(BtnTri);
            CBgrp01.Add(BtnRec);
            CBgrp01.Add(BtnEllipse);
            CBgrp01.Add(BtnLine);

            // CheckBox Group 02
            // 윤곽선과 채우기는 개별 선택하도록

            // CheckBox Group 03
            CBgrp03.Add(BtnLineColor);
            CBgrp03.Add(BtnBgColor);

            // CheckBox Group 04
            CBgrp04.Add(BtnColor01);
            CBgrp04.Add(BtnColor02);
            CBgrp04.Add(BtnColor03);
            CBgrp04.Add(BtnColor04);
            CBgrp04.Add(BtnColor05);
            CBgrp04.Add(BtnColor06);
            CBgrp04.Add(BtnColor07);
            CBgrp04.Add(BtnColor08);
            CBgrp04.Add(BtnColor09);
            CBgrp04.Add(BtnColor10);
            CBgrp04.Add(BtnColor11);
            CBgrp04.Add(BtnColor12);
            CBgrp04.Add(BtnColor13);
            CBgrp04.Add(BtnColor14);
            CBgrp04.Add(BtnColor15);
            CBgrp04.Add(BtnColor16);
            CBgrp04.Add(BtnColor17);
            CBgrp04.Add(BtnColor18);
            CBgrp04.Add(BtnColor19);
            CBgrp04.Add(BtnColor20);

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
            myCommandManager.CursorTypeDecision(theInfomation, this);
            label4.Text = "X: " + e.X.ToString() + ", Y: " + e.Y.ToString(); // 삭제예정
            // 마우스 다운
            if (e.X >= 0 && e.X <= this.Width && e.Y >= 0 && e.Y <= this.Height) // 폼안에서 이벤트 시작
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (theInfomation.ActionType.Equals(Constant.ActionType.Draw))   // 그리기 이벤트 ON
                    {
                        theInfomation.Point = new Point(e.X, e.Y);
                        myCommandManager.CreateMain(theInfomation);
                    }
                    else if (theInfomation.ActionType.Equals(Constant.ActionType.Select))   // 선택 Mode ON
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
            theInfomation.Point = new Point(e.X, e.Y);
            myCommandManager.CursorTypeDecision(theInfomation, this);

            label3.Text = "X: " + e.X.ToString() + ", Y: " + e.Y.ToString(); // 삭제 예정

            myCommandManager.MoveMouse(theInfomation, this);
           
            Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            myCommandManager.CursorTypeDecision(theInfomation, this);

            if (theInfomation.ActionType == Constant.ActionType.Draw)
            {
                theInfomation.Point = new Point(e.X, e.Y);
                myCommandManager.MoveMouse(theInfomation, this);
            }

            myCommandManager.CreateComeplete(theInfomation);
            theInfomation.MoveType = Constant.MoveType.None;
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // 패인터
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            myCommandManager.testDraw(e.Graphics, theInfomation);
        }

        private void UpDownLine_SelectedItemChanged(object sender, EventArgs e)
        {
            // 펜 굵기
            theInfomation.Thickness = Convert.ToInt32(UpDownLine.SelectedItem.ToString());
        }


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

            if (Constant.ActionType.Select.Equals(Constant.ActionType.Select))
            {
                myCommandManager.ChangeLineColor(theInfomation);
                Invalidate();
            }

            ComboBox tmp = (ComboBox)sender;
        }

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

            if (Constant.ActionType.Select.Equals(Constant.ActionType.Select))
            {
                myCommandManager.ChangeFillColor(theInfomation);
                Invalidate();
            }
        }

        //private void penChk_CheckedChanged(object sender, EventArgs e)
        //{
        //    // Line 사용
        //    if(penChk.Checked)
        //        theInfomation.UseLine = true;
        //    else
        //        theInfomation.UseLine = false;
        //}

        //private void FillChk_CheckedChanged(object sender, EventArgs e)
        //{
        //    // 채우기 사용
        //    if (FillChk.Checked)
        //        theInfomation.UseFill = true;
        //    else
        //        theInfomation.UseFill = false;
        //}

       

        private void Forward_btn_Click(object sender, EventArgs e)  // 도형 순서 위로 이동
        {
            myCommandManager.MoveShapeFrontOneStep();
            Invalidate();
        }

        private void Backward_btn_Click(object sender, EventArgs e) // 도형 순서 뒤로 이동
        {
            myCommandManager.MoveShapeBackOneStep();
            Invalidate();
        }

        int tmp = 0;
        private void buttontest_Click(object sender, EventArgs e)
        {
            tmp++;
            switch (tmp)
            {
                case 1:
                    this.Cursor = Cursors.SizeAll;
                    break;
                case 2:
                    this.Cursor = Cursors.SizeNESW;
                    break;
                case 3:
                    this.Cursor = Cursors.SizeNS;
                    break;
                case 4:
                    this.Cursor = Cursors.SizeNWSE;
                    break;
                case 5:
                    this.Cursor = Cursors.SizeWE;
                    break;
                
            }
        }

        // 다른 체크 박스 해제 함수화
        public void CheckingCheckBox(List<CheckBox> CBgrp, object sender)
        {
            foreach (CheckBox rd in CBgrp)
            {
                rd.Checked = false;
            }
            CheckBox rad = sender as CheckBox;
            rad.Checked = true;
        }

        private void PolygonClick_Click(object sender, EventArgs e)
        {
            CheckingCheckBox(CBgrp01, sender);

            if (sender == BtnPencil)
            {

            }
            else if (sender == BtnLine)
            {
                theInfomation.ActionType = Constant.ActionType.Draw;
                theInfomation.ShapeType = Constant.ShapeType.Line;
            }
            else if (sender == BtnEllipse)
            {
                theInfomation.ActionType = Constant.ActionType.Draw;
                theInfomation.ShapeType = Constant.ShapeType.Circle;
            }
            else if (sender == BtnRec)
            {
                theInfomation.ActionType = Constant.ActionType.Draw;
                theInfomation.ShapeType = Constant.ShapeType.Rectangle;
            }
            else if (sender == BtnTri)
            {
                theInfomation.ActionType = Constant.ActionType.Draw;
                theInfomation.ShapeType = Constant.ShapeType.Triangle;
            }
            else if (sender == BtnPentagon)
            {
                theInfomation.ActionType = Constant.ActionType.Draw;
                theInfomation.ShapeType = Constant.ShapeType.Pentagon;
            }
            else if (sender == BtnHexa)
            {
                theInfomation.ActionType = Constant.ActionType.Draw;
                theInfomation.ShapeType = Constant.ShapeType.Hexagon;
            }
            else if (sender == BtnStar)
            {
            }
            else if (sender == BtnText)
            {
            }
            else if (sender == BtnChoice)
            {
                if (BtnChoice.Checked)
                    theInfomation.ActionType = Constant.ActionType.Select;
                else
                    theInfomation.ActionType = Constant.ActionType.None;
            }
            else if (sender == BtnFill)
            {
            }
            else if (sender == BtnImg)
            {

            }
        }

        private void ColorChoice(object sender, EventArgs e)
        {
            CheckingCheckBox(CBgrp03, sender);            
        }

        // 컬러 check 너무 길다!
        private void ColorPt(object sender, EventArgs e)
        {
            CheckingCheckBox(CBgrp04, sender);

            if (BtnLineColor.Checked)
            {
                if (BtnColor01.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[0];
                    BtnLineColor.ForeColor = Color.White;
                    theInfomation.LineColor = WIUtility.colorSet[0];
                }
                else if (BtnColor02.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[1];
                    BtnLineColor.ForeColor = Color.White;
                    theInfomation.LineColor = WIUtility.colorSet[1];
                }
                else if (BtnColor03.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[2];
                    BtnLineColor.ForeColor = Color.White;
                    theInfomation.LineColor = WIUtility.colorSet[2];
                }
                else if (BtnColor04.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[3];
                    BtnLineColor.ForeColor = Color.White;
                    theInfomation.LineColor = WIUtility.colorSet[3];
                }
                else if (BtnColor05.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[4];
                    BtnLineColor.ForeColor = Color.White;
                    theInfomation.LineColor = WIUtility.colorSet[4];
                }
                else if (BtnColor06.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[5];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[5];
                }
                else if (BtnColor07.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[6];
                    BtnLineColor.ForeColor = Color.White;
                    theInfomation.LineColor = WIUtility.colorSet[6];
                }
                else if (BtnColor08.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[7];
                    BtnLineColor.ForeColor = Color.White;
                    theInfomation.LineColor = WIUtility.colorSet[7];
                }
                else if (BtnColor09.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[8];
                    BtnLineColor.ForeColor = Color.White;
                    theInfomation.LineColor = WIUtility.colorSet[8];
                }
                else if (BtnColor10.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[9];
                    BtnLineColor.ForeColor = Color.White;
                    theInfomation.LineColor = WIUtility.colorSet[9];
                }
                else if (BtnColor11.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[10];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[10];
                }
                else if (BtnColor12.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[11];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[11];
                }
                else if (BtnColor13.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[12];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[12];
                }
                else if (BtnColor14.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[13];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[13];
                }
                else if (BtnColor15.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[14];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[14];
                }
                else if (BtnColor16.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[15];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[15];
                }
                else if (BtnColor17.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[16];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[16];
                }
                else if (BtnColor18.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[17];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[17];
                }
                else if (BtnColor19.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[18];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[18];
                }
                else if (BtnColor20.Checked)
                {
                    BtnLineColor.BackColor = WIUtility.colorSet[19];
                    BtnLineColor.ForeColor = Color.Black;
                    theInfomation.LineColor = WIUtility.colorSet[19];
                }
            }
            else if (BtnBgColor.Checked)
            {
                if (BtnColor01.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[0];
                    BtnBgColor.ForeColor = Color.White;
                    theInfomation.FillColor = WIUtility.colorSet[0];
                }
                else if (BtnColor02.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[1];
                    BtnBgColor.ForeColor = Color.White;
                    theInfomation.FillColor = WIUtility.colorSet[1];
                }
                else if (BtnColor03.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[2];
                    BtnBgColor.ForeColor = Color.White;
                    theInfomation.FillColor = WIUtility.colorSet[2];
                }
                else if (BtnColor04.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[3];
                    BtnBgColor.ForeColor = Color.White;
                    theInfomation.FillColor = WIUtility.colorSet[3];
                }
                else if (BtnColor05.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[4];
                    BtnBgColor.ForeColor = Color.White;
                    theInfomation.FillColor = WIUtility.colorSet[4];
                }
                else if (BtnColor06.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[5];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[5];
                }
                else if (BtnColor07.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[6];
                    BtnBgColor.ForeColor = Color.White;
                    theInfomation.FillColor = WIUtility.colorSet[6];
                }
                else if (BtnColor08.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[7];
                    BtnBgColor.ForeColor = Color.White;
                    theInfomation.FillColor = WIUtility.colorSet[7];
                }
                else if (BtnColor09.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[8];
                    BtnBgColor.ForeColor = Color.White;
                    theInfomation.FillColor = WIUtility.colorSet[8];
                }
                else if (BtnColor10.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[9];
                    BtnBgColor.ForeColor = Color.White;
                    theInfomation.FillColor = WIUtility.colorSet[9];
                }
                else if (BtnColor11.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[10];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[10];
                }
                else if (BtnColor12.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[11];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[11];
                }
                else if (BtnColor13.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[12];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[12];
                }
                else if (BtnColor14.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[13];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[13];
                }
                else if (BtnColor15.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[14];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[14];
                }
                else if (BtnColor16.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[15];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[15];
                }
                else if (BtnColor17.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[16];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[16];
                }
                else if (BtnColor18.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[17];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[17];
                }
                else if (BtnColor19.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[18];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[18];
                }
                else if (BtnColor20.Checked)
                {
                    BtnBgColor.BackColor = WIUtility.colorSet[19];
                    BtnBgColor.ForeColor = Color.Black;
                    theInfomation.FillColor = WIUtility.colorSet[19];
                }
            }
        }

        private void btnUseLine_CheckedChanged(object sender, EventArgs e)
        {
            // 라인 사용 체크
            if (btnUseLine.Checked)
                theInfomation.UseLine = true;
            else
                theInfomation.UseLine = false;

            myCommandManager.ChangeLineColor(theInfomation);
            Invalidate();

        }

        private void btnUseFill_CheckedChanged(object sender, EventArgs e)
        {
            // 전경색 사용 체크
            if (btnUseFill.Checked)
                theInfomation.UseFill = true;
            else
                theInfomation.UseFill = false;

            myCommandManager.ChangeFillColor(theInfomation);
            Invalidate();
        }


        

        

        //public void GetMouseState()
        //{
        //    Constant.MouseType mouseType = my
        //    switch (mouseType)
        //    {
        //        case Constant.MouseType.Default:
        //            this.Cursor = Cursors.Default;
        //            break;
        //        case Constant.MouseType.SizeAll:
        //            this.Cursor = Cursors.SizeAll;
        //            break;

        //    }
        //}

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
    }
}
