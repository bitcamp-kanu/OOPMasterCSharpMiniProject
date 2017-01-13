﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_PJ
{
    class Cosntant
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

        public enum Command
        {
            None,
            Select,     // 선택
            Draw,       // 그리기
            Fill,       // 채우기
            Erase       // 삭제
        }
    }

    public class PositionInfo
    {
        public List<Point> PointList;
        public Rectangle myRectangle;
        Color myPenColor; // 펜 색
        Color myBrushColor; // 채우기 색
        Cosntant.ShapeType ShapeType;
        Cosntant.Command Command;

        public PositionInfo()
        {
            PointList = new List<Point>();
            myRectangle = new Rectangle();
        }

    }

    static class WIUtility
    {
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
