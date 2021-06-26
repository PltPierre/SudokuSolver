using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    class Program
    {
        public static int[,] board;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Solver());
        }

        public static void setBoard(int[,] board2)
        {
            Program.board = board2;
        }

        public static int[,] getBoard()
        {
            return Program.board;
        }

        public static int[,] newBoard()
        {
            board = new int[9, 9];

            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = 0;
                }
            }

            var ran = new Random();
            int row;
            int col;
            int num;

            for (int k = 0; k < 5; k++)
            {
                row = ran.Next(0, 9);
                col = ran.Next(0, 9);
                num = ran.Next(1, 10);

                if (k == 0)
                {
                    board[row, col] = num;
                }

                while(!isValid(row, col, num) && board[row, col] == 0)
                {
                    row = ran.Next(0, 9);
                    col = ran.Next(0, 9);
                    num = ran.Next(1, 10);
                }

                board[row, col] = num;

            }

            Program.SolveSudoku();

            for(int h = 0; h < 63; h++)
            {
                row = ran.Next(0, 9);
                col = ran.Next(0, 9);

                if (h == 0)
                {
                    board[row, col] = 0;
                }

                while (board[row, col] == 0)
                {
                    row = ran.Next(0, 9);
                    col = ran.Next(0, 9);
                }

                board[row, col] = 0;
            }

            return board;
        }

        public static void printBoard()
        {
            Console.WriteLine("-------------------------------------------------");
            for (int i = 0; i < 9; i++)
            {
                Console.Write("|");
                Console.Write("   ");
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(board[i, j]);
                    Console.Write("   ");
                    if ((j + 1) % 3 == 0)
                    {
                        Console.Write("|");
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
                if ((i + 1) % 3 == 0)
                {
                    Console.WriteLine("-------------------------------------------------");
                }
            }
        }

        public static void SolveSudoku()
        {
            backtrack(0, 0);
        }

        private static bool backtrack(int ligne, int col)
        {
            if (col == 9)
            {
                col = 0; ++ligne;
                if (ligne == 9)
                {
                    return true;
                }
            }

            if (board[ligne, col] != 0)
            {
                return backtrack(ligne, col + 1);
            }

            for (int n = 1; n <= 9; n++)
            {
                if (isValid(ligne, col, n))
                {
                    board[ligne, col] = n;
                    if (backtrack(ligne, col + 1))
                    {
                        return true;
                    }
                    else
                    {
                        board[ligne, col] = 0;
                    }
                }
            }

            return false;
        }
        private static bool isValid(int ligne, int col, int val)
        {
            //check col
            for (int i = 0; i < 9; i++)
            {
                if (board[i, col] == val)
                {
                    return false;
                }
            }
                

            //check ligne
            for (int c = 0; c < 9; c++)
            {
                if (board[ligne, c] == val)
                {
                    return false;
                }
            }

            //check 3 X 3 box
            int I = ligne / 3;
            int J = col / 3;

            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    if (val == board[3 * I + a, 3 * J + b])
                    {
                        return false;
                    }
                }
            }

            return true;

        }
    }
}
