namespace Chess_Atestat
{
    partial class FormInregistrare
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSignUp = new System.Windows.Forms.Button();
            this.textBoxNume = new System.Windows.Forms.TextBox();
            this.textBoxParola = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxView = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Creeate an account:";
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.DarkCyan;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBack.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.Location = new System.Drawing.Point(403, 244);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(88, 38);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            this.buttonBack.MouseEnter += new System.EventHandler(this.buttonBack_MouseEnter);
            this.buttonBack.MouseLeave += new System.EventHandler(this.buttonBack_MouseLeave);
            // 
            // buttonSignUp
            // 
            this.buttonSignUp.BackColor = System.Drawing.Color.DarkCyan;
            this.buttonSignUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSignUp.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSignUp.Location = new System.Drawing.Point(297, 244);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(90, 38);
            this.buttonSignUp.TabIndex = 2;
            this.buttonSignUp.Text = "Sing up";
            this.buttonSignUp.UseVisualStyleBackColor = false;
            this.buttonSignUp.Click += new System.EventHandler(this.buttonSignUp_Click);
            this.buttonSignUp.MouseEnter += new System.EventHandler(this.buttonSignUp_MouseEnter);
            this.buttonSignUp.MouseLeave += new System.EventHandler(this.buttonSignUp_MouseLeave);
            // 
            // textBoxNume
            // 
            this.textBoxNume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNume.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.textBoxNume.Location = new System.Drawing.Point(264, 74);
            this.textBoxNume.Name = "textBoxNume";
            this.textBoxNume.Size = new System.Drawing.Size(227, 32);
            this.textBoxNume.TabIndex = 3;
            // 
            // textBoxParola
            // 
            this.textBoxParola.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxParola.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.textBoxParola.Location = new System.Drawing.Point(264, 164);
            this.textBoxParola.Name = "textBoxParola";
            this.textBoxParola.PasswordChar = '*';
            this.textBoxParola.Size = new System.Drawing.Size(227, 32);
            this.textBoxParola.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(312, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 35);
            this.label2.TabIndex = 5;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(312, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 35);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password ";
            // 
            // checkBoxView
            // 
            this.checkBoxView.AutoSize = true;
            this.checkBoxView.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxView.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxView.ForeColor = System.Drawing.Color.White;
            this.checkBoxView.Location = new System.Drawing.Point(389, 202);
            this.checkBoxView.Name = "checkBoxView";
            this.checkBoxView.Size = new System.Drawing.Size(102, 19);
            this.checkBoxView.TabIndex = 7;
            this.checkBoxView.Text = "View Password";
            this.checkBoxView.UseVisualStyleBackColor = false;
            this.checkBoxView.CheckedChanged += new System.EventHandler(this.checkBoxView_CheckedChanged);
            // 
            // FormInregistrare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Chess_Atestat.Properties.Resources._27319_746c2259_668x375o_c6cf8543e2d5;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(503, 294);
            this.Controls.Add(this.checkBoxView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxParola);
            this.Controls.Add(this.textBoxNume);
            this.Controls.Add(this.buttonSignUp);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.label1);
            this.Name = "FormInregistrare";
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSignUp;
        private System.Windows.Forms.TextBox textBoxNume;
        private System.Windows.Forms.TextBox textBoxParola;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxView;
    }
}