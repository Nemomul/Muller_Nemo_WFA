using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace SnakeProject_WFA
{
    public partial class Form1 : Form
    {
        private List <Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        int maxWidth;
        int minWidth;
        int maxHeight;
        int minHeight;
        int score;
        int highScore;

        Random rand = new Random();

        bool goLeft, goRight, goDown, goUp;



        public Form1()
        {
            InitializeComponent();

            new Settings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && Settings.direction != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Settings.direction != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Settings.direction != "down")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Settings.direction != "Up")
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            RestartGame();
            Difficulty();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            if (goLeft)
            {
                Settings.direction = "left";
            }
            if (goRight)
            {
                Settings.direction = "right";
            }
            if (goDown)
            {
                Settings.direction = "down";
            }
            if (goUp)
            {
                Settings.direction = "up";
            }

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {

                    switch (Settings.direction)
                    {
                        case "left":
                                Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                    }
                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxWidth;
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Snake[i].X = 0;
                    }
                    if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxHeight;
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Snake[i].Y = 0;
                    }


                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }

                    for(int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            GameOver();
                        }
                    }

                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }

            picCanvas.Invalidate();
            



        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.DarkGreen;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].X * Settings.Width,
                    Snake[i].Y * Settings.Height,
                    Settings.Width, Settings.Height
                    
                    ));
            }

            canvas.FillEllipse(Brushes.DarkRed, new Rectangle
                    (
                    food.X * Settings.Width,
                    food.Y * Settings.Height,
                    Settings.Width, Settings.Height
                    ));
        }

        private void RestartGame()
        {
            maxWidth= picCanvas.Width / Settings.Width - 1;
            maxHeight= picCanvas.Height / Settings.Height - 1;

            Snake.Clear();
            DifficultyGame.Enabled = false;
            startButton.Enabled = false;
            score = 0;
            txtScore.Text = "Score" + score;

            Circle head = new Circle { X = 10, Y= 5 };
            Snake.Add(head);

            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            gameTimer.Start();
        }

        private void EatFood()
        {
            score += 1;
            txtScore.Text = "Score: " + score;

            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            Snake.Add(body);
            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
        }

        private void GameOver()
        {
            gameTimer.Stop();
            startButton.Enabled = true;
            DifficultyGame.Enabled = true;
            if (score > highScore)
            {
                highScore = score;

                txtHighScore.Text = "High score: " + Environment.NewLine + highScore;
                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        private void Difficulty()
        {
            if (DifficultyGame.SelectedIndex == 0) 
            {
               gameTimer.Interval = 50;
                
            }
            if (DifficultyGame.SelectedIndex == 1)
            {
                gameTimer.Interval = 30;
                
            }
            if (DifficultyGame.SelectedIndex == 2)
            {
                gameTimer.Interval = 18;
                
            }
        }
    }
}