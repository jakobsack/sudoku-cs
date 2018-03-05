using System;
using Xunit;
using Sudoku;
using Sudoku.Solvers;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Xunit.Solvers
{
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
            board.ReplaceCell(new Cell(0, 0) { Candidates = new List<int> { 3 } });
            OnlyCandidateInCellSolver solver = new OnlyCandidateInCellSolver(board);

            List<Cell> result = solver.Find();
            Assert.Single(result);
            Assert.Equal(3, result.First().Number);
        }
    }
}
