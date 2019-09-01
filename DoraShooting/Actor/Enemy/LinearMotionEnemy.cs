using DoraShooting.Enemy;
using DoraShooting.Shoots;
using DoraShooting.Shots;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Actor.Enemy
{
    /// <summary>
    /// 一定の方向に動き続ける敵
    /// </summary>
    class LinearMotionEnemy : EnemyBase
    {
        /// <summary>
        /// 進行方向の角度を表す
        /// </summary>
        protected int direction;

        /// <summary>
        /// 通常のコンストラクタ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        public LinearMotionEnemy(int x, int y, int size, int clockDirection, int speed, int shotInterval, ShotBase shot) : base(x, y, size, shotInterval, shot)
        {
            isEnable = true;
            direction = Utils.ParseToDirection(clockDirection);
            this.speed = speed;
        }

        public override void Tick()
        {
            if(isEnable)
            {
                double radian = direction * Math.PI / 180;

                X += ((int)(Math.Cos(radian)) * speed);
                Y += ((int)(Math.Sin(radian)) * speed);

                if(shotIntervalTimer == 0 || shotIntervalTimer % shotInterval == 0)
                {
                    var shotCopy = shot.Clone();
                    shotCopy.X = X;
                    shotCopy.Y = Y;
                    GameMaster.ShotList.Add(shotCopy);
                }

                shotIntervalTimer++;

                this.IsOnGameArea();
            }
        }

        public override void Paint(Graphics g)
        {
            if(!isEnable)
            {
                return;
            }
            else if(IsDead)
            {
                //DevDeathAnim(g);
                isEnable = false;
            }
            else
            {
                g.FillEllipse(Brushes.Blue, X - Size / 2, Y - Size / 2, Size, Size);
            }
        }
    }
}
