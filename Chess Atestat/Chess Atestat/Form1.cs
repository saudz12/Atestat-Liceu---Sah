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

namespace Chess_Atestat
{   
    public partial class Form1 : Form
    {
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
            deschidere_conexiune();
            panelLogare.Visible = false;
            panelP2.Visible = false;
            panelP1.Visible = false;
            Timer t1 = new Timer();
            t1.Interval = 1000;
            t1.Tick += new EventHandler(t1_tick);
            t1.Start();
            pictureBox1.Visible = false;
        }
        public void t1_tick(object sender, EventArgs e)
        {
            GC.Collect();
        }
        public void deschidere_conexiune()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Sah.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
        }
        private void buttonIesire_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        FormJoc chessGame;
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            panelLogare.Visible = true; //panelLogare.Enabled = true;
            buttonIesire.Visible = false; //buttonIesire.Enabled = false;
        }
        FormSettings settings;
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            settings = new FormSettings();
            this.Hide();
            settings.ShowDialog();
            this.Show();
        }
        int stateL1 = 0, stateL2 = 0;
        private void buttonInapoi_Click(object sender, EventArgs e)
        {
            textBoxP1.Clear(); textBoxP2.Clear(); textBoxU1.Clear(); textBoxU2.Clear();
            panelLogare.Visible = false; //panelLogare.Enabled = true;
            buttonIesire.Visible = true; //buttonIesire.Enabled = false;
        }
        private void buttonL1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE NumeUtilizator = @Nume AND ParolaUtilizator = @Parola", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU1.Text.ToString());
            cmd.Parameters.AddWithValue("@Parola", textBoxP1.Text.ToString());
            var x = cmd.ExecuteScalar();
            if (x == null)
            {
                MessageBox.Show("Wrong username/password!");
                return;
            }
            if(stateL2 == 1)
            {
                if(textBoxU1.Text == textBoxU2.Text)
                {
                    MessageBox.Show("User already logged in!");
                    return;
                }
            }
            stateL1 = 1;
            panelP1.Visible = true;
            label2.Visible = false;
            //panel2.Visible = false;
            labelU1.Text = textBoxU1.Text;
            cmd = new SqlCommand($"SELECT NrJocuri FROM Utilizatori WHERE NumeUtilizator = @Nume", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU1.Text.ToString());
            double nr_jocuri = Convert.ToDouble(cmd.ExecuteScalar());
            cmd = new SqlCommand($"SELECT NrWinuri FROM Utilizatori WHERE NumeUtilizator = @Nume", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU1.Text.ToString());
            double nr_winuri = Convert.ToDouble(cmd.ExecuteScalar());
            double winrate;
            if (nr_jocuri != 0)
                winrate = Math.Round(nr_winuri / (nr_jocuri / 100), 2);
            else
                winrate = 0;
            labelI1.Text = $"Games Played: {nr_jocuri}\nGames Won: {nr_winuri}\nWinrate: {winrate}";
        }
        private void buttonL2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT IdUtilizator FROM Utilizatori WHERE NumeUtilizator = @Nume AND ParolaUtilizator = @Parola", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU2.Text.ToString());
            cmd.Parameters.AddWithValue("@Parola", textBoxP2.Text.ToString());
            var x = cmd.ExecuteScalar();
            if (x == null)
            {
                MessageBox.Show("Wrong username/password!");
                return;
            }
            if (stateL1 == 1)
            {
                if (textBoxU1.Text == textBoxU2.Text)
                {
                    MessageBox.Show("User already logged in!");
                    return;
                }
            }
            stateL2 = 1;
            //panel3.Visible = false;
            panelP2.Visible = true;
            label3.Visible = false;
            labelU2.Text = textBoxU2.Text;
            cmd = new SqlCommand($"SELECT NrJocuri FROM Utilizatori WHERE NumeUtilizator = @Nume", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU2.Text.ToString());
            double nr_jocuri = Convert.ToDouble(cmd.ExecuteScalar());
            cmd = new SqlCommand($"SELECT NrWinuri FROM Utilizatori WHERE NumeUtilizator = @Nume", conn);
            cmd.Parameters.AddWithValue("@Nume", textBoxU2.Text.ToString());
            double nr_winuri = Convert.ToDouble(cmd.ExecuteScalar());
            double winrate;
            if (nr_jocuri != 0)
                winrate = Math.Round(nr_winuri / (nr_jocuri / 100), 2);
            else
                winrate = 0;
            labelI2.Text = $"Games Played: {nr_jocuri}\nGames Won: {nr_winuri}\nWinrate: {winrate}%";
        }
        FormJoc chess;
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if(stateL1 != 1 || stateL2 != 1)
            {
                MessageBox.Show("2 users have to be logged in before starting a match!");
                return;
            }
            chess = new FormJoc();
            this.Hide();
            chess.ShowDialog();
            this.Show();
        }
        private void buttonLOP2_Click(object sender, EventArgs e)
        {
            stateL2 = 0;
            panelP2.Visible = false;
            label3.Visible = true;
        }
        private void buttonLOP1_Click(object sender, EventArgs e)
        {
            stateL1 = 0;
            panelP1.Visible = false;
            label2.Visible = true;
        }
        FormInregistrare inregistrare;
        private void buttonCreeare_Click(object sender, EventArgs e)
        {
            inregistrare = new FormInregistrare();
            this.Hide();
            inregistrare.ShowDialog();
            this.Show();
        }
        public void button_MouseEnterLeave(object sender, EventArgs e)
        {
            Button b1 = (Button)sender;
            if (b1.BackColor == Color.DarkCyan) b1.BackColor = Color.LightBlue;
            else if(b1.BackColor == Color.LightBlue) b1.BackColor = Color.DarkCyan;
            else if(b1.BackColor == Color.ForestGreen) b1.BackColor = Color.LightGreen;
            else if(b1.BackColor == Color.LightGreen) b1.BackColor = Color.ForestGreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chess = new FormJoc();
            this.Hide();
            chess.ShowDialog();
            this.Show();
        }
    }
}
