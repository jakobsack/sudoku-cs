using System;
using Xunit;
using Sudoku;
using Sudoku.Reducers;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Xunit
{
    public class ReducerMock : Reducer
    {
        private List<Cell> CellsToReplace;

        public ReducerMock(Board board, List<Cell> cells) : base(board)
        {
            CellsToReplace = cells;
        }

        public override List<Cell> Find()
        {
            return CellsToReplace;
        }
    }
}
