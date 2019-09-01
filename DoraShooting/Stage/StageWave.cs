using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Stage
{
    public struct StageWave
    {
        /// <summary>
        /// ウェーブを開始する秒数
        /// </summary>
        public uint timing;
        /// <summary>
        /// ウェーブの処理
        /// </summary>
        public Action action;

        public StageWave(uint timing, Action action)
        {
            this.timing = timing;
            this.action = action;
        }
    }
}
