using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Shoots
{
    class PlayerLaser : ShotBase
    {
        public PlayerLaser(int playerX, int playerY) : base(true, 100, 360, Brushes.LightGreen, 70, 500)
        {
            playerX -= SizeX/3-3;
            playerY -= 405;
            X = playerX;
            Y = playerY;
        }

        public override void Tick()
        {
            Y -= Speed;

            this.IsOnGameArea();
        }

        public override void Paint(Graphics g)
        {
            g.FillEllipse(ShotBrush, X, Y, SizeX, SizeY);
        }

        public override ShotBase Clone()
        {
            throw new NotImplementedException();
        }
    }
}
