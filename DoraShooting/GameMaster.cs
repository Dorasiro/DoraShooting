using DoraShooting.Actor;
using DoraShooting.Enemy;
using DoraShooting.Shoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting
{
    /// <summary>
    /// ゲームの進行や状況を管理するクラス
    /// </summary>
    class GameMaster
    {
        /// <summary>
        /// 自機
        /// </summary>
        public static SamplePlayer Player
        {
            get; private set;
        }

        /// <summary>
        /// 敵機のリスト
        /// </summary>
        public static List<EnemyBase> EnemyList
        {
            get; private set;
        }

        /// <summary>
        /// フィールド上の弾のリスト
        /// </summary>
        public static List<ShotBase> ShotList
        {
            get; private set;
        }

        static GameMaster()
        {
            Player = new SamplePlayer();
            EnemyList = new List<EnemyBase>();
            ShotList = new List<ShotBase>();
        }
    }
}
