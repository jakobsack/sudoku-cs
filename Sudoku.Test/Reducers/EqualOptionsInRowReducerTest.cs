namespace Sudoku.Test.Reducers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sudoku.Reducers;
    using Xunit;

    public class EqualOptionsInRowReducerTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            EqualOptionsInRowReducer solver = new EqualOptionsInRowReducer(board);
        }

        [Fact]
        public void FindParallelCandidatesWithExclusiveParallelsTest()
        {
            Board board = new Board();
            board.ReplaceCell(new Cell(0, 0, new List<int> { 3, 2 }));
            board.ReplaceCell(new Cell(1, 0, new List<int> { 3, 4 }));
            board.ReplaceCell(new Cell(2, 0, new List<int> { 5, 3 }));
            board.ReplaceCell(new Cell(3, 0, new List<int> { 9 }));
            board.ReplaceCell(new Cell(4, 0, new List<int> { 3, 6 }));
            board.ReplaceCell(new Cell(5, 0, new List<int> { 3, 4 }));
            board.ReplaceCell(new Cell(6, 0, new List<int> { 5, 6 }));
            board.ReplaceCell(new Cell(7, 0, new List<int> { 5, 7 }));
            board.ReplaceCell(new Cell(8, 0, new List<int> { 3, 8 }));
            EqualOptionsInRowReducer solver = new EqualOptionsInRowReducer(board);

            List<Cell> result = solver.Find();
            Assert.Equal(9, result.Count());
            Assert.Equal(new List<int> { 2 }, result[0].Candidates);
            Assert.Equal(new List<int> { 3, 4 }, result[1].Candidates);
            Assert.Equal(new List<int> { 5 }, result[2].Candidates);
            Assert.Equal(new List<int> { 9 }, result[3].Candidates);
            Assert.Equal(new List<int> { 6 }, result[4].Candidates);
            Assert.Equal(new List<int> { 3, 4 }, result[5].Candidates);
            Assert.Equal(new List<int> { 5, 6 }, result[6].Candidates);
            Assert.Equal(new List<int> { 5, 7 }, result[7].Candidates);
            Assert.Equal(new List<int> { 8 }, result[8].Candidates);
        }
    }
}
