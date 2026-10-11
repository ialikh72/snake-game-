namespace snake_game_sem_II
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnnGame = new System.Windows.Forms.Panel();
            this.lblScore = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Pause = new System.Windows.Forms.Button();
            this.Restart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnnGame
            // 
            this.pnnGame.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnnGame.Location = new System.Drawing.Point(124, 29);
            this.pnnGame.Name = "pnnGame";
            this.pnnGame.Size = new System.Drawing.Size(664, 409);
            this.pnnGame.TabIndex = 0;
            this.pnnGame.Paint += new System.Windows.Forms.PaintEventHandler(this.pnnGame_Paint);
            this.pnnGame.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pnnGame_PreviewKeyDown);
            // 
            // lblScore
            // 
            this.lblScore.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblScore.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblScore.Location = new System.Drawing.Point(415, 9);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(84, 25);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "score: 0";
            this.lblScore.Click += new System.EventHandler(this.lblScore_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Pause
            // 
            this.Pause.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Pause.Location = new System.Drawing.Point(29, 71);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(89, 35);
            this.Pause.TabIndex = 1;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = false;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // Restart
            // 
            this.Restart.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Restart.Location = new System.Drawing.Point(29, 140);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(89, 35);
            this.Restart.TabIndex = 2;
            this.Restart.Text = "Restart";
            this.Restart.UseVisualStyleBackColor = false;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.pnnGame);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "snake game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pnnGame_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnnGame;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.Button Restart;
    }
}

