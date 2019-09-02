using DoraShooting.Shoots;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Shots
{
    class EnemyHomingShot : ShotBase
    {
        /// <summary>
        /// 最後に向いて方向
        /// </summary>
        private double lastRadian;

        /// <summary>
        /// 一度でも制御が解かれたらそのまま惰性で進む
        /// </summary>
        private bool canHoming = true;

        /// <summary>
        /// 一度右に動くか左に動くかを決めた後はもう変更できないようにする
        /// </summary>
        private int canMovePos = 0;

        public new int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public new int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public EnemyHomingShot() : base(false, 5, 0, Brushes.Brown, 20, 20)
        {
        }

        public override void Tick()
        {
            // 一定より離れていればホーミング
            if (canHoming && Math.Sqrt((GameMaster.Player.X - X) * (GameMaster.Player.X - X) + (GameMaster.Player.Y - Y) * (GameMaster.Player.Y - Y)) >= 200)
            {
                // 角度を求める
                lastRadian = Math.Atan2(GameMaster.Player.Y - Y, GameMaster.Player.X - X);

                if((int)(Math.Cos(lastRadian) * Speed) > X)
                {
                    if(canMovePos == 0 || canMovePos == 1)
                    {
                        X -= (int)(Math.Cos(lastRadian) * Speed);
                        canMovePos = 1;
                    }
                }
                else
                {
                    if(canMovePos == 0 || canMovePos == 2)
                    {
                        X += (int)(Math.Cos(lastRadian) * Speed);
                        canMovePos = 2;
                    }
                }
                
                if((Math.Sin(lastRadian) * Speed) > 0)
                {
                    Y += (int)(Math.Sin(lastRadian) * Speed);
                }
                else
                {
                    canHoming = false;
                }
            }
            else
            {
                canHoming = false;

                X += (int)(Math.Cos(lastRadian) * Speed);
                Y += (int)(Math.Sin(lastRadian) * Speed);
            }

            this.IsOnGameArea();
        }

        /// <summary>
        /// 自身がゲームエリアから出た場合、isEnableをfalseにする
        /// </summary>
        protected new void IsOnGameArea()
        {
            // エリア外に出てない弾を動かす
            if (!(X > -SizeX && Y > -SizeY && X < Form1.GameAreaWidth - SizeX && Y < Form1.GameAreaHeight - SizeY))
            {
                IsEnable = false;
            }
        }

        public override void Paint(Graphics g)
        {
            g.FillEllipse(ShotBrush, X, Y, SizeX, SizeY);
        }

        public override ShotBase Clone()
        {
            return new EnemyHomingShot();
        }
    }
}
