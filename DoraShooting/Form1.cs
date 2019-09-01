using DoraShooting.Actor;
using DoraShooting.Enemy;
using DoraShooting.Shoots;
using DoraShooting.Stage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoraShooting
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// ゲームエリアの横幅
        /// </summary>
        public static int GameAreaWidth;
        /// <summary>
        /// ゲームエリアの縦幅
        /// </summary>
        public static int GameAreaHeight;

        // 自機移動用のボタン入力
        private bool up;
        private bool down;
        private bool left;
        private bool right;

        /// <summary>
        /// 自機射撃用のボタン入力
        /// </summary>
        private bool shoot;
        private bool shoot2;

        /// <summary>
        /// 無敵モードのフラグ
        /// </summary>
        private bool debug;

        private StageBase nowStage;

        public Form1()
        {
            InitializeComponent();

            GameAreaWidth = ClientRectangle.Width;
            GameAreaHeight = ClientRectangle.Height;

            nowStage = new SampleStage();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;

            label1.Text = "(" + GameMaster.Player.X + "," + GameMaster.Player.Y + ")";

            var stage = new SampleStage();
            nowStage = stage;

            stage.PreparationStage();
            await nowStage.PlayStageAsync().ConfigureAwait(false);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            nowStage.NextTick();

            // 複数キー入力の場合に単独キー入力の処理を走らせないためのフラグ
            var isMultiKey = false;

            if (up && left)
            {
                isMultiKey = true;

                if(GameMaster.Player.X > GameMaster.Player.Size/2 && GameMaster.Player.Y > GameMaster.Player.Size/2)
                {
                    GameMaster.Player.SetX(GameMaster.Player.X - (int)Math.Sqrt(GameMaster.Player.MoveSpeed*4));
                    GameMaster.Player.SetY(GameMaster.Player.Y - (int)Math.Sqrt(GameMaster.Player.MoveSpeed * 4));
                }
            }
            else if (up && right)
            {
                isMultiKey = true;
               
                if(GameMaster.Player.X < ClientRectangle.Width-GameMaster.Player.Size/2 && GameMaster.Player.Y > GameMaster.Player.Size/2)
                {
                    GameMaster.Player.SetX(GameMaster.Player.X + (int)Math.Sqrt(GameMaster.Player.MoveSpeed * 4));
                    GameMaster.Player.SetY(GameMaster.Player.Y - (int)Math.Sqrt(GameMaster.Player.MoveSpeed * 4));
                }
            }
            else if (down && left)
            {
                isMultiKey = true;
                
                if(GameMaster.Player.X > GameMaster.Player.Size/2 && GameMaster.Player.Y < ClientRectangle.Height-GameMaster.Player.Size/2)
                {
                    GameMaster.Player.SetX(GameMaster.Player.X - (int)Math.Sqrt(GameMaster.Player.MoveSpeed * 4));
                    GameMaster.Player.SetY(GameMaster.Player.Y + (int)Math.Sqrt(GameMaster.Player.MoveSpeed * 4));
                }
            }
            else if (down && right)
            {
                isMultiKey = true;

                if(GameMaster.Player.X < ClientRectangle.Width- GameMaster.Player.Size/2 && GameMaster.Player.Y < ClientRectangle.Height- GameMaster.Player.Size/2)
                {
                    GameMaster.Player.SetX(GameMaster.Player.X + (int)Math.Sqrt(GameMaster.Player.MoveSpeed * 4));
                    GameMaster.Player.SetY(GameMaster.Player.Y + (int)Math.Sqrt(GameMaster.Player.MoveSpeed * 4));
                }
            }
            else if(!isMultiKey && up && GameMaster.Player.Y > GameMaster.Player.Size/2)
            {
                GameMaster.Player.SetY(GameMaster.Player.Y - GameMaster.Player.MoveSpeed);
            }
            else if(!isMultiKey && down && GameMaster.Player.Y < ClientRectangle.Height- GameMaster.Player.Size/2)
            {
                GameMaster.Player.SetY(GameMaster.Player.Y + GameMaster.Player.MoveSpeed);
            }
            else if(!isMultiKey && left && GameMaster.Player.X > GameMaster.Player.Size/2)
            {
                GameMaster.Player.SetX(GameMaster.Player.X - GameMaster.Player.MoveSpeed);
            }
            else if(!isMultiKey && right && GameMaster.Player.X < ClientRectangle.Width- GameMaster.Player.Size/2)
            {
                GameMaster.Player.SetX(GameMaster.Player.X + GameMaster.Player.MoveSpeed);
            }

            if(shoot)
            {
                GameMaster.Player.Shot();
            }

            if(shoot2)
            {
                GameMaster.ShotList.Add(new PlayerLaser(GameMaster.Player.X, GameMaster.Player.Y));
            }

            if(InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label7.Text = "shootList:" + GameMaster.ShotList.Count;
                });
            }
            else
            {
                label7.Text = "shootList:" + GameMaster.ShotList.Count;
            }

            // フィールド上の敵を動かす
            foreach(var enemy in GameMaster.EnemyList.ToArray())
            {
                if (enemy == null)
                {
                    continue;
                }

                enemy.Tick();
            }

            // 不要になったShootのリスト
            var removeShootList = new List<ShotBase>();
            // フィールド上の弾を動かす
            foreach (var shoot in GameMaster.ShotList.ToArray())
            {
                if(shoot == null)
                {
                    continue;
                }

                shoot.Tick();

                // エリア外に出た球を消す
                if (!shoot.IsEnable)
                {
                    removeShootList.Add(shoot);
                }
            }

            foreach(var shot in GameMaster.ShotList.ToArray())
            {
                if(shot == null || shot.IsEnable == false)
                {
                    continue;
                }

                if(GameMaster.Player.IsHit(shot.isPlayer, shot.X, shot.Y, shot.SizeY))
                {
                    shot.IsEnable = false;

                    if(!debug)
                    {
                        Application.Exit();
                    }
                }

                foreach(var enemy in GameMaster.EnemyList.ToArray())
                {
                    if(enemy.IsHit(shot.isPlayer, shot.X, shot.Y, shot.SizeY))
                    {
                        shot.IsEnable = false;
                        enemy.IsDead = true;
                    }
                }
            }

            // 不要になった弾を消す
            foreach(var r in removeShootList)
            {
                GameMaster.ShotList.Remove(r);
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 自機の描画
            e.Graphics.FillEllipse(Brushes.Gold, GameMaster.Player.X- GameMaster.Player.Size / 2, GameMaster.Player.Y- GameMaster.Player.Size / 2, GameMaster.Player.Size, GameMaster.Player.Size);

            // 敵機の描画
            foreach(var enemy in GameMaster.EnemyList.ToArray())
            {
                if(enemy != null)
                {
                    enemy.Paint(e.Graphics);
                }
            }

            // フィールド上の弾の描画
            foreach (var shoot in GameMaster.ShotList.ToArray())
            {
                if(shoot != null)
                {
                    shoot.Paint(e.Graphics);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyData)
            {
                case Keys.Up:
                    up = true;
                    label2.Text = "Up   :T";
                    break;

                case Keys.Down:
                    down = true;
                    debug = true;
                    label3.Text = "Down :T";
                    break;

                case Keys.Left:
                    left = true;
                    label4.Text = "Left :T";
                    break;

                case Keys.Right:
                    right = true;
                    label5.Text = "Right:T";
                    break;

                case Keys.Z:
                    shoot = true;
                    label6.Text = "Shoot:T";
                    break;

                case Keys.X:
                    shoot2 = true;
                    break;
            }

            label1.Text = "(" + GameMaster.Player.X + "," + GameMaster.Player.Y + ")";
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    up = false;
                    label2.Text = "Up   :F";
                    break;

                case Keys.Down:
                    down = false;
                    debug = false;
                    label3.Text = "Down :F";
                    break;

                case Keys.Left:
                    left = false;
                    label4.Text = "Left :F";
                    break;

                case Keys.Right:
                    right = false;
                    label5.Text = "Right:F";
                    break;

                case Keys.Z:
                    shoot = false;
                    label6.Text = "Shoot:F";
                    break;

                case Keys.X:
                    shoot2 = false;
                    break;
            }

            label1.Text = "(" + GameMaster.Player.X + "," + GameMaster.Player.Y + ")";
        }
    }
}
