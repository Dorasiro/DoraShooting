using DoraShooting.Shoots;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Enemy
{
    public abstract class EnemyBase : ActorBase
    {
        /// <summary>
        /// 死亡フラグ
        /// </summary>
        public bool IsDead
        {
            get; set;
        }

        /// <summary>
        /// 進行速度
        /// </summary>
        protected int speed;

        /// <summary>
        /// 死亡アニメ再生終了後、ゲームから完全に除外された状態
        /// </summary>
        protected bool isEnable = true;

        /// <summary>
        /// 射撃間隔
        /// </summary>
        protected int shotInterval;

        /// <summary>
        /// 射撃するショットの種類
        /// </summary>
        protected ShotBase shot;

        /// <summary>
        /// 1Tickごとに加算されShotIntervalと一致すると射撃が行われる
        /// </summary>
        protected int shotIntervalTimer;

        protected EnemyBase(int x, int y, int size, int shotInterval, ShotBase shot) : base(x, y, size)
        {
            isPlayer = false;
            this.shotInterval = shotInterval;
            this.shot = shot;
        }

        /// <summary>
        /// ゲームループごとの動き
        /// </summary>
        public abstract void Tick();

        /// <summary>
        /// 自身の描画
        /// </summary>
        public abstract void Paint(Graphics g);

        /// <summary>
        /// 開発用の仮死亡アニメーション
        /// </summary>
        protected void DevDeathAnim(Graphics g)
        {
            // 本体から離す距離
            var i = 10;

            // 左
            g.FillEllipse(Brushes.BlueViolet, X - i + Size, Y + Size, 10, 10);
        }

        /// <summary>
        /// 自身がゲームエリアから出た場合、isEnableをfalseにする
        /// </summary>
        protected void IsOnGameArea()
        {
            // エリア外に出てない弾を動かす
            if (!(X > -20 && Y > -20 && X < Form1.GameAreaWidth - Size + 20 && Y < Form1.GameAreaHeight - Size + 20))
            {
                isEnable = false;
                IsDead = true;
            }
        }
    }
}
