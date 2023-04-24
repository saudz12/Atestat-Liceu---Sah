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
using System.Runtime.CompilerServices;
using Chess_Atestat.Properties;
using System.Security.Cryptography.X509Certificates;

namespace Chess_Atestat
{
    public partial class FormJoc : Form
    {
        Button[,] chess_board = new Button[9, 9];
        
        SqlConnection conn;
        Timer timerWhite = new Timer();
        Timer timerBlack = new Timer();
        int turn = 1;
        bool whiteChecked = false;
        bool whiteMated = false;
        bool blackChecked = false;
        bool blackMated = false;
        int white_kingI = 7;
        int white_kingJ = 4;
        int black_kingI = 0;
        int black_kingJ = 4;
        public FormJoc()
        {
            InitializeComponent();
            deschidere_conexiune();
            timerWhite.Interval = 1000;
            timerBlack.Interval = 1000;
            set_board();
            
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                    Console.Write(chess_board[i,j].Tag.ToString() + "| ");
                Console.Write('\n');
            }
            
        }
        public void deschidere_conexiune()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Sah.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
        }
        public Color get_backcolor(int i, int j)
        {
            if(i % 2 == 0) {
                if (j % 2 == 0) return Color.Wheat;
                else return Color.SaddleBrown;
            }
            else
            {
                if (j % 2 == 0) return Color.SaddleBrown;
                else return Color.Wheat;
            }
        }
        public string get_piecetype(int i, int j)
        {
            if (i == 1) return "Black Pawn " + (j+1);
            if (i == 6) return "White Pawn " + (j+1);
            if (i == 0 && (j == 0 || j == 7)) return "Black Rook " + ((j == 0)?1:2);
            if (i == 0 && (j == 1 || j == 6)) return "Black Knight " + ((j == 1) ? 1 : 2); 
            if (i == 0 && (j == 2 || j == 5)) return "Black Bishop " + ((j == 2) ? 1 : 2); 
            if (i == 7 && (j == 0 || j == 7)) return "White Rook " + ((j == 0) ? 1 : 2); 
            if (i == 7 && (j == 1 || j == 6)) return "White Knight " + ((j == 1) ? 1 : 2); 
            if (i == 7 && (j == 2 || j == 5)) return "White Bishop " + ((j == 2) ? 1 : 2); 
            if (i == 0 && j == 3) return "Black Queen 1";
            if (i == 0 && j == 4) return "Black King 1";
            if (i == 7 && j == 3) return "White Queen 1";
            if (i == 7 && j == 4) return "White King 1";
            return "Neutral Space " + i + j;
        }
        public Image get_image(int i, int j)
        {
            if (i == 1) return Resources.black_pawn;
            if (i == 6) return Resources.white_pawn; ;
            if (i == 0 && (j == 0 || j == 7)) return Resources.black_rook;
            if (i == 0 && (j == 1 || j == 6)) return Resources.black_knight;
            if (i == 0 && (j == 2 || j == 5)) return Resources.bishop_black;
            if (i == 7 && (j == 0 || j == 7)) return Resources.white_rook;
            if (i == 7 && (j == 1 || j == 6)) return Resources.white_knight;
            if (i == 7 && (j == 2 || j == 5)) return Resources.white_bishop;
            if (i == 0 && j == 3) return Resources.black_queen;
            if (i == 0 && j == 4) return Resources.black_king;
            if (i == 7 && j == 3) return Resources.white_queen;
            if (i == 7 && j == 4) return Resources.white_king;
            return null;
        }

        int is_selected = 0;
        int selectedI, selectedJ;
        string active_side, active_piece;
        bool[,] did_first_move = new bool[3, 10];

        public Point get_point(string tag)
        {
            int i = 0, j = 0;
            bool found = false;
            for (i = 0; i < 8 && !found; i++)
                for (j = 0; j < 8 && !found; j++)
                {
                    ///Console.Write($"{i} {j}|");
                    if (chess_board[i, j].Tag.ToString() == tag)
                        found = true;
                }
            ///Console.WriteLine(i + " " + j);
            return new Point(i, j);
        }
        public void set_activepiece(int i, int j, string tag)
        {
            string[] info = tag.Split(' ');
            string piece = info[1], side = info[0];
            Console.WriteLine(side + " " + piece + " " + info[2]);
            active_piece = piece;
            selectedI = i;
            selectedJ = j;
            active_side = side;
            ///Console.WriteLine($"//////\n{selectedI} {selectedJ} {active_piece} {active_side}\n//////\n");
        }
        bool is_space(int i, int j)
        {
            Console.WriteLine(i+ " " + j);
            if (chess_board[i, j].Tag.ToString().Split(' ')[0] == "Neutral")
                return true;
            return false;
        }
        int get_ord(int i, int j)
        {
            string ord = chess_board[i, j].Tag.ToString().Split(' ')[2];
            Console.WriteLine(Convert.ToInt32(ord) - 1);
            return Convert.ToInt32(ord) - 1;
        }
        bool check_pawn(int i, int j)
        {
            int k = (active_side == "White") ? (-1) : 1, z = (active_side == "White") ? (0) : (1);
            if (i == selectedI + 2 * k && j == selectedJ && is_space(i, j) && did_first_move[z, get_ord(selectedI, selectedJ)] == false)
            {
                did_first_move[z, get_ord(selectedI, selectedJ)] = true;
                return true;
            }
            did_first_move[z, get_ord(selectedI, selectedJ)] = true;
            if (i == selectedI + k && (j == selectedJ - 1 || j == selectedJ + 1) && !is_space(i, j))
                return true;
            if(i != selectedI+k || j != selectedJ) 
                return false;
            if(i == selectedI+k && j == selectedJ && !is_space(i, j))
                return false;
            return true;
        }
        bool check_rook(int i, int j)
        {
            if(i != selectedI && j != selectedJ)
            {
                return false;
            }
            int u = selectedI-1, d = selectedI+1, l = selectedJ-1, r = selectedJ+1;
            if(u > 0) for (; is_space(u, selectedJ) && u > 0; u--);
            if(d < 7) for (; is_space(d, selectedJ) && d < 7; d++);
            if(l > 0) for (; is_space(selectedI, l) && l > 0; l--);
            if(r < 7) for (; is_space(selectedI, r) && r < 7; r++);
            if (i < u || i > d)
            {
                return false;
            }
            if (j < l || j > r)
            {
                return false;
            }
            return true;
        }
        bool check_bishop(int i, int j)
        {
            if (i + j != selectedI + selectedJ && i - j != selectedI - selectedJ)
                return false;
            int TLi = selectedI - 1, TLj = selectedJ - 1;
            int TRi = selectedI - 1, TRj = selectedJ + 1;
            int BLi = selectedI + 1, BLj = selectedJ - 1;
            int BRi = selectedI + 1, BRj = selectedJ + 1;
            if(TLi > 0 && TLj > 0) for (;is_space(TLi, TLj) && TLi > 0 && TLj > 0; TLi--, TLj--);
            if(TRi > 0 && TRj < 7) for (;is_space(TRi, TRj) && TRi > 0 && TRj < 7; TRi--, TRj++);
            if(BLi < 7 && BLj > 0) for (;is_space(BLi, BLj) && BLi < 7 && BLj > 0; BLi++, BLj--);
            if(BRi < 7 && BRj < 7) for (;is_space(BRi, BRj) && BRi < 7 && BRj < 7; BRi++, BRj++);
            if (i < TLi && j < TLj || i < TRi && j > TRj || i > BLi && j < BLj || i > BRi && j > BRj)
                return false;
            return true;
        }
        bool check_knight(int i, int j)
        {
            if(i == selectedI - 2 && j == selectedJ - 1 || i == selectedI - 2 && j == selectedJ + 1 ||
               i == selectedI - 1 && j == selectedJ - 2 || i == selectedI - 1 && j == selectedJ + 2 ||
               i == selectedI + 1 && j == selectedJ - 2 || i == selectedI + 1 && j == selectedJ + 2 ||
               i == selectedI + 2 && j == selectedJ - 1 || i == selectedI + 2 && j == selectedJ + 1)
                return true;
            return false;
        }
        bool check_king(int i, int j)
        {
            if (i == selectedI - 1 && j == selectedJ - 1 ||
               i == selectedI - 1 && j == selectedJ ||
               i == selectedI - 1 && j == selectedJ + 1 ||
               i == selectedI && j == selectedJ + 1 ||
               i == selectedI + 1 && j == selectedJ + 1 ||
               i == selectedI + 1 && j == selectedJ ||
               i == selectedI + 1 && j == selectedJ - 1 ||
               i == selectedI && j == selectedJ - 1)
            {
                if(active_side == "White")
                {
                    white_kingI = i;
                    white_kingJ = j;
                }
                else
                {
                    black_kingI = i;
                    black_kingJ = j;
                }
                return true;
            }
            return false;
        }
        bool check_queen(int i, int j)
        {
            return check_rook(i, j) || check_bishop(i, j);
            return true;
        }
        bool can_move(int i, int j)
        {
            if (i == selectedI && j == selectedJ) return false;
            if (i == selectedI && j == selectedJ) return false;
            if (chess_board[i, j].Tag.ToString().Split(' ')[0] == active_side) return false;
            if (active_piece == "Pawn") return check_pawn(i, j);
            if (active_piece == "Rook") return check_rook(i, j);
            if (active_piece == "Bishop") return check_bishop(i, j);
            if (active_piece == "Knight") return check_knight(i, j);
            if (active_piece == "Queen") return check_queen(i, j);
            if (active_piece == "King") return check_king(i, j);
            return true;
        }
        public void move_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string new_tag = b.Tag.ToString();
            string my_tag = chess_board[selectedI, selectedJ].Tag.ToString();
            Point p = get_point(new_tag);
            int i = p.X - 1, j = p.Y - 1;
            if (is_selected == 1)
            {
                if (can_move(i, j))
                {
                    b.BackgroundImage = chess_board[selectedI, selectedJ].BackgroundImage;
                    chess_board[selectedI, selectedJ].BackgroundImage = null;
                    b.Tag = my_tag;
                    chess_board[selectedI, selectedJ].Tag = "Neutral Space " + selectedI + selectedJ;
                    turn++;
                }
                else if (new_tag.Split(' ')[0] == active_side)
                {
                    set_activepiece(i, j, new_tag);
                    return;
                }
                is_selected = 0;
            }
            else
            {
                if (turn % 2 == 1 && new_tag.Split(' ')[0] == "Black" || turn % 2 == 0 && new_tag.Split(' ')[0] == "White" || new_tag.Split(' ')[0] == "Neutral")
                {
                    return;
                }
                else
                {
                    is_selected = 1;
                    set_activepiece(i, j, new_tag);
                }
            }
        }
        public void set_board()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    chess_board[i, j] = new Button();
                    chess_board[i, j].Size = new Size(60, 60);
                    chess_board[i, j].Tag = get_piecetype(i, j);
                    chess_board[i, j].BackColor = get_backcolor(i, j);
                    chess_board[i, j].BackgroundImage = get_image(i, j);
                    chess_board[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    chess_board[i, j].Location = new Point(60*j, 60*i);
                    chess_board[i, j].FlatStyle = FlatStyle.Flat;
                    chess_board[i, j].Click += new EventHandler(move_click);
                    this.panelGame.Controls.Add(chess_board[i, j]);
                }
            }
            for(int i = 0; i < 8; i++)
            {
                did_first_move[0, i] = false;
                did_first_move[1, i] = false;
            }
        }
    }
}
