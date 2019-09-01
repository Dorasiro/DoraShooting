using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Actor
{
    public abstract class PlayerBase : ActorBase
    {
        /// <summary>
        /// 移動速度
        /// </summary>
        public int MoveSpeed
        {
            get; protected set;
        }

        /// <summary>
        /// 射撃間隔を制御する
        /// </summary>
        protected uint shotTimer;

        /// <summary>
        /// 画面下側中央に初期配置するコンストラクタ
        /// </summary>
        protected PlayerBase(int moveSpeed) : base(0, 0, 15)
        {
            isPlayer = true;

            X = Form1.GameAreaWidth / 2;
            Y = Form1.GameAreaHeight-size/2;

            MoveSpeed = moveSpeed;
        }

        public void SetX(int x)
        {
            this.X = x;
        }

        public void SetY(int y)
        {
            this.Y = y;
        }

        /// <summary>
        /// プレイヤーがショットボタンを操作している間、Tickごとに呼び出され、必要に応じて弾を出現させる
        /// </summary>
        public abstract void Shot();
    }
}
