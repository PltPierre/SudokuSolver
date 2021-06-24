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
        public static int[,] board;

        public Solver()
        {
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
            board = new int[9, 9];

            foreach (TextBox c in GetTextBoxes())
            {
                if(c.Text=="")
                {
                    board[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))] = 0;
                }
                else
                {
                    board[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))] = Int32.Parse(c.Text);
                }
            }

            Program.setBoard(board);
            Program.SolveSudoku();
            board = Program.getBoard();

            foreach (TextBox c in GetTextBoxes())
            {
                c.Text = "" + board[Int32.Parse(c.Name.Substring(3, 1)), Int32.Parse(c.Name.Substring(4, 1))];
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
            foreach (TextBox c in GetTextBoxes())
            {
                c.Text = string.Empty;
            }
        }
    }
}
