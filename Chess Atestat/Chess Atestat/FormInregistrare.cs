﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Chess_Atestat.Properties;

namespace Chess_Atestat
{
    public partial class FormInregistrare : Form
    {
        SqlConnection conn;
        public FormInregistrare()
        {
            InitializeComponent();
            deschidere_conexiune();
        }
        public void deschidere_conexiune()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Sah.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
        }
        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE NumeUtilizator = @Name", conn);
            cmd.Parameters.AddWithValue("@Name", textBoxNume.Text);
            var x = cmd.ExecuteScalar();
            if (textBoxParola.Text == "" || textBoxNume.Text == "")
            {
                MessageBox.Show("Please enter an username/password!");
                return;
            }
            if (x != null)
            {
                MessageBox.Show("Username already used!");
                textBoxNume.Clear();
                textBoxParola.Clear();
                return;
            }
            cmd = new SqlCommand("INSERT INTO Utilizatori VALUES(@Nume, @Parola, 0, 0)", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxNume.Text);
            cmd.Parameters.AddWithValue("@Parola", textBoxParola.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("A new account has been created.");
            textBoxNume.Clear();
            textBoxParola.Clear();
            checkBoxView.Checked = false;
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void button_MouseEnterLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Button b1 = (System.Windows.Forms.Button)sender;
            if (b1.BackColor == Color.DarkCyan) b1.BackColor = Color.LightBlue;
            else b1.BackColor = Color.DarkCyan;
        }
        private void checkBoxView_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxView.Checked == true) textBoxParola.PasswordChar = '\0';
            else textBoxParola.PasswordChar = '*';
        }
    }
}
