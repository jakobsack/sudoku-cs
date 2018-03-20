namespace Sudoku.Test.Solvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sudoku.Solvers;
    using Xunit;

    public class OnlyOptionInColumnSolverTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            OnlyOptionInColumnSolver solver = new OnlyOptionInColumnSolver(board);
        }

        [Fact]
        public void FindSinglesTest()
        {
            Board board = new Board();
            board.ReplaceCell(new Cell(0, 0, new List<int> { 3 }));
            board.ReplaceCell(new Cell(0, 1, new List<int> { 3, 4 }));
            board.ReplaceCell(new Cell(0, 2, new List<int> { 5, 3 }));
            board.ReplaceCell(new Cell(0, 3, new List<int> { 5 }));
            board.ReplaceCell(new Cell(0, 4, new List<int> { 6, 3 }));
            board.ReplaceCell(new Cell(0, 5, new List<int> { 7, 3 }));
            board.ReplaceCell(new Cell(0, 6, new List<int> { 5, 6 }));
            board.ReplaceCell(new Cell(0, 7, new List<int> { 5, 7 }));
            board.ReplaceCell(new Cell(0, 8, new List<int> { 5, 3 }));
            OnlyOptionInColumnSolver solver = new OnlyOptionInColumnSolver(board);

            List<Cell> result = solver.Find();
            Assert.Single(result);
            Assert.Equal(4, result.First().Number);
        }
    }
}
