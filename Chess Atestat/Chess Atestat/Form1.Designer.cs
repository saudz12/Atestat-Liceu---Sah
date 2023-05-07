namespace Chess_Atestat
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
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonIesire = new System.Windows.Forms.Button();
            this.buttonHTP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPlay.Font = new System.Drawing.Font("Palatino Linotype", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.Location = new System.Drawing.Point(280, 178);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(184, 60);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            this.buttonPlay.MouseEnter += new System.EventHandler(this.button_MouseEnterLeave);
            this.buttonPlay.MouseLeave += new System.EventHandler(this.button_MouseEnterLeave);
            // 
            // buttonIesire
            // 
            this.buttonIesire.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonIesire.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonIesire.Font = new System.Drawing.Font("Palatino Linotype", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIesire.Location = new System.Drawing.Point(419, 429);
            this.buttonIesire.Name = "buttonIesire";
            this.buttonIesire.Size = new System.Drawing.Size(62, 30);
            this.buttonIesire.TabIndex = 2;
            this.buttonIesire.Text = "Exit";
            this.buttonIesire.UseVisualStyleBackColor = false;
            this.buttonIesire.Click += new System.EventHandler(this.buttonIesire_Click);
            this.buttonIesire.MouseEnter += new System.EventHandler(this.button_MouseEnterLeave);
            this.buttonIesire.MouseLeave += new System.EventHandler(this.button_MouseEnterLeave);
            // 
            // buttonHTP
            // 
            this.buttonHTP.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonHTP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonHTP.Font = new System.Drawing.Font("Palatino Linotype", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHTP.Location = new System.Drawing.Point(280, 257);
            this.buttonHTP.Name = "buttonHTP";
            this.buttonHTP.Size = new System.Drawing.Size(184, 60);
            this.buttonHTP.TabIndex = 3;
            this.buttonHTP.Text = "How To play";
            this.buttonHTP.UseVisualStyleBackColor = false;
            this.buttonHTP.Click += new System.EventHandler(this.buttonHTP_Click);
            this.buttonHTP.MouseEnter += new System.EventHandler(this.button_MouseEnterLeave);
            this.buttonHTP.MouseLeave += new System.EventHandler(this.button_MouseEnterLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 63);
            this.label1.TabIndex = 4;
            this.label1.Text = "Chess";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(12, 174);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 227);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkCyan;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.button1.Location = new System.Drawing.Point(186, 426);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 35);
            this.button1.TabIndex = 13;
            this.button1.Text = "Start game!";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseEnter += new System.EventHandler(this.button_MouseEnterLeave);
            this.button1.MouseLeave += new System.EventHandler(this.button_MouseEnterLeave);
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSettings.Font = new System.Drawing.Font("Palatino Linotype", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSettings.Location = new System.Drawing.Point(280, 341);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(184, 60);
            this.buttonSettings.TabIndex = 1;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            this.buttonSettings.MouseEnter += new System.EventHandler(this.button_MouseEnterLeave);
            this.buttonSettings.MouseLeave += new System.EventHandler(this.button_MouseEnterLeave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Chess_Atestat.Properties.Resources.chess_board;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonHTP);
            this.Controls.Add(this.buttonIesire);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.button1);
            this.MaximumSize = new System.Drawing.Size(500, 500);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "Form1";
            this.Text = "Chess, but worse";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonIesire;
        private System.Windows.Forms.Button buttonHTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSettings;
    }
}

