namespace SnakeProject_WFA
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.startButton = new System.Windows.Forms.Button();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.txtScore = new System.Windows.Forms.Label();
            this.txtHighScore = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.DifficultyGame = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.Crimson;
            this.startButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startButton.Font = new System.Drawing.Font("Teko SemiBold", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startButton.Location = new System.Drawing.Point(604, 21);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(132, 57);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartGame);
            // 
            // picCanvas
            // 
            this.picCanvas.BackColor = System.Drawing.SystemColors.ControlDark;
            this.picCanvas.Location = new System.Drawing.Point(12, 21);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(580, 680);
            this.picCanvas.TabIndex = 3;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdatePictureBoxGraphics);
            // 
            // txtScore
            // 
            this.txtScore.AutoSize = true;
            this.txtScore.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtScore.Location = new System.Drawing.Point(604, 221);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(89, 23);
            this.txtScore.TabIndex = 4;
            this.txtScore.Text = "Score : 0";
            this.txtScore.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtHighScore
            // 
            this.txtHighScore.AutoSize = true;
            this.txtHighScore.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtHighScore.Location = new System.Drawing.Point(604, 272);
            this.txtHighScore.Name = "txtHighScore";
            this.txtHighScore.Size = new System.Drawing.Size(131, 23);
            this.txtHighScore.TabIndex = 5;
            this.txtHighScore.Text = "HighScore : 0";
            this.txtHighScore.Click += new System.EventHandler(this.label2_Click);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 40;
            this.gameTimer.Tick += new System.EventHandler(this.GameTimerEvent);
            // 
            // DifficultyGame
            // 
            this.DifficultyGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DifficultyGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DifficultyGame.FormattingEnabled = true;
            this.DifficultyGame.Items.AddRange(new object[] {
            "¤¤ Facile ¤¤",
            "¤¤ Normal ¤¤ ",
            "¤¤ HardCore ¤¤"});
            this.DifficultyGame.Location = new System.Drawing.Point(604, 118);
            this.DifficultyGame.Name = "DifficultyGame";
            this.DifficultyGame.Size = new System.Drawing.Size(131, 23);
            this.DifficultyGame.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(748, 725);
            this.Controls.Add(this.DifficultyGame);
            this.Controls.Add(this.txtHighScore);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.picCanvas);
            this.Controls.Add(this.startButton);
            this.Name = "Form1";
            this.Text = "Snake WFA Némo MULLER";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button startButton;
        private PictureBox picCanvas;
        private Label txtScore;
        private Label txtHighScore;
        private System.Windows.Forms.Timer gameTimer;
        private ComboBox DifficultyGame;
    }
}