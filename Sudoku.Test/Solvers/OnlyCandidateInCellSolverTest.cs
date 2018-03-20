namespace Sudoku.Test.Solvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sudoku.Solvers;
    using Xunit;

    public class OnlyCandidateInCellSolverTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            OnlyCandidateInCellSolver solver = new OnlyCandidateInCellSolver(board);
        }

        [Fact]
        public void FindSinglesTest()
        {
            Board board = new Board();
            board.ReplaceCell(new Cell(0, 0, new List<int> { 3 }));
            OnlyCandidateInCellSolver solver = new OnlyCandidateInCellSolver(board);

            List<Cell> result = solver.Find();
            Assert.Single(result);
            Assert.Equal(3, result.First().Number);
        }
    }
}
