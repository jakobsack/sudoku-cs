namespace Sudoku.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sudoku.Solvers;
    using Xunit;

    public class SolverTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            SolverMock solver = new SolverMock(board, new List<Cell>());
        }

        [Fact]
        public void FindWithoutResultTest()
        {
            Board board = new Board();
            SolverMock solver = new SolverMock(board, new List<Cell>());
            bool result = solver.Run();

            Assert.False(result);
        }

        [Fact]
        public void FindWithResultTest()
        {
            Board board = new Board();
            SolverMock solver = new SolverMock(board, new List<Cell> { new Cell(5, 6) { Number = 6 } });
            bool result = solver.Run();

            Assert.True(result);
            Assert.Equal(6, board.GetCell(5, 6).Number);
        }
    }
}
