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
using static System.Windows.Forms.AxHost;

namespace Chess_Atestat
{
    public partial class FormPlay : Form
    {
        SqlConnection conn;
        public FormPlay()
        {
            InitializeComponent();
            deschidere_conexiune();
            panelP1.Visible = false;
            panelP2.Visible = false;
        }
        public void deschidere_conexiune()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Sah.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
        }
        FormJoc chess;
        FormInregistrare inregistrare;
        int stateL1 = 0, stateL2 = 0;
        private void buttonCreeare_Click(object sender, EventArgs e)
        {
            inregistrare = new FormInregistrare();
            this.Hide();
            inregistrare.ShowDialog();
            this.Show();
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (stateL1 != 1 || stateL2 != 1)
            {
                MessageBox.Show("2 users have to be logged in before starting a match!");
                return;
            }
            chess = new FormJoc(textBoxU1.Text.ToString(), textBoxU2.Text.ToString());
            this.Hide();
            chess.ShowDialog();
            this.Show();
        }
        private void buttonL1_Click(object sender, EventArgs e)
        {
            if(textBoxU1.Text == "" || textBoxP1.Text == "")
            {
                MessageBox.Show("Invalid username/password!");
                return;
            }
            SqlCommand cmd = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE NumeUtilizator = @Nume AND ParolaUtilizator = @Parola", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU1.Text);
            cmd.Parameters.AddWithValue("@Parola", textBoxP1.Text);
            var x = cmd.ExecuteScalar();
            if(x == null)
            {
                MessageBox.Show("Wrong username/password");
                return;
            }
            cmd = new SqlCommand("SELECT NrJocuri FROM Utilizatori WHERE NumeUtilizator = @Nume", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU1.Text);
            double nrJocuri = Convert.ToDouble(cmd.ExecuteScalar());
            cmd = new SqlCommand("SELECT NrWinuri FROM Utilizatori WHERE NumeUtilizator = @Nume", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU1.Text);
            int nrWinuri = Convert.ToInt32(cmd.ExecuteScalar());
            labelP1.Text = $"{textBoxU1.Text}\n\nGames Played:{nrJocuri}\nGames Won:{nrWinuri}\nWin rate:{Math.Round(nrWinuri*100/nrJocuri, 2)}%";
            panelP1.Visible = true;
            stateL1 = 1;
        }
        private void buttonL2_Click(object sender, EventArgs e)
        {
            if (textBoxU2.Text == "" || textBoxP2.Text == "")
            {
                MessageBox.Show("Invalid username/password!");
                return;
            }
            SqlCommand cmd = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE NumeUtilizator = @Nume AND ParolaUtilizator = @Parola", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU2.Text);
            cmd.Parameters.AddWithValue("@Parola", textBoxP2.Text);
            var x = cmd.ExecuteScalar();
            if (x == null)
            {
                MessageBox.Show("Wrong username/password");
                return;
            }
            cmd = new SqlCommand("SELECT NrJocuri FROM Utilizatori WHERE NumeUtilizator = @Nume", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU2.Text);
            double nrJocuri = Convert.ToDouble(cmd.ExecuteScalar());
            cmd = new SqlCommand("SELECT NrWinuri FROM Utilizatori WHERE NumeUtilizator = @Nume", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU2.Text);
            int nrWinuri = Convert.ToInt32(cmd.ExecuteScalar());
            labelP2.Text = $"{textBoxU2.Text}\n\nGames Played:{nrJocuri}\nGames Won:{nrWinuri}\nWin rate:{Math.Round(nrWinuri*100/nrJocuri, 2)}%";
            panelP2.Visible = true;
            stateL2 = 1;
        }
        private void buttonLO1_Click(object sender, EventArgs e)
        {
            panelP1.Visible = false;
            stateL1 = 0;
        }

        private void buttonLO2_Click(object sender, EventArgs e)
        {
            panelP2.Visible = false;
            stateL2 = 0;
        }

        private void buttonInapoi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void button_MouseEnterLeave(object sender, EventArgs e)
        {
            Button b1 = (Button)sender;
            if (b1.BackColor == Color.DarkCyan) b1.BackColor = Color.LightBlue;
            else if (b1.BackColor == Color.LightBlue) b1.BackColor = Color.DarkCyan;
            else if (b1.BackColor == Color.ForestGreen) b1.BackColor = Color.LightGreen;
            else if (b1.BackColor == Color.LightGreen) b1.BackColor = Color.ForestGreen;
        }
    }
}
