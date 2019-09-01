using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Stage
{
    public abstract class StageBase
    {
        /// <summary>
        /// 描画タイマーを利用して時間に応じたゲームの進行を行う
        /// </summary>
        protected uint TickCounter;

        protected Queue<StageWave> waveQueu = new Queue<StageWave>();

        /// <summary>
        /// TickCounterをインターバル分だけ進める
        /// </summary>
        public void NextTick()
        {
            TickCounter += 5;
        }

        /// <summary>
        /// 1Tick=5msを秒に変換する
        /// </summary>
        /// <returns></returns>
        protected uint GetSecond()
        {
            return TickCounter % 200;
        }

        /// <summary>
        /// WaveQueuにWaveを加える
        /// </summary>
        /// <param name="wave"></param>
        protected void Enqueue(StageWave wave)
        {
            waveQueu.Enqueue(wave);
        }

        /// <summary>
        /// ステージを開始する
        /// </summary>
        public async Task PlayStageAsync()
        {
            while(waveQueu.Count > 0)
            {
                var wave = waveQueu.Dequeue();
                // 時間が来るまで待機する
                while(GetSecond() < wave.timing)
                {
                    await Task.Delay(100);
                }

                // Waveの中の処理を実行する
                wave.action();
            }
        }
    }
}
