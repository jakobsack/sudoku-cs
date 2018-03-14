using System;
using Xunit;
using Sudoku;
using Sudoku.Reducers;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Xunit.Reducers
{
    public class EqualOptionsReducerTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            EqualOptionsReducerMock solver = new EqualOptionsReducerMock(board);
        }

        [Fact]
        public void FindParallelCandidatesWithExclusiveParallelsTest()
        {
            List<Cell> cells = new List<Cell>{
                new Cell(0, 0) { Candidates = new List<int> { 1, 2, 3 }},
                new Cell(1, 0) { Candidates = new List<int> { 1, 2 }},
                new Cell(2, 0) { Candidates = new List<int> { 1, 2 }},
                new Cell(3, 0) { Candidates = new List<int> { 3, 4 }}
            };

            Board board = new Board();
            EqualOptionsReducerMock solver = new EqualOptionsReducerMock(board);

            List<Cell> result = solver.FindParallelCandidates(cells);
            Assert.Equal(4, result.Count());
            Assert.Equal(new List<int> { 3 }, result[0].Candidates);
            Assert.Equal(new List<int> { 1, 2 }, result[1].Candidates);
            Assert.Equal(new List<int> { 1, 2 }, result[2].Candidates);
            Assert.Equal(new List<int> { 3, 4 }, result[3].Candidates);
        }

        [Fact]
        public void FindParallelCandidatesWithExactParallelsTest()
        {
            List<Cell> cells = new List<Cell>{
                new Cell(0, 0) { Candidates = new List<int> { 3 }},
                new Cell(1, 0) { Candidates = new List<int> { 1, 2, 5, 6 }},
                new Cell(2, 0) { Candidates = new List<int> { 1, 2, 7, 8 }},
                new Cell(3, 0) { Candidates = new List<int> { 3, 4 }}
            };

            Board board = new Board();
            EqualOptionsReducerMock solver = new EqualOptionsReducerMock(board);

            List<Cell> result = solver.FindParallelCandidates(cells);
            Assert.Equal(4, result.Count());
            Assert.Equal(new List<int> { 3 }, result[0].Candidates);
            Assert.Equal(new List<int> { 1, 2 }, result[1].Candidates);
            Assert.Equal(new List<int> { 1, 2 }, result[2].Candidates);
            Assert.Equal(new List<int> { 3, 4 }, result[3].Candidates);
        }

        [Fact]
        public void FindParallelCandidatesWithNoParallelsTest()
        {
            List<Cell> cells = new List<Cell>{
                new Cell(0, 0) { Candidates = new List<int> { 1 }},
                new Cell(1, 0) { Candidates = new List<int> { 2 }},
                new Cell(2, 0) { Candidates = new List<int> { 3 }},
                new Cell(3, 0) { Candidates = new List<int> { 4 }}
            };

            Board board = new Board();
            EqualOptionsReducerMock solver = new EqualOptionsReducerMock(board);

            List<Cell> result = solver.FindParallelCandidates(cells);
            Assert.Empty(result);
        }
    }
}
