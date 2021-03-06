namespace Sudoku.Test.Solvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sudoku.Solvers;
    using Xunit;

    public class OnlyOptionInRowSolverTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            OnlyOptionInRowSolver solver = new OnlyOptionInRowSolver(board);
        }

        [Fact]
        public void FindSinglesTest()
        {
            Board board = new Board();
            board.ReplaceCell(new Cell(0, 0, new List<int> { 3 }));
            board.ReplaceCell(new Cell(1, 0, new List<int> { 3, 4 }));
            board.ReplaceCell(new Cell(2, 0, new List<int> { 5, 3 }));
            board.ReplaceCell(new Cell(3, 0, new List<int> { 5 }));
            board.ReplaceCell(new Cell(4, 0, new List<int> { 6, 3 }));
            board.ReplaceCell(new Cell(5, 0, new List<int> { 7, 3 }));
            board.ReplaceCell(new Cell(6, 0, new List<int> { 5, 6 }));
            board.ReplaceCell(new Cell(7, 0, new List<int> { 5, 7 }));
            board.ReplaceCell(new Cell(8, 0, new List<int> { 5, 3 }));
            OnlyOptionInRowSolver solver = new OnlyOptionInRowSolver(board);

            List<Cell> result = solver.Find();
            Assert.Single(result);
            Assert.Equal(4, result.First().Number);
        }
    }
}
