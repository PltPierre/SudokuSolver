using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Solver : Form
    {
        public static int[,] unsolvedBoard;
        public static int[,] solvedBoard;

        public Solver()
        {
            unsolvedBoard = new int[9, 9];

            InitializeComponent();
            foreach (TextBox c in GetTextBoxes())
            {
                c.TextAlign = HorizontalAlignment.Center;
                c.KeyPress += textBox_KeyPress;
                c.MaxLength = 1;
            }
            List<Label> labels = GetLabels();

            foreach (Label l in labels.Reverse<Label>())
            {
                l.SendToBack();
            }
        }

        public List<TextBox> GetTextBoxes()
        {
            var textBoxes = new List<TextBox>();
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    textBoxes.Add((TextBox) c);
                }
            }
            return textBoxes;
        }

        public List<Label> GetLabels()
        {
            var Labels = new List<Label>();
            foreach (Control c in Controls)
            {
                if (c is Label)
                {
                    Labels.Add((Label)c);
                }
            }
            return Labels;
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            lblResult.BackColor = Color.Black;
            solvedBoard = new int[9, 9];

            foreach (TextBox c in GetTextBoxes())
            {
                if(c.Text=="")
                {
                    solvedBoard[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))] = 0;
                }
                else
                {
                    solvedBoard[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))] = Int32.Parse(c.Text);
                }
            }

            Program.setBoard(unsolvedBoard);
            Program.SolveSudoku();
            solvedBoard = Program.getBoard();

            foreach (TextBox c in GetTextBoxes())
            {
                c.Text = "" + solvedBoard[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))];
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (sender == null)
            {
                return;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            lblResult.BackColor = Color.Black;
            foreach (TextBox c in GetTextBoxes())
            {
                c.Text = string.Empty;
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            solvedBoard = new int[9, 9];
            unsolvedBoard = new int[9, 9];

            foreach (TextBox c in GetTextBoxes())
            {
                if (c.Text == "")
                {
                    unsolvedBoard[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))] = 0;
                    solvedBoard[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))] = 0;

                }
                else
                {
                    unsolvedBoard[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))] = Int32.Parse(c.Text);
                    solvedBoard[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))] = Int32.Parse(c.Text);

                }
            }

            Program.setBoard(solvedBoard);
            Program.SolveSudoku();
            solvedBoard = Program.getBoard();

            bool verified = true;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (unsolvedBoard[i, j] != solvedBoard[i, j])
                    {
                        verified = false;
                    }
                }
            }


            if(verified)
            {
                lblResult.BackColor = Color.Green;
            }
            else
            {
                lblResult.BackColor = Color.Red;
            }
        }

        private void printArray(int[,] arr)
        {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", arr[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblResult.BackColor = Color.Black;
            int[,] board_new = Program.newBoard();

            foreach (TextBox c in GetTextBoxes())
            {
                int n = board_new[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))];
                if (n == 0)
                {
                    c.Text = "";
                }
                else
                {
                    c.Text = "" + board_new[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))];
                }
            }
        }
    }
}
