using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird
{
    public partial class FlappyBird : Form
    {
        int pipeSpeed = 8;
        double gravity = 15;
        int score = 0;
        int speedTimer = 0;
        bool pipeLock = true;
        double speed = 1;
        bool gameHasEnded = false;


        public FlappyBird()
        {
            InitializeComponent();
        }

        private void pipeBottom_Click(object sender, EventArgs e)
        {

        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            int ph = 0;
            bird.Top += (int)gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            pipeLock = true;

            gameScore.Text = "Score: " + score + ", MP: " + MousePosition.X + "," + MousePosition.Y + ", Pipe Height: " + ph;

            if (pipeTop.Left < -150)
            {
                Random r = new Random();
                int num = r.Next(600, 750);
                pipeTop.Left = num;
                ph = num;
                score++;
            }

            if (pipeBottom.Left < -150)
            {
                Random r = new Random();
                int num = r.Next(600, 750);
                pipeBottom.Left = num;
                ph = num;
                score++;
                pipeLock = false;
            }

            if(pipeBottom.Left > 537 && pipeBottom.Left > 537)
            {
                changePipeHeight(pipeLock);
            }

            if(bird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                bird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                bird.Bounds.IntersectsWith(Ground.Bounds) ||
                bird.Top <= 0)
            {
                gameOver();
            }

            speedTimer++;
            if(speedTimer > 400)
            {
                pipeSpeed += 2;
                speedTimer = 0;
            }
            if(score > 30)
            {
                speed = 3;
            }
            if(score > 60)
            {
                speed = 5;
            }
        }
        
        public void gameOver()
        {
            gameOverLabel.Visible = true;
            bigScoreLabel.Text = "Score: " + score;
            bigScoreLabel.Visible = true;
            restartLabel.Visible = true;
            gameHasEnded = true;
            gameTimer.Stop();
        }
        public void changePipeHeight(bool pipeLock)
        {
            if (pipeLock == false)
            {
                Random r = new Random();
                int num = r.Next(0, 3);

                if (num == 0) // middle
                {
                    pipeTop.Top = -150;
                    pipeBottom.Top = 330;
                }

                if (num == 1) // higher
                {
                    pipeTop.Top = -270;
                    pipeBottom.Top = 200;
                }

                if (num == 2) // lower
                {
                    pipeTop.Top = -210;
                    pipeBottom.Top = 270;
                }
                pipeLock = true;
            }
        }

        private void gameKeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            { 
                gravity = -11  + ( -1 * speed);
            }

            if(e.KeyCode == Keys.Enter && gameHasEnded)
            {
                Application.Restart();
            }
        }

        private void gameKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 11 + speed;
            }
        }

        private void FlappyBird_Load(object sender, EventArgs e)
        {
            pipeTop.Top = -190;
            pipeBottom.Top = 330;
            bigScoreLabel.Visible = false;
            gameOverLabel.Visible = false;
            restartLabel.Visible = false;
            gameHasEnded = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
