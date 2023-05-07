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
using System.Collections.Specialized;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.SqlServer.Server;
using System.Diagnostics;

namespace Chess_Atestat
{
    public partial class FormJoc : Form
    {
        Button[,] chess_board = new Button[9, 9];

        SqlConnection conn;
        PictureBox[] whiteTaken = new PictureBox[17];
        PictureBox[] blackTaken = new PictureBox[17];
        int turn = 1;
        bool whiteKingMoved = false;
        bool blackKingMoved = false;
        int white_kingI = 7;
        int white_kingJ = 4;
        int black_kingI = 0;
        int black_kingJ = 4;
        int whiteLost = 0, blackLost = 0;
        string playerWhite, playerBlack;

        public FormJoc(string getWhite, string getBlack)
        {
            InitializeComponent();
            stuff(getWhite, getBlack);
        }

        public void button_MouseEnterLeave(object sender, EventArgs e)
        {
            Button b1 = (Button)sender;
            if (b1.BackColor == Color.SaddleBrown) b1.BackColor = Color.Peru;
            else b1.BackColor = Color.SaddleBrown;
        }

        public void stuff(string getWhite, string getBlack)
        {
            deschidere_conexiune();
            set_board();
            set_time();
            playerWhite = getWhite;
            playerBlack = getBlack;
            this.Text = playerWhite + " vs " + playerBlack;
            labelWhite.Text = "WHITE: " + playerWhite;
            labelBlack.Text = "BLACK: " + playerBlack;
            labelTurns.Text = "";
        }

        int timeWhite = 900, timeBlack = 900;
        Timer timerWhite = new Timer();
        Timer timerBlack = new Timer();
        Timer timerTurns = new Timer();
        public void set_time()
        {
            timerWhite.Interval = 1000;
            timerWhite.Tick += new EventHandler(timerTime_tick);
            timerBlack.Interval = 1000;
            timerBlack.Tick += new EventHandler(timerTime_tick);
            timerTurns.Interval = 10;
            timerTurns.Tick += new EventHandler(timerTurns_tick);
            timerTurns.Start();
        }
        public void timerTime_tick(object sender, EventArgs e)
        {
            if (turn % 2 == 1)
            {
                timeWhite--;
                labelTimeWhite.Text = $"{timeWhite / 60}:{timeWhite % 60}";
            }
            else
            {
                timeBlack--;
                labelTimeBlack.Text = $"{timeBlack / 60}:{timeBlack % 60}";
            }
        }
        public void timerTurns_tick(object sender, EventArgs e)
        {
            if (turn % 2 == 1)
            {
                timerBlack.Stop();
                labelSide.Text = "White Turn";
                timerWhite.Start();
            }
            else
            {
                timerBlack.Start();
                labelSide.Text = "Black Turn";
                timerWhite.Stop();
            }
        }

        public void deschidere_conexiune()
        {
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Sah.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
        }

