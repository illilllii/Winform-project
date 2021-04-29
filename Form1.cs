using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC_Project_20173141_20173155
{
    public partial class Form1 : Form
    {
        const int BAB_NUM = 30;
        int[,] game_pan = new int[33, 33];
        int len, len_;
        int[] bugX = new int[34];
        int[] bugY = new int[34];
        int[] bugXX = new int[34];

        int[] bugYY = new int[34];

        int xDir, yDir, xxDir, yyDir;

        Timer Timer1 = new Timer();

        public Form1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            Timer1.Interval = 1000;
            Timer1.Tick += timer1_Tick;

            Timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GamePanInit();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGamePan(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            CursorControl(e.KeyCode);
            CursorControl2p(e.KeyCode);
        }

        void babGenerator()
        {
            int i, x, y;
            Random rand = new Random();

            for (i = 0; i < BAB_NUM; i++)
            {
                x = rand.Next() % 31 + 1;
                y = rand.Next() % 31 + 1;
                if (game_pan[y, x] == 0)
                    game_pan[y, x] = 2;
                else
                {
                    i = i - 1;
                    continue;
                }
            }
            return;
        }
        void GamePanInit()
        {
            int i;

            for (i = 0; i < 33; i++)

            {
                game_pan[i, 0] = -1;
                game_pan[i, 32] = -1;
                game_pan[0, i] = -1;
                game_pan[32, i] = -1;
            }

            bugX[0] = 2; bugY[0] = 1;
            bugX[1] = 1; bugY[1] = 1;
            bugXX[0] = 30; bugYY[0] = 31;
            bugXX[1] = 31; bugYY[1] = 31;

            game_pan[bugY[0], bugX[0]] = 3;
            game_pan[bugY[1], bugX[1]] = 3;
            game_pan[bugYY[0], bugXX[0]] = 4;
            game_pan[bugYY[1], bugXX[1]] = 4;

            babGenerator();

            len = 2; len_ = 2;
            xDir = 0; yDir = 0;
            xxDir = 0; yyDir = 0;

        }

        void DrawGamePan(Graphics g)
        {
            int x, y, i;

            Pen blackPen = new Pen(Color.Black);
            Pen redPen = new Pen(Color.Red);
            Pen bluePen = new Pen(Color.Blue);
            Pen orangePen = new Pen(Color.Orange);
            Pen purplePen = new Pen(Color.Purple);

            for (y = 0; y < 33; y++)
            {
                for (x = 0; x < 33; x++)
                {
                    switch (game_pan[y, x])
                    {
                        case -1:
                            g.DrawRectangle(blackPen, x * 20, y * 20, 20, 20);
                            break;
                        case 2:
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            g.FillEllipse(blackBrush, x * 20, y * 20, 20, 20);
                            break;

                    }

                }

            }
            g.DrawEllipse(redPen, bugX[0] * 20, bugY[0] * 20, 20, 20);
            g.DrawEllipse(orangePen, bugXX[0] * 20, bugYY[0] * 20, 20, 20);//머리
            for (i = 1; i < len; i++)
            {
                g.DrawEllipse(bluePen, bugX[i] * 20, bugY[i] * 20, 20, 20);
            }
            for (i = 1; i < len_; i++)
            {
                g.DrawEllipse(purplePen, bugXX[i] * 20, bugYY[i] * 20, 20, 20);
            }//꼬리

        }

        void CursorControl(Keys DirectKey)
        {
            switch (DirectKey)
            {
                case Keys.A:
                    if (xDir == 1)
                        break;
                    if (game_pan[bugY[0], bugX[0] - 1] != -1)
                    {
                        xDir = -1;
                        yDir = 0;
                    }
                    break;
                case Keys.D:
                    if (xDir == -1)
                        break;
                    if (game_pan[bugY[0], bugX[0] + 1] != -1)
                    {
                        xDir = 1;
                        yDir = 0;
                    }
                    break;
                case Keys.W:
                    if (yDir == 1)
                        break;
                    if (game_pan[bugY[0] - 1, bugX[0]] != -1)
                    {
                        xDir = 0;
                        yDir = -1;
                    }
                    break;
                case Keys.S:
                    if (yDir == -1)
                        break;
                    if (game_pan[bugY[0] + 1, bugX[0]] != -1)
                    {
                        xDir = 0;
                        yDir = 1;
                    }
                    break;

            }
            
            Invalidate();

        }
        void CursorControl2p(Keys DirectKey)
        {
            switch (DirectKey)
            {
                case Keys.Left:
                    if (xxDir == 1)
                        break;
                    if (game_pan[bugYY[0], bugXX[0] - 1] != -1)
                    {
                        xxDir = -1;
                        yyDir = 0;
                    }
                    break;
                case Keys.Right:
                    if (xxDir == -1)
                        break;
                    if (game_pan[bugYY[0], bugXX[0] + 1] != -1)
                    {
                        xxDir = 1;
                        yyDir = 0;
                    }
                    break;
                case Keys.Up:
                    if (yyDir == 1)
                        break;
                    if (game_pan[bugYY[0] - 1, bugYY[0]] != -1)
                    {
                        xxDir = 0;
                        yyDir = -1;
                    }
                    break;
                case Keys.Down:
                    if (yyDir == -1)
                        break;
                    if (game_pan[bugYY[0] + 1, bugXX[0]] != -1)
                    {
                        xxDir = 0;
                        yyDir = 1;
                    }
                    break;
            }
            Invalidate();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            MovingBug();
            MovingBug2p();

            Invalidate();
            
        }

        void MovingBug()
        {
            int i;
            for (i = len; i >= 0; i--)
            {
                if (i != 0)
                {
                    bugX[i] = bugX[i-1];            
                    bugY[i] = bugY[i - 1];
                }
                game_pan[bugY[i], bugX[i]] = 3; 
            }
           
            if (xDir == 1)              
            {
                bugX[0]++;
            }
            else if (xDir == -1)      
            {
                bugX[0]--;
            }
            else if (yDir == 1)       
            {
                bugY[0]++;
            }
            else if (yDir == -1)       
            {
                bugY[0]--;
            }

            if (game_pan[bugY[0], bugX[0]] == 2)
            {
                game_pan[bugY[0], bugX[0]] = 0;
                len++;

            }
            
            if (game_pan[bugY[0], bugX[0]] == 3 || game_pan[bugY[0], bugX[0]] == -1)
            {

                for (i = 0; i <= len; i++)
                {
                    bugX[i] = bugX[i + 1];
                    bugY[i] = bugY[i + 1];
                }
            }
           
            for (i = len; i >= 0; i--)
            {

                game_pan[bugY[i], bugX[i]] = 0;

            }
            
        }
        void MovingBug2p()
        {
            int i;
            for (i = len_; i >= 0; i--)
            {
                if (i != 0)
                {
                    bugXX[i] = bugXX[i - 1];
                    bugYY[i] = bugYY[i - 1];
                }
                game_pan[bugYY[i], bugXX[i]] = 4;
            }
            if (xxDir == 1)
            {
                bugXX[0]++;
            }
            else if (xxDir == -1)
            {
                bugXX[0]--;
            }
            else if (yyDir == 1)
            {
                bugYY[0]++;
            }
            else if (yyDir == -1)
            {
                bugYY[0]--;
            }
            if (game_pan[bugYY[0], bugXX[0]] == 2)
            {
                game_pan[bugYY[0], bugXX[0]] = 0;
                len_++;
            }
            if (game_pan[bugYY[0], bugXX[0]] == 4 || game_pan[bugYY[0], bugXX[0]] == -1)
            {
                for (i = 0; i <= len_; i++)
                {
                    bugXX[i] = bugXX[i + 1];
                    bugYY[i] = bugYY[i + 1];
                }
            }
            for (i = len_; i >= 0; i--)
            {
                game_pan[bugYY[i], bugXX[i]] = 0;

            }
        }
    }
}

