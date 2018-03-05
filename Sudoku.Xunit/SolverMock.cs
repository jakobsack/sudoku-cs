using System;
using Xunit;
using Sudoku;
using Sudoku.Solvers;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Xunit
{
    public class SolverMock : Solver
    {
        private List<Cell> CellsToReplace;

        public SolverMock(Board board, List<Cell> cells) : base(board)
        {
            CellsToReplace = cells;
        }

        public override List<Cell> Find()
        {
            return CellsToReplace;
        }
    }
}
