namespace Sudoku.Test.Reducers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sudoku.Reducers;
    using Xunit;

    public class EqualOptionsInQuadReducerTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            EqualOptionsInQuadReducer solver = new EqualOptionsInQuadReducer(board);
        }

        [Fact]
        public void FindParallelCandidatesWithExclusiveParallelsTest()
        {
            Board board = new Board();
            board.ReplaceCell(new Cell(0, 0, new List<int> { 3, 2 }));
            board.ReplaceCell(new Cell(1, 0, new List<int> { 3, 4 }));
            board.ReplaceCell(new Cell(2, 0, new List<int> { 5, 3 }));
            board.ReplaceCell(new Cell(0, 1, new List<int> { 9 }));
            board.ReplaceCell(new Cell(1, 1, new List<int> { 3, 6 }));
            board.ReplaceCell(new Cell(2, 1, new List<int> { 3, 4 }));
            board.ReplaceCell(new Cell(0, 2, new List<int> { 5, 6 }));
            board.ReplaceCell(new Cell(1, 2, new List<int> { 5, 7 }));
            board.ReplaceCell(new Cell(2, 2, new List<int> { 3, 8 }));
            EqualOptionsInQuadReducer solver = new EqualOptionsInQuadReducer(board);

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
