namespace Sudoku.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Xunit;
    using Reducers;

    public class ReducerMock : Reducer
    {
        private List<Cell> cellsToReplace;

        public ReducerMock(Board board, List<Cell> cells)
            : base(board)
        {
            cellsToReplace = cells;
        }

        public override List<Cell> Find()
        {
            return cellsToReplace;
        }
    }
}
