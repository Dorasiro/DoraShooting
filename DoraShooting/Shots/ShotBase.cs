using DoraShooting.Shots;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Shoots
{
    public abstract class ShotBase
    {
        /// <summary>
        /// 弾が有効かどうか
        /// </summary>
        public bool IsEnable
        {
            get; set;
        }

        /// <summary>
        /// 自分の発射した弾であるか
        /// </summary>
        public bool isPlayer;

        /// <summary>
        /// 弾速
        /// </summary>
        protected int Speed
        {
            get; private set;
        }

        /// <summary>
        /// 弾が進む角度 真上を360とする
        /// </summary>
        protected int Direction
        {
            get; set;
        }

        /// <summary>
        /// 球の色
        /// </summary>
        protected Brush ShotBrush
        {
            get; set;
        }

        /// <summary>
        /// 球の大きさ
        /// </summary>
        protected int SizeX
        {
            get; private set;
        }

        /// <summary>
        /// 球の大きさ
        /// </summary>
        public int SizeY
        {
            get; private set;
        }

        /// <summary>
        /// 座標
        /// </summary>
        protected int x;
        public int X
        {
            get
            {
                return x;
            }

            set
            {
                value += SizeX / 3;
                x = value;
            }
        }

        protected int y;
        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                value += SizeX / 3;
                y = value;
            }
        }

        public ShotBase(bool isPlayer, int speed, int direction, Brush brush, int sizeX, int sizeY)
        {
            this.isPlayer = isPlayer;
            this.Speed = speed;
            this.Direction = direction;
            this.ShotBrush = brush;
            this.SizeX = sizeX;
            this.SizeY = sizeY;

            IsEnable = true;
        }

        /// <summary>
        /// ゲームループごとに実行される弾の挙動
        /// </summary>
        public abstract void Tick();

        /// <summary>
        /// 弾の描画
        /// </summary>
        /// <param name="g"></param>
        public abstract void Paint(Graphics g);

        /// <summary>
        /// 自身がゲームエリアから出た場合、isEnableをfalseにする
        /// </summary>
        protected void IsOnGameArea()
        {
            // エリア外に出てない弾を動かす
            if (!(X > -1000 && Y > -1000 && X < Form1.GameAreaWidth - SizeX + 1000 && Y < Form1.GameAreaHeight - SizeY + 1000))
            {
                IsEnable = false;
            }
        }

        /// <summary>
        /// 弾の軌跡を残すために使用する
        /// Tickごとにこのメソッドを呼び出すことでその位置に動かない弾を出現させる
        /// </summary>
        protected void Trajectory()
        {
            GameMaster.ShotList.Add(new DammyShot(X, Y));
        }

        /// <summary>
        /// 弾の複製を作成する
        /// </summary>
        /// <returns></returns>
        public abstract ShotBase Clone();
    }
}
