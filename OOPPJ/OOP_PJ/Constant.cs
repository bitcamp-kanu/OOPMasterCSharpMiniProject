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
        public enum ShapeType
        {
            None,
            Pen,        // 연필
            Line,       // 라인
            Rectangle,  // 사각형
            Circle,     // 원
            Triangle    // 삼각형
        }

        public enum ActionType
        {
            None,
            Select,     // 선택
            Draw,       // 그리기
            Fill,       // 채우기
            Erase       // 삭제
        }
    }

    public class Infomation
    {
        private int thickness; // 펜 굵기
        private Color lineColor; // 선 색
        private Color fillColor; // 도형 채우기 색
        private Point point; // x,y 좌표
        private Constant.ShapeType shapeType;
        private Constant.ActionType actionType;

        public Infomation()
        {
            thickness = 1;
            lineColor = Color.Black;
            fillColor = Color.Orange;
            shapeType = Constant.ShapeType.None;
            actionType = Constant.ActionType.None;
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
    }



    static class WIUtility
    {
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
