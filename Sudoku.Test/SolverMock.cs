namespace Sudoku.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sudoku.Solvers;
    using Xunit;

    public class SolverMock : Solver
    {
        private List<Cell> cellsToReplace;

        public SolverMock(Board board, List<Cell> cells)
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
