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
using Chess_Atestat.Properties;

namespace Chess_Atestat
{   
    public partial class Form1 : Form
    {
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
            deschidere_conexiune();
            Timer t1 = new Timer();
            t1.Interval = 1000;
            t1.Tick += new EventHandler(t1_tick);
            t1.Start();
            //pictureBox1.Visible = false;
            buttonSettings.Visible = false;
            //button1.Visible = false;
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
        FormPlay play;
        private async void buttonPlay_Click(object sender, EventArgs e)
        {
            play = new FormPlay();
            this.Hide();
            play.ShowDialog();
            this.Show();
        }
        FormSettings settings;
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            settings = new FormSettings();
            this.Hide();
            settings.ShowDialog();
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

        FormJoc chess;
        private void button1_Click(object sender, EventArgs e)
        {
            chess = new FormJoc("a", "b");
            this.Hide();
            chess.ShowDialog();
            this.Show();
        }
    }
}