        public Color init_backcolor(int i, int j)
        {
            if (i % 2 == 0) {
                if (j % 2 == 0) return Color.Wheat;
                else return Color.SaddleBrown;
            }
            else
            {
                if (j % 2 == 0) return Color.SaddleBrown;
                else return Color.Wheat;
            }
        }
        public string init_piece(int i, int j)
        {
            if (i == 1) return "Black Pawn " + (j + 1);
            if (i == 6) return "White Pawn " + (j + 1);
            if (i == 0 && (j == 0 || j == 7)) return "Black Rook " + ((j == 0) ? 1 : 2);
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
        public Image init_image(int i, int j)
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
        int activeI, activeJ;
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
        int get_ord(int i, int j)
        {
            string ord = chess_board[i, j].Tag.ToString().Split(' ')[2];
            //Console.WriteLine(Convert.ToInt32(ord) - 1);
            return Convert.ToInt32(ord) - 1;
        }
        public string get_piece(int i, int j)
        {
            Button b = new Button();
            b = chess_board[i, j];
            //Console.WriteLine(b.Tag);
            return b.Tag.ToString().Split(' ')[1];
        }
        public string get_side(int i, int j)
        {
            return chess_board[i, j].Tag.ToString().Split(' ')[0];
        }
        public int current_KingI()
        {
            if (active_side == "Black")
                return black_kingI;
            return white_kingI;
        }
        public int current_KingJ()
        {
            if (active_side == "Black")
                return black_kingJ;
            return white_kingJ;
        }
        public string get_opposing_side()
        {
            if (active_side == "Black")
                return "White";
            return "Black";
        }

        public void set_activepiece(int i, int j, string tag)
        {
            string[] info = tag.Split(' ');
            string piece = info[1], side = info[0];
            Console.WriteLine(side + " " + piece + " " + info[2]);
            active_piece = piece;
            activeI = i;
            activeJ = j;
            active_side = side;
            ///Console.WriteLine($"//////\n{selectedI} {selectedJ} {active_piece} {active_side}\n//////\n");
        }

        bool is_space(int i, int j)
        {
            //Console.WriteLine(i+ " " + j);
            if (get_piece(i, j) == "Space")
                return true;
            return false;
        }

        bool pawn_moves(int i, int j)
        {
            int k = (active_side == "White") ? (-1) : 1, z = (active_side == "White") ? (0) : (1);
            if (i == activeI + 2 * k && j == activeJ && is_space(i, j) && curr_is_checked() == false && did_first_move[z, get_ord(activeI, activeJ)] == false)
            {
                did_first_move[z, get_ord(activeI, activeJ)] = true;
                return true;
            }
            if (i == activeI + k && (j == activeJ - 1 || j == activeJ + 1) && !is_space(i, j))
            {
                did_first_move[z, get_ord(activeI, activeJ)] = true;
                return true;
            }
            if (i != activeI + k || j != activeJ)
                return false;
            if (i == activeI + k && j == activeJ && !is_space(i, j))
                return false;
            did_first_move[z, get_ord(activeI, activeJ)] = true;
            return true;
        }
        bool rook_moves(int i, int j)
        {
            if (i != activeI && j != activeJ)
            {
                return false;
            }
            int u = activeI - 1, d = activeI + 1, l = activeJ - 1, r = activeJ + 1;
            if (u > 0) for (; is_space(u, activeJ) && u > 0; u--) ;
            if (d < 7) for (; is_space(d, activeJ) && d < 7; d++) ;
            if (l > 0) for (; is_space(activeI, l) && l > 0; l--) ;
            if (r < 7) for (; is_space(activeI, r) && r < 7; r++) ;
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
        bool bishop_moves(int i, int j)
        {
            if (i + j != activeI + activeJ && i - j != activeI - activeJ)
                return false;
            int TLi = activeI - 1, TLj = activeJ - 1;
            int TRi = activeI - 1, TRj = activeJ + 1;
            int BLi = activeI + 1, BLj = activeJ - 1;
            int BRi = activeI + 1, BRj = activeJ + 1;
            if (TLi > 0 && TLj > 0) for (; is_space(TLi, TLj) && TLi > 0 && TLj > 0; TLi--, TLj--) ;
            if (TRi > 0 && TRj < 7) for (; is_space(TRi, TRj) && TRi > 0 && TRj < 7; TRi--, TRj++) ;
            if (BLi < 7 && BLj > 0) for (; is_space(BLi, BLj) && BLi < 7 && BLj > 0; BLi++, BLj--) ;
            if (BRi < 7 && BRj < 7) for (; is_space(BRi, BRj) && BRi < 7 && BRj < 7; BRi++, BRj++) ;
            if (i < TLi && j < TLj || i < TRi && j > TRj || i > BLi && j < BLj || i > BRi && j > BRj)
                return false;
            return true;
        }
        bool knight_moves(int i, int j)
        {
            if (i == activeI - 2 && j == activeJ - 1 || i == activeI - 2 && j == activeJ + 1 ||
               i == activeI - 1 && j == activeJ - 2 || i == activeI - 1 && j == activeJ + 2 ||
               i == activeI + 1 && j == activeJ - 2 || i == activeI + 1 && j == activeJ + 2 ||
               i == activeI + 2 && j == activeJ - 1 || i == activeI + 2 && j == activeJ + 1)
                return true;
            return false;
        }
        bool king_moves(int i, int j)
        {
            if (i == activeI - 1 && j == activeJ - 1 ||
               i == activeI - 1 && j == activeJ ||
               i == activeI - 1 && j == activeJ + 1 ||
               i == activeI && j == activeJ + 1 ||
               i == activeI + 1 && j == activeJ + 1 ||
               i == activeI + 1 && j == activeJ ||
               i == activeI + 1 && j == activeJ - 1 ||
               i == activeI && j == activeJ - 1)
            {
                return true;
            }
            return false;
        }
        bool queen_moves(int i, int j)
        {
            return rook_moves(i, j) || bishop_moves(i, j);
            return true;
        }
        bool can_move(int i, int j)
        {
            if (i == activeI && j == activeJ) return false;
            if (i == activeI && j == activeJ) return false;
            if (chess_board[i, j].Tag.ToString().Split(' ')[0] == active_side) return false;
            if (active_piece == "Pawn") return pawn_moves(i, j);
            if (active_piece == "Rook") return rook_moves(i, j);
            if (active_piece == "Bishop") return bishop_moves(i, j);
            if (active_piece == "Knight") return knight_moves(i, j);
            if (active_piece == "Queen") return queen_moves(i, j);
            if (active_piece == "King") return king_moves(i, j);
            return true;
        }

        bool is_not_king(int i, int j)
        {
            if (active_side == "White")
            {
                if (i == black_kingI && j == black_kingJ)
                    return false;
            }
            if (active_side == "Black")
            {
                if (i == white_kingI && j == white_kingJ)
                    return false;
            }
            return true;
        }

        bool checked_by_pawn(int KI, int KJ)
        {
            if(active_side == "White")
            {

                if (KI - 1 >= 0 && KJ - 1 >= 0) if (get_piece(KI - 1, KJ - 1) == "Pawn" && get_side(KI - 1, KJ - 1) == "Black")
                    {return true; }
                if (KI - 1 >= 0 && KJ + 1 <= 7) if (get_piece(KI - 1, KJ + 1) == "Pawn" && get_side(KI - 1, KJ + 1) == "Black")
                    {return true; }

                return false;
            }
            else {

                if (KI + 1 <= 7 && KJ - 1 >= 0) if (get_piece(KI + 1, KJ - 1) == "Pawn" && get_side(KI + 1, KJ - 1) == "White")
                    {return true; }
                if (KI + 1 <= 7 && KJ + 1 <= 7) if (get_piece(KI + 1, KJ + 1) == "Pawn" && get_side(KI + 1, KJ + 1) == "White")
                    {return true; }

                return false;
            }

        }
        bool checked_by_bishop(int KI, int KJ)
        {
            int i, j;
            for(i = KI-1, j = KJ-1; i >= 0 && j >= 0; i--, j--)
                if(is_space(i, j) == false)
                {
                    if ((get_piece(i, j) == "Bishop" || get_piece(i, j) == "Queen") && get_side(i, j) == get_opposing_side())
                        return true;
                    break;
                }
            for (i = KI - 1, j = KJ + 1; i >= 0 && j <= 7; i--, j++)
                if (is_space(i, j) == false)
                {
                    if ((get_piece(i, j) == "Bishop" || get_piece(i, j) == "Queen") && get_side(i, j) == get_opposing_side())
                        return true;
                    break;
                }
            for (i = KI + 1, j = KJ + 1; i <= 7 && j <= 7; i++, j++)
                if (is_space(i, j) == false)
                {
                    if ((get_piece(i, j) == "Bishop" || get_piece(i, j) == "Queen") && get_side(i, j) == get_opposing_side())
                        return true;
                    break;
                }
            for (i = KI + 1, j = KJ - 1; i <= 7 && j >= 0; i++, j--)
                if (is_space(i, j) == false)
                {
                    if ((get_piece(i, j) == "Bishop" || get_piece(i, j) == "Queen") && get_side(i, j) == get_opposing_side())
                        return true;
                    break;
                }
            return false;
        }
        bool checked_by_rook(int KI, int KJ)
        {
            int i, j;
            for (i = KI - 1, j = KJ; i >= 0; i--)
                if (is_space(i, j) == false)
                {
                    if ((get_piece(i, j) == "Rook" || get_piece(i, j) == "Queen") && get_side(i, j) == get_opposing_side())
                        return true;
                    break;
                }
            for (i = KI + 1, j = KJ; i <= 7; i++)
                if (is_space(i, j) == false)
                {
                    if ((get_piece(i, j) == "Rook" || get_piece(i, j) == "Queen") && get_side(i, j) == get_opposing_side())
                        return true;
                    break;
                }
            for (i = KI, j = KJ-1; j >= 0; j--)
                if (is_space(i, j) == false)
                {
                    if ((get_piece(i, j) == "Rook" || get_piece(i, j) == "Queen") && get_side(i, j) == get_opposing_side())
                        return true;
                    break;
                }
            for (i = KI, j = KJ+1; j <= 7; j++)
                if (is_space(i, j) == false)
                {
                    if ((get_piece(i, j) == "Rook" || get_piece(i, j) == "Queen") && get_side(i, j) == get_opposing_side())
                        return true;
                    break;
                }
            return false;
        }
        bool checked_by_knight(int KI, int KJ)
        {
            if (KI - 2 >= 0 && KJ - 1 >= 0) if (get_piece(KI - 2, KJ - 1) == "Knight" && get_side(KI - 2, KJ - 1) == get_opposing_side()) return true;
            if (KI - 2 >= 0 && KJ + 1 <= 7) if (get_piece(KI - 2, KJ + 1) == "Knight" && get_side(KI - 2, KJ + 1) == get_opposing_side()) return true;
            
            if (KI - 1 >= 0 && KJ - 2 >= 0) if (get_piece(KI - 1, KJ - 2) == "Knight" && get_side(KI - 1, KJ - 2) == get_opposing_side()) return true;
            if (KI - 1 >= 0 && KJ + 2 <= 7) if (get_piece(KI - 1, KJ + 2) == "Knight" && get_side(KI - 1, KJ + 2) == get_opposing_side()) return true;
            
            if (KI + 1 <= 7 && KJ - 2 >= 0) if (get_piece(KI + 1, KJ - 2) == "Knight" && get_side(KI + 1, KJ - 2) == get_opposing_side()) return true;
            if (KI + 1 <= 7 && KJ + 2 <= 7) if (get_piece(KI + 1, KJ + 2) == "Knight" && get_side(KI + 1, KJ + 2) == get_opposing_side()) return true;

            if (KI + 2 <= 7 && KJ - 1 >= 0) if (get_piece(KI + 2, KJ - 1) == "Knight" && get_side(KI + 2, KJ - 1) == get_opposing_side()) return true;
            if (KI + 2 <= 7 && KJ + 1 <= 7) if (get_piece(KI + 2, KJ + 1) == "Knight" && get_side(KI + 2, KJ + 1) == get_opposing_side()) return true;

            return false;
        }
        bool curr_is_checked()
        {
            int KI = current_KingI(), KJ = current_KingJ();
            //Console.WriteLine(KI + " " + KJ);

            return checked_by_pawn(KI, KJ) || checked_by_bishop(KI, KJ) || checked_by_rook(KI, KJ) || checked_by_knight(KI, KJ);
        }

        bool simulate(int oldI, int oldJ, int newI, int newJ)
        {
            int is_checked = 1;
            string oldTag = chess_board[oldI, oldJ].Tag.ToString();
            string newTag = chess_board[newI, newJ].Tag.ToString();

            if(get_piece(oldI, oldJ) == "King")
            {
                if(active_side == "White")
                {
                    white_kingI = newI;
                    white_kingJ = newJ;
                }
                else
                {
                    black_kingI = newI;
                    black_kingJ = newJ;
                }
            }

            chess_board[oldI, oldJ].Tag = "Neutral Space" + oldI + oldJ;
            chess_board[newI, newJ].Tag = oldTag;

            if (curr_is_checked() == false)
                is_checked = 0;

            if (get_piece(newI, newJ) == "King")
            {
                if (active_side == "White")
                {
                    white_kingI = oldI;
                    white_kingJ = oldJ;
                }
                else
                {
                    black_kingI = oldI;
                    black_kingJ = oldJ;
                }
            }

            chess_board[oldI, oldJ].Tag = oldTag;
            chess_board[newI, newJ].Tag = newTag;

            if (is_checked == 0)
                return true;
            return false;
        }

        bool pawn_stops_mate(int i, int j)
        {
            if(active_side == "White")
            {
                if (i - 1 >= 0) if (get_piece(i - 1, j) == "Space") if (simulate(i, j, i - 1, j) == true) return true;
                if (i - 1 >= 0 && j - 1 >= 0) if (get_piece(i - 1, j - 1) != "Space" && get_side(i - 1, j - 1) == get_opposing_side()) if (simulate(i, j, i - 1, j - 1) == true) return true;
                if (i - 1 >= 0 && j + 1 <= 7) if (get_piece(i - 1, j + 1) != "Space" && get_side(i - 1, j + 1) == get_opposing_side()) if (simulate(i, j, i - 1, j + 1) == true) return true;
                if (i - 2 >= 0 && did_first_move[0, get_ord(i, j)] == false) if (get_piece(i - 1, j) == "Space" && get_piece(i - 2, j) == "Space") if (simulate(i, j, i - 2, j) == true) return true;
            }
            else
            {
                if (i + 1 <= 7) if (get_piece(i + 1, j) == "Space") if (simulate(i, j, i + 1, j) == true) return true;
                if (i + 1 <= 7 && j - 1 >= 0) if (get_piece(i + 1, j - 1) != "Space" && get_side(i + 1, j - 1) == get_opposing_side()) if (simulate(i, j, i + 1, j - 1) == true) return true;
                if (i + 1 <= 7 && j + 1 <= 7) if (get_piece(i + 1, j + 1) != "Space" && get_side(i + 1, j + 1) == get_opposing_side()) if (simulate(i, j, i + 1, j - 1) == true) return true;
                if (i + 2 <= 7 && did_first_move[0, get_ord(i, j)] == false) if (get_piece(i + 1, j) == "Space" && get_piece(i + 2, j) == "Space") if (simulate(i, j, i + 2, j) == true) return true;
            }
            return false;
        }
        bool bishop_stops_mate(int i, int j)
        {

            return false;
        }
        bool rook_stops_mate(int i, int j)
        {
            return false;
        }
        bool knight_stops_mate(int i, int j)
        {
            return false;
        }
        bool king_stops_mate(int i, int j)
        {
            if (i - 1 >= 0 && j - 1 >= 0) if (get_piece(i - 1, j - 1) == "Space" || get_side(i - 1, j - 1) == get_opposing_side()) if (simulate(i, j, i - 1, j - 1) == true) return true;
            if (i - 1 >= 0) if (get_piece(i - 1, j) == "Space" || get_side(i - 1, j) == get_opposing_side()) if (simulate(i, j, i - 1, j) == true) return true;
            if (i - 1 >= 0 && j + 1 <= 7) if (get_piece(i - 1, j + 1) == "Space" || get_side(i - 1, j + 1) == get_opposing_side()) if (simulate(i, j, i - 1, j + 1) == true) return true;
            if (j + 1 <= 7) if (get_piece(i, j + 1) == "Space" || get_side(i, j + 1) == get_opposing_side()) if (simulate(i, j, i, j + 1) == true) return true;
            if (i + 1 <= 7 && j + 1 <= 7) if (get_piece(i + 1, j + 1) == "Space" || get_side(i + 1, j + 1) == get_opposing_side()) if (simulate(i, j, i + 1, j + 1) == true) return true;
            if (i + 1 <= 7) if (get_piece(i + 1, j) == "Space" || get_side(i + 1, j) == get_opposing_side()) if (simulate(i, j, i + 1, j) == true) return true;
            if (i + 1 <= 7 && j - 1 >= 0) if (get_piece(i + 1, j - 1) == "Space" || get_side(i + 1, j - 1) == get_opposing_side()) if (simulate(i, j, i + 1, j - 1) == true) return true;
            if (j - 1 >= 0) if (get_piece(i, j - 1) == "Space" || get_side(i, j - 1) == get_opposing_side()) if (simulate(i, j, i, j - 1) == true) return true;

            return false;
        }

        bool curr_is_mated()
        {
            for(int i = 0; i <= 7; i++)
                for(int j = 0; j <= 7; j++)
                {
                    if (get_piece(i, j) == "Space" || get_side(i, j) == get_opposing_side()) continue;
                    if (get_piece(i, j) == "Pawn") if (pawn_stops_mate(i, j) == true) return false;
                    if (get_piece(i, j) == "Bishop" || get_piece(i, j) == "Queen") if (bishop_stops_mate(i, j) == true) return false;
                    if (get_piece(i, j) == "Rook" || get_piece(i, j) == "Queen") if (rook_stops_mate(i, j) == true) return false;
                    if (get_piece(i, j) == "Knight") if (knight_stops_mate(i, j) == true) return false;
                    if (get_piece(i, j) == "King") if (king_stops_mate(i, j) == true) return false;
                }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            end_game();
        }

        public void end_game()
        {   
            SqlCommand cmd;
            cmd = new SqlCommand("UPDATE Utilizatori SET NrWinuri = NrWinuri+1, NrJocuri = NrJocuri+1 WHERE NumeUtilizator = @Nume", conn);
            cmd.Parameters.AddWithValue("@Nume", (turn%2 == 1) ? playerBlack : playerWhite);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("UPDATE Utilizatori SET NrJocuri = NrJocuri+1 WHERE NumeUtilizator = @Nume");
            cmd.Parameters.AddWithValue("@Nume", (turn%2 == 1) ? playerWhite : playerBlack);
            string winning_side = (turn % 2 == 1) ? "Black" : "White";
            MessageBox.Show($"{winning_side} Won!");
            this.Close();
        }

        public void highlight()
        {
            
        }
        public void decolor()
        {
            /*for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                    chess_board[i, j].BackColor = init_backcolor(i, j);*/
        }

        public void move_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string new_tag = b.Tag.ToString();
            string my_tag = chess_board[activeI, activeJ].Tag.ToString();
            Point p = get_point(new_tag);
            int i = p.X - 1, j = p.Y - 1;
            int auxKingI = current_KingI();
            int auxKingJ = current_KingJ();
            if (is_selected == 1)
            {
                if (can_move(i, j) && is_not_king(i, j))
                {
                    if (curr_is_checked() == true)
                    {
                        Console.WriteLine($"{active_side} is in Check!");
                        if (active_piece == "King")
                        {
                            if (active_side == "White")
                            {
                                white_kingI = i;
                                white_kingJ = j;
                            }
                            else
                            {
                                black_kingI = i;
                                black_kingJ = j;
                            }
                        }
                        b.Tag = my_tag;
                        chess_board[activeI, activeJ].Tag = "Neutral Space " + activeI + activeJ;
                        if(curr_is_checked() == false)
                        {
                            if (active_piece == "King")
                            {
                                if (active_side == "White")
                                {
                                    white_kingI = auxKingI;
                                    white_kingJ = auxKingJ;
                                }
                                else
                                {
                                    black_kingI = auxKingI;
                                    black_kingJ = auxKingJ;
                                }
                            }
                            b.Tag = new_tag;
                            chess_board[activeI, activeJ].Tag = my_tag;
                            if (chess_board[i, j].Tag.ToString().Split(' ')[1] != "Space")
                            {
                                if (active_side == "White")
                                    whiteTaken[blackLost++].BackgroundImage = chess_board[i, j].BackgroundImage;
                                else
                                    blackTaken[whiteLost++].BackgroundImage = chess_board[i, j].BackgroundImage;
                            }
                            if (active_piece == "King")
                            {
                                if (active_side == "White")
                                {
                                    white_kingI = i;
                                    white_kingJ = j;
                                }
                                else
                                {
                                    black_kingI = i;
                                    black_kingJ = j;
                                }
                            }
                            string acro2 = "";
                            if (turn % 2 == 1)
                                labelTurns.Text += (turn + 1) / 2 + ".";
                            if (active_piece != "Pawn")
                                acro2 += active_piece[0];
                            labelTurns.Text += $" {acro2}{(char)('a' + j)}{8 - i}";
                            if (turn % 2 == 0)
                                labelTurns.Text += "/ ";
                            if (turn % 8 == 0)
                                labelTurns.Text += '\n';
                            b.Tag = my_tag;
                            chess_board[activeI, activeJ].Tag = "Neutral Space " + activeI + activeJ;
                            b.BackgroundImage = chess_board[activeI, activeJ].BackgroundImage;
                            chess_board[activeI, activeJ].BackgroundImage = null;
                            turn++;
                            is_selected = 0;
                            return;
                        }
                        if (active_piece == "King")
                        {
                            if (active_side == "White")
                            {
                                white_kingI = auxKingI;
                                white_kingJ = auxKingJ;
                            }
                            else
                            {
                                black_kingI = auxKingI;
                                black_kingJ = auxKingJ;
                            }
                        }

                        b.Tag = new_tag;
                        chess_board[activeI, activeJ].Tag = my_tag;
                        return;
                    }
                    if (active_piece == "King")
                    {
                        if (active_side == "White")
                        {
                            white_kingI = i;
                            white_kingJ = j;
                        }
                        else
                        {
                            black_kingI = i;
                            black_kingJ = j;
                        }
                    }
                    b.Tag = my_tag;
                    chess_board[activeI, activeJ].Tag = "Neutral Space " + activeI + activeJ;
                    if(curr_is_checked() == true)
                    {
                        if (active_piece == "King")
                        {
                            if (active_side == "White")
                            {
                                white_kingI = auxKingI;
                                white_kingJ = auxKingJ;
                            }
                            else
                            {
                                black_kingI = auxKingI;
                                black_kingJ = auxKingJ;
                            }
                        }
                        b.Tag = new_tag;
                        chess_board[activeI, activeJ].Tag = my_tag;
                        is_selected = 0;
                        return;
                    }
                    if (active_piece == "King")
                    {
                        if (active_side == "White")
                        {
                            white_kingI = auxKingI;
                            white_kingJ = auxKingJ;
                        }
                        else
                        {
                            black_kingI = auxKingI;
                            black_kingJ = auxKingJ;
                        }
                    }
                    b.Tag = new_tag;
                    chess_board[activeI, activeJ].Tag = my_tag;
                    if (chess_board[i, j].Tag.ToString().Split(' ')[1] != "Space")
                    {
                        if (active_side == "White")
                            whiteTaken[blackLost++].BackgroundImage = chess_board[i, j].BackgroundImage;
                        else
                            blackTaken[whiteLost++].BackgroundImage = chess_board[i, j].BackgroundImage;
                    }
                    if(active_piece == "King")
                    {
                        if (active_side == "White")
                        {
                            white_kingI = i;
                            white_kingJ = j;
                        }
                        else
                        {
                            black_kingI = i;
                            black_kingJ = j;
                        }
                    }
                    string acro = "";
                    if (turn % 2 == 1)
                        labelTurns.Text += (turn + 1) / 2 + ".";
                    if(active_piece != "Pawn")
                        acro += active_piece[0];
                    labelTurns.Text += $" {acro}{(char)('a' + j)}{8 - i}";   
                    if(turn % 2 == 0)
                        labelTurns.Text += "/ ";
                    if (turn % 8 == 0)
                        labelTurns.Text += '\n';
                    b.BackgroundImage = chess_board[activeI, activeJ].BackgroundImage;
                    chess_board[activeI, activeJ].BackgroundImage = null;
                    b.Tag = my_tag;
                    chess_board[activeI, activeJ].Tag = "Neutral Space " + activeI + activeJ;
                    turn++;
                }
                else if (new_tag.Split(' ')[0] == active_side)
                {
                    decolor();
                    set_activepiece(i, j, new_tag);
                    highlight();
                    return;
                }
                decolor();
                is_selected = 0;
            }
            else
            {
                if (turn % 2 == 1 && new_tag.Split(' ')[0] == "Black" || turn % 2 == 0 && new_tag.Split(' ')[0] == "White" || new_tag.Split(' ')[0] == "Neutral")
                {
                    decolor();
                    return;
                }
                else
                {
                    is_selected = 1;
                    set_activepiece(i, j, new_tag);
                    highlight();
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
                    chess_board[i, j].Tag = init_piece(i, j);
                    chess_board[i, j].BackColor = init_backcolor(i, j);
                    chess_board[i, j].BackgroundImage = init_image(i, j);
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
            for(int i = 0; i < 16; i++)
            {
                whiteTaken[i] = new PictureBox();
                whiteTaken[i].Location = new Point(72 + 30 * (i % 8), 52 + 30 * (i / 8));
                whiteTaken[i].BackColor = Color.Transparent;
                whiteTaken[i].BackgroundImageLayout = ImageLayout.Stretch;
                //iteTaken[i].BackColor = Color.Black;
                whiteTaken[i].Size = new Size(30,30);
                this.panel2.Controls.Add(whiteTaken[i]);

                blackTaken[i] = new PictureBox();
                blackTaken[i].Location = new Point(72 + 30 * (i % 8), 153 + 30 * (i / 8));
                blackTaken[i].BackColor = Color.Transparent;
                blackTaken[i].BackgroundImageLayout = ImageLayout.Stretch;
                //blackTaken[i].BackColor = Color.Black;
                blackTaken[i].Size = new Size(30, 30);
                this.panel2.Controls.Add(blackTaken[i]);
            }
        }
    }
}
