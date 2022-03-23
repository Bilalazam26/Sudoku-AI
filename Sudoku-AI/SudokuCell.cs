using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_AI
{
    class SudokuCell : Button
    {
        public int value { get; set; }
        public int x { get; set; }
        public  int y { get; set; }
        public bool locked { get; set; }

        public void clear()
        {
            this.Text = "";
        }
    }
}
