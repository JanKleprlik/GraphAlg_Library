namespace Pacman
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
            this.infoPanel = new System.Windows.Forms.Panel();
            this.levelPicker = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.Hardmode = new System.Windows.Forms.CheckBox();
            this.liveLabel = new System.Windows.Forms.Label();
            this.lives = new System.Windows.Forms.Label();
            this.score = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.movement = new System.Windows.Forms.Timer(this.components);
            this.purpleTimer = new System.Windows.Forms.Timer(this.components);
            this.blueTimer = new System.Windows.Forms.Timer(this.components);
            this.yellowTimer = new System.Windows.Forms.Timer(this.components);
            this.infoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.infoPanel.Controls.Add(this.levelPicker);
            this.infoPanel.Controls.Add(this.startButton);
            this.infoPanel.Controls.Add(this.Hardmode);
            this.infoPanel.Controls.Add(this.liveLabel);
            this.infoPanel.Controls.Add(this.lives);
            this.infoPanel.Controls.Add(this.score);
            this.infoPanel.Controls.Add(this.scoreLabel);
            this.infoPanel.Location = new System.Drawing.Point(0, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(630, 30);
            this.infoPanel.TabIndex = 1;
            // 
            // levelPicker
            // 
            this.levelPicker.FormattingEnabled = true;
            this.levelPicker.Items.AddRange(new object[] {
            "level 1",
            "level 2",
            "level 3",
            "level 4"});
            this.levelPicker.Location = new System.Drawing.Point(354, 5);
            this.levelPicker.Name = "levelPicker";
            this.levelPicker.Size = new System.Drawing.Size(92, 21);
            this.levelPicker.TabIndex = 8;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(543, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Hardmode
            // 
            this.Hardmode.AutoSize = true;
            this.Hardmode.Location = new System.Drawing.Point(452, 7);
            this.Hardmode.Name = "Hardmode";
            this.Hardmode.Size = new System.Drawing.Size(75, 17);
            this.Hardmode.TabIndex = 4;
            this.Hardmode.Text = "Hardmode";
            this.Hardmode.UseVisualStyleBackColor = true;
            // 
            // liveLabel
            // 
            this.liveLabel.AutoSize = true;
            this.liveLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.liveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.liveLabel.Location = new System.Drawing.Point(211, 0);
            this.liveLabel.Name = "liveLabel";
            this.liveLabel.Size = new System.Drawing.Size(75, 29);
            this.liveLabel.TabIndex = 2;
            this.liveLabel.Text = "Lives:";
            // 
            // lives
            // 
            this.lives.AutoSize = true;
            this.lives.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lives.Location = new System.Drawing.Point(292, 0);
            this.lives.Name = "lives";
            this.lives.Size = new System.Drawing.Size(37, 29);
            this.lives.TabIndex = 2;
            this.lives.Text = "---";
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.score.Location = new System.Drawing.Point(92, 0);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(37, 29);
            this.score.TabIndex = 1;
            this.score.Text = "---";
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.scoreLabel.Location = new System.Drawing.Point(3, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(83, 29);
            this.scoreLabel.TabIndex = 0;
            this.scoreLabel.Text = "Score:";
            // 
            // movement
            // 
            this.movement.Interval = 600;
            this.movement.Tick += new System.EventHandler(this.movement_Tick);
            // 
            // purpleTimer
            // 
            this.purpleTimer.Interval = 3000;
            this.purpleTimer.Tick += new System.EventHandler(this.purpleTimer_Tick);
            // 
            // blueTimer
            // 
            this.blueTimer.Interval = 2500;
            this.blueTimer.Tick += new System.EventHandler(this.blueTimer_Tick);
            // 
            // yellowTimer
            // 
            this.yellowTimer.Interval = 3000;
            this.yellowTimer.Tick += new System.EventHandler(this.yellowTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(630, 660);
            this.Controls.Add(this.infoPanel);
            this.Name = "Form1";
            this.Text = "Pacman";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.ComboBox levelPicker;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox Hardmode;
        private System.Windows.Forms.Label liveLabel;
        private System.Windows.Forms.Label lives;
        private System.Windows.Forms.Label score;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Timer movement;
        private System.Windows.Forms.Timer purpleTimer;
        private System.Windows.Forms.Timer blueTimer;
        private System.Windows.Forms.Timer yellowTimer;
    }
}

