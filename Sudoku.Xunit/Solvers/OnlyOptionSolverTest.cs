using System;
using Xunit;
using Sudoku;
using Sudoku.Solvers;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Xunit.Solvers
{
    public class OnlyOptionSolverTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Board board = new Board();
            OnlyOptionSolver solver = new OnlyOptionSolverMock(board);
        }

        [Fact]
        public void FindSinglesTest()
        {
            List<Cell> cells = new List<Cell>{
                new Cell(0, 0) { Candidates = new List<int> { 1, 2, 3 }},
                new Cell(1, 0) { Candidates = new List<int> { 1, 2 }},
                new Cell(2, 0) { Candidates = new List<int> { 1 }},
                new Cell(3, 0) { Candidates = new List<int>()}
            };

            Board board = new Board();
            OnlyOptionSolver solver = new OnlyOptionSolverMock(board);

            List<Cell> result = solver.FindSingles(cells);
            Assert.Single(result);
            Assert.Equal(3, result.First().Number);
        }
    }
}
