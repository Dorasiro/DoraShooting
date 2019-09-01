using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting
{
    public abstract class ActorBase
    {
        /// <summary>
        /// プレイヤー側に属するかのフラグ
        /// </summary>
        protected bool isPlayer;

        /// <summary>
        /// 自身のX座標
        /// </summary>
        public int X
        {
            protected set; get;
        }
        /// <summary>
        /// 自身のY座標
        /// </summary>
        public int Y
        {
            protected set; get;
        }

        /// <summary>
        /// 円の当たり判定にするため、その直径をサイズとする
        /// </summary>
        protected int size;
        /// <summary>
        /// アクターのサイズ(直径)
        /// </summary>
        public int Size => size;

        /// <summary>
        /// 座標を初期化するコンストラクタ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected ActorBase(int x, int y, int size)
        {
            this.X = x;
            this.Y = y;
            this.size = size;
        }

        /// <summary>
        /// 自身への当たり判定をチェックする
        /// </summary>
        /// <param name="targetX">チェック対象のx座標</param>
        /// <param name="targetY">チェック対象のy座標</param>
        /// <returns>あたった場合はtrue</returns>
        public bool IsHit(bool isPlayer, int targetX, int targetY, int targetSize)
        {
            // 敵陣営の弾の場合のみ計算する
            if(isPlayer != this.isPlayer)
            {
                if ((X < targetX && Y < targetY) || (X > targetX && Y > targetY))
                {
                    if (size / 2 + targetSize / 2 >= Math.Sqrt(Math.Pow(Math.Max(X, targetX) - Math.Min(X, targetX), 2) + Math.Pow(Math.Max(Y, targetY) - Math.Min(Y, targetY), 2)))
                    {
                        return true;
                    }
                }
                else
                {
                    if (size / 2 + targetSize / 2 >= Math.Sqrt(Math.Pow(Math.Max(X, targetX) - Math.Min(X, targetX), 2) + Math.Pow(Math.Max(Y, targetY) - Math.Min(Y, targetY), 2)))
                    {
                        return true;
                    }
                }

                return false;
            }

            // 味方陣営の弾の場合は当たらない
            return false;
        }
    }
}
