using DoraShooting.Shoots;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Shots
{
    class DammyShot : ShotBase
    {
        public DammyShot(int x, int y) : base(false, 0, 0, Brushes.Black, 20, 20)
        {
            X = x;
            Y = y;
        }

        public override void Paint(Graphics g)
        {
            g.FillEllipse(ShotBrush, X, Y , SizeX, SizeY);
        }

        public override void Tick()
        {
            // 何もしない
        }

        public override ShotBase Clone()
        {
            return new DammyShot(X, Y);
        }
    }
}
