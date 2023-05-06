using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Chess_Atestat
{
    public partial class FormSettings : Form
    {
        SqlConnection conn;
        public FormSettings()
        {
            InitializeComponent();
            deschidere_conexiune();
            panelUser.Visible = false;
        }
        public void deschidere_conexiune()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Sah.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(textBoxUser.Text == "" || textBoxParola.Text == "")
            {
                MessageBox.Show("Please enter an username/password!");
                return;
            }
            SqlCommand cmd = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE NumeUtilizator = @Nume AND ParolaUtilizator = @Parola", conn);
            cmd.Parameters.AddWithValue("@Nume",textBoxUser.Text.ToString());
            cmd.Parameters.AddWithValue("@Parola",textBoxParola.Text.ToString());
            var x = cmd.ExecuteScalar();
            if(x == null)
            {
                MessageBox.Show("Invalid username/password");
                textBoxParola.Clear();
                textBoxUser.Clear();
                return;
            }
            panelLogin.Visible = false;
            panelUser.Visible = true;
        }
        private void checkBoxView_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxView.Checked == true) textBoxParola.PasswordChar = '\0';
            else textBoxParola.PasswordChar = '*';
        }
        public void button_MouseEnterLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Button b1 = (System.Windows.Forms.Button)sender;
            if(b1.BackColor == Color.DarkCyan) b1.BackColor = Color.LightBlue;
            else b1.BackColor = Color.DarkCyan;
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            panelUser.Visible = false;
            panelLogin.Visible = true;
        }

       
    }
}
