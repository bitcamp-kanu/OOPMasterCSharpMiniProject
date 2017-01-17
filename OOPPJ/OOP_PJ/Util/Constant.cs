using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace OOP_PJ
{
    public class Constant
    {
        public static int GuideOffset = 30;

        public enum ActionType
        {
            None,
            Select,     // 선택
            Draw,       // 그리기
            Fill,       // 채우기
            DrawAndFill,// 선 + 채우기
            Erase       // 삭제
        }

        public enum MoveType
        {
            None,   
            DrawDrag,
            Moveshape,
            Resize, // 테스트 용
            UpResize,
            DownResize,
            LeftResize,
            RightResize,
            LeftUpResize,
            LeftDownResize,
            RightUpResize,
            RightDownResize
        }

        public enum ShapeType
        {
            None,
            Pen,        // 연필
            Line,       // 라인
            Rectangle,  // 사각형
            Circle,     // 원
            Triangle,    // 삼각형
            Pentagon,    // 오각형
            Hexagon,    // 육각형
            Star         // 별그리기
        }

        public enum MouseType
        {
            None,
            Default,
            SizeAll,
            SizeNESW,
            SizeNS,
            SizeNWSE,
            SizeWE
        }
    }

    public class Infomation
    {
        public int Thickness { get; set; }
        public Color LineColor { get; set; }
        public Color FillColor { get; set; }
        public bool UseLine { get; set; }
        public bool UseFill { get; set; }
        public Point Point { get; set; }
        public Constant.ShapeType ShapeType { get; set; }
        public Constant.ActionType ActionType { get; set; }
        public Constant.MoveType MoveType { get; set; }

        public Infomation()
        {
            UseFill = false;
            UseLine = true;
            ShapeType = Constant.ShapeType.None;
            ActionType = Constant.ActionType.None;
            MoveType = Constant.MoveType.None;
        }
        
    }

    public struct UndoInfo
    {
        public bool Success { get; set; }
        public bool OneStepMovement { get; set; }
        public bool DirectFrontMovement { get; set; }
        public bool DirectBackMovement { get; set; }

        public int PreIndex { get; set; }
        public int CurIndex { get; set; }


        public UndoInfo CloneEX()
        {
            UndoInfo info = (UndoInfo)this.MemberwiseClone();

            return info;
        }
    }

    static class WIUtility
    {
        
        // color 차후 추가 해야함
        public static Color[] colorSet = {
                                            Color.Black,
                                            Color.Gray,
                                            Color.Maroon,
                                            Color.Red,
                                            Color.DarkOrange,
                                            Color.Yellow,
                                            Color.Green,
                                            Color.DeepSkyBlue,
                                            Color.DodgerBlue,
                                            Color.Purple,
                                            Color.White,
                                            Color.LightGray,
                                            Color.RosyBrown,
                                            Color.Pink,
                                            Color.Gold,
                                            Color.PaleGoldenrod,
                                            Color.GreenYellow,
                                            Color.PaleTurquoise,
                                            Color.CadetBlue,
                                            Color.Thistle
                                         };


        /// <summary>
        /// 입력한 x,y 값이 원 안에 있는지검사 한다. (found75)
        /// </summary>
        /// <param name="rect">원을 그릴 사각형 좌표</param>
        /// <param name="x">x 좌표</param>
        /// <param name="y">y 좌표</param>
        /// <returns>true/false</returns>
        static public bool InEllipsePt(Rectangle rect, int x, int y)
        {
            int cy, cx, radius;
            cx = (rect.Width / 2) + rect.X;
            cy = (rect.Height / 2) + rect.Y;
            radius = rect.Width / 2;
            if (radius >= Math.Sqrt(Math.Pow(cx - x, 2) + Math.Pow(cy - y, 2)))
            {
                return true;
            }
            return false;
        }

        // 사각도형 선택 확인
        static public bool InRectanglePt(Rectangle rect, int x, int y)
        {
            if (x >= rect.X && x <= rect.X + rect.Width && y >= rect.Y && y <= rect.Y + rect.Height)
                return true;
            else
                return false;
        }
        

        // 선택 이동시 클릭 한게 선인지 확인
        // TODO : 알고리즘 수정 해야함... 라인 선택 과 도형선택 그리고 속도...
        static public bool IsRectangleLine(Rectangle rect, int x, int y)
        {
            int offset = 20;
            int outSide = 20;
            if(
               ((rect.X - outSide <= x && rect.X + offset >= x) && (rect.Y <= y && rect.Y + rect.Height >= y)) ||
               (rect.X + rect.Width + outSide >= x && (rect.X + rect.Width - offset) <= x && rect.Y + rect.Height + outSide >= y) ||
               (rect.Y - outSide <= y && (rect.Y + offset >= y) && (rect.X - outSide <= x && rect.X + rect.Width + outSide >= x)) ||
               ((rect.Y + rect.Height + outSide >= y) && (rect.Y + rect.Height - offset) <= y && (rect.X - outSide <= x && rect.X + rect.Width >= x))            
               )
               return true;
           else
               return false;
        }
        // 선택 이동시 클릭 한곳이 선이 아닌 도형안인지 확인
        //static public bool IsRectangleShape(Rectangle rect, int x, int y)
        //{
        //    int offset = 20;
        //    if (x > rect.X + offset && x < rect.X + rect.Width - offset && y > rect.Y + offset && y < rect.Y + rect.Height - offset)
        //        return true;
        //    else
        //        return false;
        //}

        /// <summary>
        /// 두 점 간에 거리를 구한다. (kimsoar)
        /// </summary>
        /// <param name="mx"></param>
        /// <param name="my"></param>
        /// <param name="catx"></param>
        /// <param name="caty"></param>
        /// <returns></returns>
        static public float CalcDistance(float mx, float my, float catx, float caty)
        {
            return (float)Math.Sqrt((double)(mx - catx) * (mx - catx) + (my - caty) * (my - caty));
        }        
    }
}
