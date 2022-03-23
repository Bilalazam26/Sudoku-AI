namespace Sudoku_AI
{
    public partial class Sudoku : Form
    {
        public Sudoku()
        {
            InitializeComponent();
            createBoard();
            newGame();
        }

        SudokuCell [,] board= new SudokuCell[9, 9];

        public void createBoard()
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    board[i, j] = new SudokuCell();
                    board[i, j].Location = new Point(i * 50 + 25, j * 50 + 25);
                    board[i, j].Size = new Size(50,50);
                    board[i, j].ForeColor = Color.Black;
                    board[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                    board[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? Color.LightCyan : Color.LightGreen;
                    board[i, j].FlatStyle = FlatStyle.Flat;
                    board[i, j].FlatAppearance.BorderColor = Color.Black;
                    board[i, j].KeyPress += Sudoku_KeyPress;
                    myPanel.Controls.Add(board[i, j]);
                }
            }
            
            
        }

        private void Sudoku_KeyPress(object? sender, KeyPressEventArgs e)
        {
            var cell = sender as SudokuCell;
            int value;
            if (cell.locked)
                return;
            if (int.TryParse(e.KeyChar.ToString(), out value))
            {
                if (value == 0)
                    cell.clear(); //if user press zero clear the cell
                else
                    cell.Text = value.ToString();
            }
        }

        Random random = new Random();
        public void newGame()
        {
            foreach (var cell in board)
            {
                cell.clear();
                cell.value = 0;
                cell.locked = false;
                cell.ForeColor = Color.Black;
            }
            loadValues(0, -1);
            showRandomValues(30);
            
        }

        private bool loadValues(int row, int col)
        {
            if (++col > 8) // increment the colomn by one and check if the row ends
            {
                col = 0;

                if (++row > 8) // increment the row by one and exit if the board ends
                    return true;
            }
            var value = 0;
            var nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            do
            {
                if (nums.Count < 1)
                {
                    board[row, col].value = 0;
                    return false;
                }
                value = nums[random.Next(0, nums.Count)];
                board[row, col].value = value;
                nums.Remove(value);
            } while (!isValidValue(value, row, col) || !loadValues(row, col));
            return true;
        }
        
        private bool isValidValue(int value, int i, int j)
        {
            
            for (int x = 0; x < 9; x++)
            {
                if (x != i && board[x, j].value == value)
                    return false;
                if (x != j && board[i, x].value == value)
                    return false;
            }

            for (int x = i - (i % 3); x < i - (i % 3) + 3; x++)
            {
                for (int y = j - (j % 3); y < j - (j % 3) + 3; y++)
                {
                    if (i != x && j != y && board[x, y].value == value)
                        return false;
                }
            }
            return true;
        }

        private void showRandomValues(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var x = random.Next(9);
                var y = random.Next(9);
                board[x, y].Text = board[x, y].value.ToString();
                board[x, y].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                board[x, y].ForeColor = Color.DarkCyan;
                board[x, y].locked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var cell in board)
            {
                // Clear the cell only if it is not locked
                if (!cell.locked)
                    cell.clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var wrongCells = new List<SudokuCell>();

            // Find all the wrong inputs
            foreach (var cell in board)
            {
                if (!string.Equals(cell.value.ToString(), cell.Text))
                {
                    wrongCells.Add(cell);
                }
                if (cell.Text == "")
                {
                    MessageBox.Show("Empty Cells");
                    return;
                }

            }

            // Check if the inputs are wrong or the player wins 
            if (wrongCells.Any())
            {
                // Highlight the wrong inputs with red color
                foreach (var cell in wrongCells)
                {
                    cell.ForeColor = Color.Red;
                }
                MessageBox.Show("Wrong inputs");
            }
            else
            {
                MessageBox.Show("Congratulations You Wins");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var wrongCells = new List<SudokuCell>();

            // Find all the wrong inputs
            foreach (var cell in board)
            {
                if (!string.Equals(cell.value.ToString(), cell.Text))
                {
                    wrongCells.Add(cell);
                }
            }
            foreach (var cell in wrongCells)
            {
                cell.clear();
            }
            foreach (var cell in board)
            {
                if(!cell.locked)
                    cell.ForeColor = Color.Black;
            }
        }
    }
}