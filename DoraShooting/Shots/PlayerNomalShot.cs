using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Shoots
{
    public class PlayerNomalShot : ShotBase
    {
        public PlayerNomalShot(int playerX, int playerY, int clockDirection) : base(true, 20, 0, Brushes.Green, 20, 20)
        {
            X = playerX-SizeX+5;
            Y = playerY-SizeY;
            Direction = Utils.ParseToDirection(clockDirection);
        }

        public PlayerNomalShot(int playerX, int playerY) : base(true, 20, 0, Brushes.Green, 20, 20)
        {
            X = playerX-SizeX+5;
            Y = playerY-SizeY;
        }

        public override void Tick()
        {
            Y -= Speed;

            this.IsOnGameArea();
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
            return new PlayerNomalShot(X, Y, Utils.ParseToClockDirection(Direction));
        }
    }
}
