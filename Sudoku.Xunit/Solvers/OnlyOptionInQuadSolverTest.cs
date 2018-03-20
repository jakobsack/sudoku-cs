namespace Sudoku.Xunit.Solvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Sudoku.Solvers;
    using global::Xunit;

    public class OnlyOptionInQuadSolverTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            OnlyOptionInQuadSolver solver = new OnlyOptionInQuadSolver(board);
        }

        [Fact]
        public void FindSinglesTest()
        {
            Board board = new Board();
            board.ReplaceCell(new Cell(0, 0, new List<int> { 3 }));
            board.ReplaceCell(new Cell(1, 0, new List<int> { 3, 4 }));
            board.ReplaceCell(new Cell(2, 0, new List<int> { 5, 3 }));
            board.ReplaceCell(new Cell(0, 1, new List<int> { 5 }));
            board.ReplaceCell(new Cell(1, 1, new List<int> { 6, 3 }));
            board.ReplaceCell(new Cell(2, 1, new List<int> { 7, 3 }));
            board.ReplaceCell(new Cell(0, 2, new List<int> { 5, 6 }));
            board.ReplaceCell(new Cell(1, 2, new List<int> { 5, 7 }));
            board.ReplaceCell(new Cell(2, 2, new List<int> { 5, 3 }));
            OnlyOptionInQuadSolver solver = new OnlyOptionInQuadSolver(board);

            List<Cell> result = solver.Find();
            Assert.Single(result);
            Assert.Equal(4, result.First().Number);
        }
    }
}
