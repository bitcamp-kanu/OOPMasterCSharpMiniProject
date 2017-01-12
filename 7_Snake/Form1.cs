using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7_Snake
{
    public static class MyConst
    {
        public const int BALL_SIZE = 20;
        public const int X_SIZE = BALL_SIZE * 20;
        public const int Y_SIZE = BALL_SIZE * 20;
        public const int X_WALL_CNT = X_SIZE / BALL_SIZE;
        public const int Y_WALL_CNT = Y_SIZE / BALL_SIZE;
        public static int FEED_CNT = 5;
    }

    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        Render mRender;

        int mDir = 1;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;

            timer.Interval += 50;
            timer.Tick += Timer_Tick;

            mRender = new Render();

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            mRender.Move(mDir);
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (mDir != 2)
                        mDir = 1;
                    break;
                case Keys.Left:
                    if(mDir != 1)
                        mDir = 2;
                    break;
                case Keys.Up:
                    if (mDir != 4)
                        mDir = 3;
                    break;
                case Keys.Down:
                    if (mDir != 3)
                        mDir = 4;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            mRender.Draw(e.Graphics);
        }
    }


    class Render
    {
        List<CShape> myCircle;

        public Render()
        {
            myCircle = new List<CShape>();

            AddUnit(1, Brushes.Red);
            AddWall();
            AddFeed(MyConst.FEED_CNT);
        }

        public void AddUnit(int dirParam, Brush brushColor)
        {
            int xPosition = MyConst.BALL_SIZE;
            int yPosition = MyConst.BALL_SIZE;
            int myDir = dirParam;

            if (myCircle.Count > 0) // 게임 중
            {
                CCircle beforeCircle = ((CCircle)myCircle[0]);

                for (int i = 1; i < myCircle.Count; i++)
                {
                    if (myCircle[i] is CCircle)
                    {
                        beforeCircle = (CCircle)myCircle[i];
                    }
                }

                myDir = beforeCircle.Dir;
                xPosition = beforeCircle.GetRectangle().X;
                yPosition = beforeCircle.GetRectangle().Y;

                switch (myDir)
                {
                    case 1: // right
                        xPosition -= MyConst.BALL_SIZE;
                        break;
                    case 2: // left
                        xPosition += MyConst.BALL_SIZE;
                        break;
                    case 3: // up
                        yPosition += MyConst.BALL_SIZE;
                        break;
                    case 4: // down
                        yPosition -= MyConst.BALL_SIZE;
                        break;
                }
            }

            myCircle.Add(new CCircle(xPosition, yPosition, myDir, brushColor));
        }

        public void AddWall() // 벽추가
        {
            const int x = MyConst.X_WALL_CNT;
            const int y = MyConst.Y_WALL_CNT;

            Pen mPen = new Pen(Color.Black, 2);
            Rectangle mRectangle = new Rectangle(0, 0, MyConst.BALL_SIZE, MyConst.BALL_SIZE);

            mRectangle.X = 0;
            mRectangle.Y = 0;

            for (int i = 0; i <= y; i++)
            {
                for (int j = 0; j <= x; j++)
                {
                    if (i == 0 || i == y || j == 0 || j == x) // 경기장 테두리만 벽 쌓기
                    {
                        mRectangle.X = j * MyConst.BALL_SIZE;
                        mRectangle.Y = i * MyConst.BALL_SIZE;

                        myCircle.Add(new CWall(mRectangle, mPen));
                    }
                }
            }
        }


        public void AddFeed(int cntParam)
        {
            Rectangle paramRectangle;
            Random myRand;
            myRand = new Random();

            int tX = 0;
            int tY = 0;

            for (int i = 0; i < cntParam; i++)
            {
                tX = ((myRand.Next(MyConst.X_SIZE - (MyConst.BALL_SIZE * 2)) + MyConst.BALL_SIZE + 1) / MyConst.BALL_SIZE) * MyConst.BALL_SIZE;
                tY = ((myRand.Next(MyConst.X_SIZE - (MyConst.BALL_SIZE * 2)) + MyConst.BALL_SIZE + 1) / MyConst.BALL_SIZE) * MyConst.BALL_SIZE;

                if( !CheckTouchBody(tX,tY) )
                {
                    paramRectangle = new Rectangle(tX, tY, MyConst.BALL_SIZE, MyConst.BALL_SIZE);
                    myCircle.Add(new CFeed(paramRectangle));
                }
                else
                {
                    i--;
                }
            }
        }
        
        public void Move(int paramDir)
        {
            CCircle beforeCircle = (CCircle)((CCircle)myCircle[0]).Clone();
            int beforeX;
            int beforeY;

            int goX = beforeX = beforeCircle.GetRectangle().X;
            int goY = beforeY = beforeCircle.GetRectangle().Y;
            bool lFlag = false;

            //TODO : 지금은 몸과 벽을 건들면 정지.
            //       Game Out 원할시 종료해주면 됨.
            switch (paramDir)
            {
                case 1:
                    goX += MyConst.BALL_SIZE;

                    if ((MyConst.X_SIZE - MyConst.BALL_SIZE) < goX || CheckTouchBody(goX, goY))
                        goX -= MyConst.BALL_SIZE;
                    else
                        lFlag = true;

                    break;

                case 2:
                    goX -= MyConst.BALL_SIZE;
                    if (goX < MyConst.BALL_SIZE || CheckTouchBody(goX, goY))
                        goX += MyConst.BALL_SIZE;
                    else
                        lFlag = true;
                    break;

                case 3:
                    goY -= MyConst.BALL_SIZE;

                    if (goY < MyConst.BALL_SIZE || CheckTouchBody(goX, goY))
                        goY += MyConst.BALL_SIZE;
                    else
                        lFlag = true;

                    break;

                case 4:
                    goY += MyConst.BALL_SIZE;

                    if ((MyConst.Y_SIZE - MyConst.BALL_SIZE) < goY || CheckTouchBody(goX, goY))
                        goY -= MyConst.BALL_SIZE;
                    else
                        lFlag = true;

                    break;
            }
            ((CCircle)myCircle[0]).Move(goX, goY, paramDir);

            int index;
            if ((index = CheckEatFood(goX, goY)) != 0)
            {
                myCircle.RemoveAt(index);
                AddUnit(paramDir, Brushes.Blue);
            }

            if (lFlag == true)
            {
                CCircle temp;

                for (int i = 1; i < myCircle.Count; i++)
                {
                    if (myCircle[i] is CCircle)
                    {
                        temp = (CCircle)((CCircle)myCircle[i]).Clone();

                        ((CCircle)myCircle[i]).Move(beforeCircle.GetRectangle().X, beforeCircle.GetRectangle().Y, beforeCircle.Dir);

                        beforeCircle = (CCircle)temp.Clone();
                    }
                }
            }
        }

        private bool CheckTouchBody(int xParam, int yParam)
        {
            bool lFlag = false; // 몸 건들면 true

            for (int i = 0; i < myCircle.Count; i++)
            {
                if (myCircle[i] is CCircle)
                {
                    if (((CCircle)myCircle[i]).GetRectangle().X == xParam && ((CCircle)myCircle[i]).GetRectangle().Y == yParam)
                    {
                        lFlag = true;
                    }
                }
            }
            return lFlag;
        }


        private int CheckEatFood(int xParam, int yParam)
        {
            int index = 0;
            int foodCnt = 0;

            for (int i = 0; i < myCircle.Count; i++)
            {
                if (myCircle[i] is CFeed)
                {
                    foodCnt++;
                    if (((CFeed)myCircle[i]).GetRectangle().X == xParam && ((CFeed)myCircle[i]).GetRectangle().Y == yParam)
                    {
                        index = i;
                    }
                }
            }

            if (foodCnt < 3)
                AddFeed(1);

            return index;
        }

        public void Draw(Graphics g)
        {
            foreach (CShape cs in myCircle)
            {
                cs.Draw(g);
            }
        }
    }

    /// <summary>
    /// 개별 도형 클래스
    /// </summary>
    public abstract class CShape
    {
        abstract public void Draw(Graphics g);
    }

    public class CCircle : CShape
    {
        private Rectangle mRec;
        private int mDir;
        private Brush mColor;

        public int Dir
        {
            get { return mDir; }
            set { mDir = value; }
        }

        public CCircle(int xParam, int yParam, int dirParam, Brush colorParam) 
        {
            mColor = colorParam;
            mRec = new Rectangle(xParam, yParam, MyConst.BALL_SIZE, MyConst.BALL_SIZE);
            Dir = dirParam;
        }

        public object Clone()
        {
            // CCircle ccircle = new CCircle(mRec.X, mRec.Y, mDir, mColor);
            CCircle ccircle = (CCircle)this.MemberwiseClone();
            // CCircle ccircle1 = (CCircle)this.MemberwiseClone(); 같은 역활 사용할수도 있다.
            return ccircle;
        }

        public Rectangle GetRectangle()
        {
            return mRec;
        }

        public void Move(int xParam, int yParam, int dirParam)
        {
            mRec.X = xParam;
            mRec.Y = yParam;
            mDir = dirParam;
        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(mColor, mRec);
        }
    }


    public class CWall : CShape
    {
        private Rectangle mRec;
        private Pen mPen;

        public CWall(Rectangle recParam, Pen paramPen)
        {
            mRec = new Rectangle();
            mRec = recParam;
            mPen = new Pen(paramPen.Color, paramPen.Width);
        }

        public override void Draw(Graphics g)
        {
            g.DrawRectangle(mPen, mRec);
        }
    }

    public class CFeed : CShape
    {
        private Rectangle mRec;

        public CFeed(Rectangle recParam)
        {
            mRec = new Rectangle();
            mRec = recParam;
        }

        public Rectangle GetRectangle()
        {
            return mRec;
        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Gray, mRec);
        }
    }
}