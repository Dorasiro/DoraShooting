using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoraShooting
{
    /// <summary>
    /// 汎用的に使用可能な関数を集めたクラス
    /// </summary>
    class Utils
    {
        /// <summary>
        /// 時計盤の数字で表した向きを角度に変換する
        /// </summary>
        /// <param name="clockDirection"></param>
        /// <returns></returns>
        public static int ParseToDirection(int clockDirection)
        {
            var direction = 0;

            if (!(clockDirection >= 0 && clockDirection <= 12))
            {
                throw new ArgumentException("clockDirectionは0から12の範囲である必要があります");
            }

            switch (clockDirection)
            {
                case 0:
                    direction = 270;
                    break;

                case 1:
                    direction = 300;
                    break;

                case 2:
                    direction = 330;
                    break;

                case 3:
                    direction = 0;
                    break;

                case 4:
                    direction = 30;
                    break;

                case 5:
                    direction = 60;
                    break;

                case 6:
                    direction = 90;
                    break;

                case 7:
                    direction = 120;
                    break;

                case 8:
                    direction = 150;
                    break;

                case 9:
                    direction = 180;
                    break;

                case 10:
                    direction = 210;
                    break;

                case 11:
                    direction = 120;
                    break;

                case 12:
                    direction = 270;
                    break;
            }

            return direction;
        }

        /// <summary>
        /// 時計盤の数字で表した向きを角度に変換する
        /// </summary>
        /// <param name="clockDirection"></param>
        /// <returns></returns>
        public static int ParseToClockDirection(int direction)
        {
            var clockDirection = 0;

            if (!(direction >= 0 && direction <= 360))
            {
                throw new ArgumentException("clockDirectionは0から12の範囲である必要があります");
            }

            switch (direction)
            {
                case 180:
                    clockDirection = 9;
                    break;

                case 150:
                    clockDirection = 8;
                    break;

                case 120:
                    clockDirection = 7;
                    break;

                case 90:
                    clockDirection = 6;
                    break;

                case 60:
                    clockDirection = 5;
                    break;

                case 30:
                    clockDirection = 4;
                    break;

                case 360:
                    clockDirection = 3;
                    break;

                case 330:
                    clockDirection = 2;
                    break;

                case 300:
                    clockDirection = 1;
                    break;

                case 270:
                    clockDirection = 0;
                    break;

                case 240:
                    clockDirection = 11;
                    break;

                case 210:
                    clockDirection = 10;
                    break;

                case 0:
                    clockDirection = 3;
                    break;
            }

            return clockDirection;
        }
    }
}
