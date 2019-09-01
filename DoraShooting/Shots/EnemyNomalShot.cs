using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Shoots
{
    public class EnemyNomalShot : ShotBase
    {
        int a = 0;

        public EnemyNomalShot() : base(false, 5, 90, Brushes.Green, 20, 20)
        {
        }

        public EnemyNomalShot(int clockDirection) : base(false, 5, 0, Brushes.Chocolate, 20, 20)
        {
            a = clockDirection;
            Direction = Utils.ParseToDirection(clockDirection);
        }

        public override void Tick()
        {
            var rad = Direction * Math.PI / 180;

            x += ((int)(Math.Cos(rad)) * Speed);
            y += ((int)(Math.Sin(rad)) * Speed);

            this.IsOnGameArea();
        }

        public override void Paint(Graphics g)
        {
            g.FillEllipse(ShotBrush, X, Y, SizeX, SizeY);
        }

        public override ShotBase Clone()
        {
            return new EnemyNomalShot(Utils.ParseToClockDirection(Direction));
        }
    }
}
