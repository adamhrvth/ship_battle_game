using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shipbattle_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ships Ally;
        ships Enemy;
        double dProjectileX, dProjectileY, dVx, dVy;
        Rectangle Tank, Projectile;
        SolidBrush tankBrush = new SolidBrush(Color.Black);
        bool isProjectile = false;
        int scoreCount = 0;

        private void Game()
        {
            Ally = new ships(Color.Red, 20, this.Width - 20, this.Height / 10 * 7);
            Enemy = new ships(Color.Aquamarine, 30, this.Width - 30, this.Height / 10 * 7);
            Ally.NewShip();
            Enemy.NewShip();
            Tank = new Rectangle(this.Width / 2 - 30, this.Height - 30, 60, 30);
            NewProjectile();
            timer1.Enabled = true;
            timer1.Interval = 1;
            isProjectile = false;
       }

        private void NewProjectile()
        {
            int projectileDiameter = 8;
            isProjectile = false;
            dProjectileX = this.Width / 2 - projectileDiameter/2;
            dProjectileY = this.Height - 30 - projectileDiameter;
            Projectile = new Rectangle((int)dProjectileX, (int)dProjectileY, projectileDiameter, projectileDiameter);
            dVx = 0;
            dVy = 0;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Projectile.X>=ClientRectangle.Width-4)
            {
                dVx = -dVx;
            }
            if (Projectile.X<=0)
            {
                dVx = -dVx;
            }
            if (Projectile.Y>=this.Height)
            {
                NewProjectile();
            }
            if (Projectile.Y <= 0)
            {
                NewProjectile();
            }
            if (Enemy.HasCollided(Projectile))
            {
                Enemy = new ships(Color.Aquamarine, 30, this.Width - 30, this.Height / 10 * 7);
                Enemy.NewShip();
                Ally.NewShip();
                NewProjectile();
                isProjectile = false;
                scoreCount++;
            }
            if (Ally.HasCollided(Projectile))
            {
                timer1.Enabled = false;
                MessageBox.Show("You have hit an ally!  Close this window and then press [space] to start new game or press [esc] to exit.", $"Game over! Your score: {scoreCount}.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                scoreCount = 0;
            }

            dProjectileX += dVx;
            dProjectileY += dVy;
            Projectile.X =(int) dProjectileX;
            Projectile.Y =(int) dProjectileY;

            this.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Space)
            {
                Game();
            
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Bounds = Screen.PrimaryScreen.Bounds;
            this.FormBorderStyle = FormBorderStyle.None;
            Game();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isProjectile)
            {
                if (e.Button==MouseButtons.Left)
                {
                    double a, b, c, d, h, t;
                    a = dProjectileX;
                    b = dProjectileY;
                    c = e.X;
                    d = e.Y;
                    h = Math.Sqrt(Math.Pow(c - a, 2) + Math.Pow(d - b, 2));
                    t = h / 25;
                    dVx = (c - a) / t;
                    dVy = (d - b) / t;
                }
                isProjectile = true;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Enemy.Draw(e.Graphics);
            Ally.Draw(e.Graphics);
            e.Graphics.FillRectangle(tankBrush, Tank);
            e.Graphics.FillEllipse(tankBrush, Projectile);
        }
    }
}
