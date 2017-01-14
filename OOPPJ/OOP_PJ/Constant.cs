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
        public enum ActionType
        {
            None,
            Select,     // 선택
            //MoveDrag,   // 선택후 드래그
            Draw,       // 그리기
            Fill,       // 채우기
            DrawAndFill,// 선 + 채우기
            Erase       // 삭제
        }

        public enum DragType
        {
            None,   
            Drag  // 임시대용으로 사용... 클릭 하고 드레그 구분 할 방법이 없음
        }

        public enum ShapeType
        {
            None,
            Pen,        // 연필
            Line,       // 라인
            Rectangle,  // 사각형
            Circle,     // 원
            Triangle    // 삼각형
        }
    }

    public class Infomation
    {
        private int thickness; // 펜 굵기
        private Color lineColor; // 선 색
        private Color fillColor; // 도형 채우기 색
        private bool useLine; // 선 사용
        private bool useFill; // 채우기 사용
        private Point point; // x,y 좌표
        private Constant.ShapeType shapeType;
        private Constant.ActionType actionType;
        private Constant.DragType drag;   // 임시방편 클릭 후 드레그 구분 할 방법이 없음...

        public Infomation()
        {
            useFill = false;
            useLine = true;
            shapeType = Constant.ShapeType.None;
            actionType = Constant.ActionType.None;
            drag = Constant.DragType.None;
        }

        public int Thickness
        {
            set { thickness = value; }
            get { return thickness; }
        }
        public Color LineColor
        {
            set { lineColor = value; }
            get { return lineColor; }
        }
        public Color FillColor
        {
            set { fillColor = value; }
            get { return fillColor; }
        }
        public bool UseLine
        {
            set { useLine = value; }
            get { return useLine; }
        }
        public bool UseFill
        {
            set { useFill = value; }
            get { return useFill; }
        }
        public Point Point
        {
            set { point = value; }
            get { return point; }
        }
        public Constant.ShapeType ShapeType
        {
            set { shapeType = value; }
            get { return shapeType; }
        }
        public Constant.ActionType ActionType
        {
            set { actionType = value; }
            get { return actionType; }
        }
        public Constant.DragType Drag // 임시 방편....
        {
            set { drag = value; }
            get { return drag; }
        }
    }

    static class WIUtility
    {
        // color 차후 추가 해야함
        public static Color[] colorSet = {Color.Black, Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.White };

        static public Rectangle GetPositiveRectangle(Point start, Point end)
        {
            Rectangle rect = new Rectangle
            {
                X = end.X > start.X ? start.X : end.X,
                Y = end.Y > start.Y ? start.Y : end.Y,
                Width = Math.Abs(end.X - start.X),
                Height = Math.Abs(end.Y - start.Y)
            };
            return rect;
        }

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
            //if ((InRectanglePt(rect, x,y) && !IsRectangleShape(rect, x, y)))
            if(
               ((rect.X - 20 <= x && rect.X + offset >= x) && (rect.Y <= y && rect.Y + rect.Height >= y)) ||
               (rect.X + rect.Width + offset >= x && (rect.X + rect.Width - offset) <= x && rect.Y + rect.Height >= y) ||
               (rect.Y - 20 <= y && (rect.Y + offset >= y) && (rect.X <= x && rect.X + rect.Width >= x)) ||
               ((rect.Y + rect.Height + offset >= y) && (rect.Y + rect.Height - offset) <= y && (rect.X <= x && rect.X + rect.Width >= x))            
               )
               return true;
           else
               return false;
        }
        // 선택 이동시 클릭 한곳이 선이 아닌 도형안인지 확인
        static public bool IsRectangleShape(Rectangle rect, int x, int y)
        {
            int offset = 20;
            if (x > rect.X + offset && x < rect.X + rect.Width - offset && y > rect.Y + offset && y < rect.Y + rect.Height - offset)
                return true;
            else
                return false;
        }

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
