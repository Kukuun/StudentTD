using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentTD {
    public partial class EndScreen : Form {
        public EndScreen() {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            Graphics dc = this.CreateGraphics();
            Font font = new Font("Arial", 55);
            if (GameWorld.WaveKeeper.CurrentWave < 10) {
                dc.DrawString(GameWorld.WaveKeeper.CurrentWave.ToString(), font, Brushes.Red, 125, 60);
            }
            else if (GameWorld.WaveKeeper.CurrentWave < 100 && GameWorld.WaveKeeper.CurrentWave >= 10) {
                dc.DrawString(GameWorld.WaveKeeper.CurrentWave.ToString(), font, Brushes.Red, 105, 60);
            }
            else {
                dc.DrawString(GameWorld.WaveKeeper.CurrentWave.ToString(), font, Brushes.Red, 80, 60);
            }
        }

        private void EndScreen_Load(object sender, EventArgs e) {
        }

        private void btnBackMenu_Click(object sender, EventArgs e) {
            GameForm.SelfGameForm.Close();
            StudentTD.Menu.SelfMenu.Show();
            this.Close();
        }

        private void EndScreen_FormClosed(object sender, FormClosedEventArgs e) {
            GameForm.SelfGameForm.Close();
            StudentTD.Menu.SelfMenu.Show();
        }
    }
}