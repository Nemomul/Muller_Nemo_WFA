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
        // Déclaration des variables et objets
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        private Circle badfood = new Circle();
        private Circle goldfood = new Circle();
        private Circle portaldifficulty = new Circle();

        private int applesEaten = 0;

        // Déclaration des variables de jeu et des indicateurs de direction
        int maxWidth;
        int maxHeight;
        int score;
        int highScore;
        int highScoreFacile;
        int highScoreNormal;
        int highScoreDifficile;



        Random rand = new Random();

        bool goLeft, goRight, goDown, goUp;



        // Constructeur de la classe Form1
        public Form1()
        {
            InitializeComponent();

            new Settings(); // Initialisation des paramètres du jeu

        }

        // Méthode appelée lors du chargement du formulaire
        private void Form1_Load(object sender, EventArgs e)
        {
            DifficultyGame.SelectedIndex = 0; // Sélection de la difficulté par défaut
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Fonction déclenchée lorsqu'une touche est enfoncée
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // Gestion des touches enfoncées pour le contrôle du serpent
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

        // Fonction déclenchée lorsqu'une touche est relâchée
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            // Gestion des touches relâchées pour le contrôle du serpent
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

        // Fonction déclenchée lorsqu'on commence une nouvelle partie
        private void StartGame(object sender, EventArgs e)
        {
            // Initialisation du jeu et choix de la difficulté
            RestartGame();
            Difficulty();

        }

        // Fonction principale du jeu, appelée à chaque intervalle de temps
        private void GameTimerEvent(object sender, EventArgs e)
        {
            // Logique principale du jeu, déplacement du serpent, gestion des collisions, etc.
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
                    // Déplacement du serpent
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
                    // Gestion des collisions avec la nourriture, les pommes mauvaises, les pommes en or et le portail de difficulté
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

                    if (score >= 25 && Snake[i].X >= portaldifficulty.X && Snake[i].X < portaldifficulty.X + portaldifficulty.Width &&
                        Snake[i].Y >= portaldifficulty.Y && Snake[i].Y < portaldifficulty.Y + portaldifficulty.Height)
                    {
                        EatPortalDifficulty();
                    }

                    // Gestion de la collision avec lui-même
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

            picCanvas.Invalidate(); // Rafraîchissement de l'affichage

        }

        // Fonction de mise à jour de l'affichage dans le PictureBox
        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            // Dessin du serpent, de la nourriture et d'autres éléments du jeu
            Graphics canvas = e.Graphics;


            Image snakeHead = null;
            Image snakeNeck = null;

            Image snakeBody = Properties.Resources.bodysnake;
            Image foodImage = Properties.Resources.apple;
            Image badfoodImage = Properties.Resources.apple_bad;
            Image goldfoodImage = Properties.Resources.apple_golden;
            Image portalDifficulty = Properties.Resources.portal;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i > 1)
                {
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
                            snakeHead = Properties.Resources.snakeheadleft;
                            break;
                        case "right":
                            snakeHead = Properties.Resources.snakeheadright;
                            break;
                        case "down":
                            snakeHead = Properties.Resources.snakeheaddown;
                            break;
                        case "up":
                            snakeHead = Properties.Resources.snakehead;
                            break;
                    }

                    canvas.DrawImage(snakeHead, new Rectangle
                        (
                        Snake[i].X * Settings.Width,
                        Snake[i].Y * Settings.Height,
                        Settings.Width, Settings.Height
                        ));
                }
                if (i == 1)
                {
                    switch (Settings.direction)
                    {
                        case "left":
                            snakeNeck = Properties.Resources.snake_neck_right;
                            break;
                        case "right":
                            snakeNeck = Properties.Resources.snake_neck_left;
                            break;
                        case "down":
                            snakeNeck = Properties.Resources.snake_neck_up;
                            break;
                        case "up":
                            snakeNeck = Properties.Resources.snake_neck_down;
                            break;
                    }

                    canvas.DrawImage(snakeNeck, new Rectangle
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

            if (score >= 25)
            {
                canvas.DrawImage(portalDifficulty, new Rectangle
                    (
                    portaldifficulty.X * Settings.Width,
                    portaldifficulty.Y * Settings.Height,
                    portaldifficulty.Width * Settings.Width,
                    portaldifficulty.Height * Settings.Height
                    ));


            }
        }

        // Fonction de redémarrage du jeu
        private void RestartGame()
        {

            // Réinitialisation du jeu, du serpent et de la nourriture
            maxWidth = picCanvas.Width / Settings.Width - 1;
            maxHeight = picCanvas.Height / Settings.Height - 1;

            Snake.Clear();
            DifficultyGame.Enabled = false;
            startButton.Enabled = false;
            score = 0;
            txtScore.Text = "Score : " + score;


            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);
            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
            }

            food = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            badfood = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            goldfood = new Circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };
            portaldifficulty = new Circle { X = rand.Next(2, maxWidth - 2), Y = rand.Next(2, maxHeight - 2), Width = 2, Height = 2 };




            gameTimer.Start();
        }
        // Fonction appelée lorsque le serpent mange une mauvaise pomme
        private void EatBadApple()
        {
            // Gestion de la collision avec une mauvaise pomme
            int pointsToRemove = 5;

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
        // Fonction appelée lorsque le serpent mange une pomme en or
        private void EatGoldApple()
        {
            // Gestion de la collision avec une pomme en or
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

        // Fonction appelée lorsque le serpent mange une pomme normale
        private void EatFood()
        {
            // Gestion de la collision avec une pomme normale
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

        // Fonction appelée lorsque la partie est terminée
        private void GameOver()
        {
            // Gestion de la fin de partie, arrêt du jeu, mise à jour du score, etc.
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
        // Fonction de gestion de la difficulté du jeu
        private void Difficulty()
        {

            // Définition de la difficulté en ajustant l'intervalle de temps du jeu

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
        // Cette méthode est appelée lorsque le serpent touche le portail de difficulté.
        private void EatPortalDifficulty()
        {
            // Vérifie la difficulté actuellement sélectionnée dans le menu déroulant DifficultyGame pour ajuster l'accélération selon la difficulté de base.
            if (DifficultyGame.SelectedIndex == 0)
            {
                gameTimer.Interval = 30;
                portaldifficulty.X = -2;
                portaldifficulty.Y = -2;
            }
            if (DifficultyGame.SelectedIndex == 1)
            {
                gameTimer.Interval = 20;
                portaldifficulty.X = -2;
                portaldifficulty.Y = -2;
            }
            if (DifficultyGame.SelectedIndex == 2)
            {
                gameTimer.Interval = 10;
                portaldifficulty.X = -2;
                portaldifficulty.Y = -2;
            }
        }
    }
}