using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data;


namespace SnakeProject_WFA
{
    public partial class Form1 : Form
    {
        private List <Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        private Circle badfood = new Circle();
        private Circle goldfood = new Circle();

        private int applesEaten = 0;

        int maxWidth;
        int minWidth;
        int maxHeight;
        int minHeight;
        int score;
        int highScore;
        int highScoreFacile;
        int highScoreNormal;
        int highScoreDifficile;

        

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
            if (e.KeyCode == Keys.Down && Settings.direction != "up")
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

                    if (Snake[i].X == badfood.X && Snake[i].Y == badfood.Y)
                    {
                        EatBadApple();
                    }
                    if (Snake[i].X == goldfood.X && Snake[i].Y == goldfood.Y)
                    {
                        EatGoldApple();
                    }

                    for (int j = 1; j < Snake.Count; j++)
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

            Image snakeBody = null;
            Image snakeHead = null;

            Image foodImage = Properties.Resources.apple;
            Image badfoodImage = Properties.Resources.apple_bad;
            Image goldfoodImage = Properties.Resources.apple_golden;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i > 0)
                {
                    switch (Settings.direction)
                    {
                        case "left":
                        case "right":

                            snakeBody = Properties.Resources.snake_body_right_or_left;
                            break;
                        case "up":
                        case "down":
                            snakeBody = Properties.Resources.snake_body_up_or_down;
                            break;
                    }

                    canvas.DrawImage(snakeBody, new Rectangle
                        (
                        Snake[i].X * Settings.Width,
                        Snake[i].Y * Settings.Height,
                        Settings.Width, Settings.Height
                        ));
                }

                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case "left":
                            snakeHead = Properties.Resources.snake_head_left;
                            break;
                        case "right":
                            snakeHead = Properties.Resources.snake_head_right;
                            break;
                        case "down":
                            snakeHead = Properties.Resources.snake_head_down;
                            break;
                        case "up":
                            snakeHead = Properties.Resources.snake_head_up;
                            break;
                    }

                    canvas.DrawImage(snakeHead, new Rectangle
                        (
                        Snake[i].X * Settings.Width,
                        Snake[i].Y * Settings.Height,
                        Settings.Width, Settings.Height
                        ));
                }

                canvas.DrawImage(foodImage, new Rectangle
                    (
                    food.X * Settings.Width,
                    food.Y * Settings.Height,
                    Settings.Width, Settings.Height
                    ));
                canvas.DrawImage(badfoodImage, new Rectangle
                    (
                    badfood.X * Settings.Width,
                    badfood.Y * Settings.Height,
                    Settings.Width, Settings.Height
                    ));
                canvas.DrawImage(goldfoodImage, new Rectangle
                    (
                    goldfood.X * Settings.Width,
                    goldfood.Y * Settings.Height,
                    Settings.Width, Settings.Height
                    ));


            }
    }

        private void RestartGame()
        {
            maxWidth= picCanvas.Width / Settings.Width - 1;
            maxHeight= picCanvas.Height / Settings.Height - 1;

            Snake.Clear();
            DifficultyGame.Enabled = false;
            startButton.Enabled = false;
            score = 0;
            txtScore.Text = "Score : " + score;

            
            Circle head = new Circle { X = 10, Y= 5 };
            Snake.Add(head);
            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            badfood = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            goldfood = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            gameTimer.Start();
        }
        private void EatBadApple()
        {
            int pointsToRemove =  5;

            if (score >= pointsToRemove)
            {
                score -= pointsToRemove;
            }

            else

            {
                score = 0;
            }

            if (Snake.Count < pointsToRemove) 
            {
                GameOver();
            }
            else
            {
                Snake.RemoveRange(Snake.Count - 5, 5);
            }

            txtScore.Text = "Score : " + score;

            badfood.X = -1; 
            badfood.Y = -1;

        }
        private void EatGoldApple()
        {
            score += 5;
            txtScore.Text = "Score : " + score;

            for (int i = 0; i < 5; i++)
            {
                Circle body = new Circle
                {
                    X = Snake[Snake.Count - 1].X,
                    Y = Snake[Snake.Count - 1].Y
                };

                Snake.Add(body);
            }

            goldfood.X = -1;
            goldfood.Y = -1;

        }

        private void EatFood()
        {
            score += 1;
            txtScore.Text = "Score : " + score;

            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            Snake.Add(body);
            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            if (applesEaten % 3 == 0)
            {
                badfood = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            }
            if (applesEaten % 5 == 0)
            {
                goldfood = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            }

            applesEaten++;

        }

        private void GameOver()
        {
            gameTimer.Stop();
            startButton.Enabled = true;
            DifficultyGame.Enabled = true;
            if (DifficultyGame.SelectedIndex == 0 && score > highScoreFacile)
            {
                highScore = score;
                highScoreFacile = highScore;


                txtHighScore.Text = "High score (facile) : " + Environment.NewLine + highScoreFacile;
                txtHighScore.ForeColor = Color.Green;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }
            if (DifficultyGame.SelectedIndex == 1 && score > highScoreNormal)
            {
                highScore = score;
                highScoreNormal = highScore;


                txtHighScore.Text = "High score (normal) : " + Environment.NewLine + highScoreNormal;
                txtHighScore.ForeColor = Color.Green;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }
            if (DifficultyGame.SelectedIndex == 2 && score > highScoreDifficile)
            {
                highScore = score;
                highScoreDifficile = highScore;


                txtHighScore.Text = "High score (hardcore) : " + Environment.NewLine + highScoreDifficile;
                txtHighScore.ForeColor = Color.Green;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }

        }
        private void Difficulty()
        {
            
            if (DifficultyGame.SelectedIndex == 0) 
            {
               gameTimer.Interval = 50;
                txtHighScore.Text = "High score (facile) : " + Environment.NewLine + highScoreFacile;
                txtHighScore.ForeColor = Color.Black;
            }
            if (DifficultyGame.SelectedIndex == 1)
            {
                gameTimer.Interval = 30;
                txtHighScore.Text = "High score (normal) : " + Environment.NewLine + highScoreNormal;
                txtHighScore.ForeColor = Color.Black;
            }
            if (DifficultyGame.SelectedIndex == 2)
            {
                gameTimer.Interval = 15;
                txtHighScore.Text = "High score (difficile) : " + Environment.NewLine + highScoreDifficile;
                txtHighScore.ForeColor = Color.Black;
            }
        }

    }
}