using DoraShooting.Actor.Enemy;
using DoraShooting.Enemy;
using DoraShooting.Shoots;
using DoraShooting.Shots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting.Stage
{
    class SampleStage : StageBase
    {
        /// <summary>
        /// ステージの情報をWaveQueueに登録してプレイ可能な状態にする
        /// </summary>
        public void PreparationStage()
        {
            waveQueu.Enqueue(new StageWave(3, new Action(async () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0 || i == 4)
                    {
                        GameMaster.EnemyList.Add(new LinearMotionEnemy(Form1.GameAreaWidth, 50, 20, 9, 5, 40, new EnemyHomingShot()));
                        await Task.Delay(200);
                    }
                    else
                    {
                        GameMaster.EnemyList.Add(new LinearMotionEnemy(Form1.GameAreaWidth, 50, 20, 9, 5, 30, new EnemyNomalShot()));
                        await Task.Delay(200);
                    }
                }

                await Task.Delay(3000);

                for (int i = 0; i < 5; i++)
                {
                    if (i == 0 || i == 4)
                    {
                        GameMaster.EnemyList.Add(new LinearMotionEnemy(0, 50, 20, 3, 5, 40, new EnemyHomingShot()));
                        await Task.Delay(200);
                    }
                    else
                    {
                        GameMaster.EnemyList.Add(new LinearMotionEnemy(0, 50, 20, 3, 5, 30, new EnemyNomalShot()));
                        await Task.Delay(200);
                    }
                }

                await Task.Delay(3000);

                for (int i = 0; i < 10; i++)
                {
                    GameMaster.EnemyList.Add(new LinearMotionEnemy(100, 0, 20, 6, 5, 15, new EnemyNomalShot(3)));
                    GameMaster.EnemyList.Add(new LinearMotionEnemy(Form1.GameAreaWidth - 100, 0, 20, 6, 5, 30, new EnemyNomalShot(9)));
                    await Task.Delay(400);
                }
            })));
        }
    }
}
