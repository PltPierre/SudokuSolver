using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        public static int[,] board;

        static void Main(string[] args)
        {
            board = new int[9, 9]
            {
                {8,0,0,  0,0,0,  0,0,0},
                {0,0,3,  6,0,0,  0,0,0},
                {0,7,0,  0,9,0,  2,0,0},

                {0,5,0,  0,0,7,  0,0,0},
                {0,0,0,  0,4,5,  7,0,0},
                {0,0,0,  1,0,0,  0,3,0},

                {0,0,1,  0,0,0,  0,6,8},
                {0,0,8,  5,0,0,  0,1,0},
                {0,9,0,  0,0,0,  4,0,0}
            };

            Program.printBoard();
            Program.SolveSudoku();
            Console.WriteLine("____________________________________________________________________________________");
            Program.printBoard();
            Console.ReadLine();
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
