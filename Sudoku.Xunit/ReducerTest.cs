using System;
using Xunit;
using Sudoku;
using Sudoku.Solvers;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Xunit
{
    public class ReducerTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            ReducerMock solver = new ReducerMock(board, new List<Cell>());
        }

        [Fact]
        public void FindWithoutResultTest()
        {
            Board board = new Board();
            ReducerMock solver = new ReducerMock(board, new List<Cell>());
            bool result = solver.Run();

            Assert.False(result);
        }

        [Fact]
        public void FindWithResultTest()
        {
            Board board = new Board();
            ReducerMock solver = new ReducerMock(board, new List<Cell> { new Cell(5, 6) { Candidates = new List<int> { 6 } } });
            bool result = solver.Run();

            Assert.True(result);
            Assert.Equal(new List<int> { 6 }, board.GetCell(5, 6).Candidates);
        }
    }
}
