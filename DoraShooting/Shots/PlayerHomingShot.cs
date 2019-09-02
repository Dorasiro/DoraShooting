using DoraShooting.Enemy;
using DoraShooting.Shoots;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Shots
{
    class PlayerHomingShot : ShotBase
    {
        /// <summary>
        /// 最後に向いていた方向
        /// </summary>
        private double lastRadian;

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

        public PlayerHomingShot() : base(true, 10, 0, Brushes.DarkGray, 20, 20)
        {
        }

        public override void Tick()
        {
            if(GameMaster.EnemyList.Count > 0)
            {
                // 最寄りの敵の座標を調べる
                var enemyListCopy = GameMaster.EnemyList.Where(enemy => enemy.IsDead == false).ToArray();
                var resultArray = new int[enemyListCopy.Length];
                for (int i = 0; i < enemyListCopy.Length; i++)
                {
                    // 生きていて範囲内にいる敵のみが対象
                    if (!enemyListCopy[i].IsDead && X > -10 && Y > -10 && X < Form1.GameAreaWidth - SizeX + 10 && Y < Form1.GameAreaHeight - SizeY + 10)
                    {
                        resultArray[i] = (int)Math.Sqrt((enemyListCopy[i].X - x) * (enemyListCopy[i].X - x) + (enemyListCopy[i].Y - y) - (enemyListCopy[i].Y - y));
                    }
                }
                if(enemyListCopy.Length == 0)
                {
                    if(lastRadian == 0)
                    {
                        // 敵がいないときはまっすぐ飛ぶ
                        Y -= Speed;
                    }
                    else
                    {
                        // 一度でも追尾を始めていた場合は慣性に従って動く
                        X += (int)(Math.Cos(lastRadian) * Speed);
                        Y += (int)(Math.Sin(lastRadian) * Speed);
                    }
                    
                }
                else
                {
                    var target = enemyListCopy[Array.IndexOf(resultArray, resultArray.Min())];

                    // 角度を求める
                    lastRadian = Math.Atan2(target.Y - Y, target.X - X);
                    X += (int)(Math.Cos(lastRadian) * Speed);
                    Y += (int)(Math.Sin(lastRadian) * Speed);
                }
            }
            else
            {
                if (lastRadian == 0)
                {
                    // 敵がいないときはまっすぐ飛ぶ
                    Y -= Speed;
                }
                else
                {
                    // 一度でも追尾を始めていた場合は慣性に従って動く
                    X += (int)(Math.Cos(lastRadian) * Speed);
                    Y += (int)(Math.Sin(lastRadian) * Speed);
                }
            }

            //if ((int)(Math.Sin(lastRadian) * Speed) > X)
            //{
            //    X -= (int)(Math.Cos(lastRadian) * Speed);
            //    canMovePos = 1;
            //}
            //else
            //{
            //    X += (int)(Math.Cos(lastRadian) * Speed);
            //    canMovePos = 2;
            //}

            //if ((Math.Sin(lastRadian) * Speed) > 0)
            //{
            //    Y -= (int)(Math.Sin(lastRadian) * Speed);
            //}
            //else
            //{
            //    Y += (int)(Math.Sin(lastRadian) * Speed);
            //}

            this.IsOnGameArea();
        }

        /// <summary>
        /// 自身がゲームエリアから出た場合、isEnableをfalseにする
        /// </summary>
        protected new void IsOnGameArea()
        {
            // エリア外に出てない弾を動かす
            if (!(X > -SizeX*2 && Y > -SizeY*2 && X < Form1.GameAreaWidth - SizeX*2 && Y < Form1.GameAreaHeight - SizeY*2))
            {
                IsEnable = false;
            }
        }

        public override void Paint(Graphics g)
        {
            if(IsEnable)
            {
                g.FillEllipse(ShotBrush, X, Y, SizeX, SizeY);
            }
        }

        public override ShotBase Clone()
        {
            return new PlayerHomingShot();
        }
    }
}
