using DoraShooting.Shoots;
using DoraShooting.Shots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Actor
{
    class SamplePlayer : PlayerBase
    {
        /// <summary>
        /// 通常弾の射撃間隔
        /// </summary>
        private const int nomalShotInterval = 10;
        /// <summary>
        /// ホーミング弾の射撃間隔
        /// </summary>
        private const int homingShotInterval = 50;

        public SamplePlayer() : base(6)
        {

        }

        public override void Shot()
        {
            if(shotTimer%nomalShotInterval == 0)
            {
                GameMaster.ShotList.Add(new PlayerNomalShot(GameMaster.Player.X, GameMaster.Player.Y));
            }

            if(shotTimer%homingShotInterval == 0)
            {
                var h = new PlayerHomingShot();
                h.X = X;
                h.Y = Y;
                GameMaster.ShotList.Add(h);
            }

            shotTimer++;
        }
    }
}
