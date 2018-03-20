namespace Sudoku.Xunit.Solvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Sudoku.Solvers;
    using global::Xunit;

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
            List<Cell> cells = new List<Cell>
            {
                new Cell(0, 0, new List<int> { 1, 2, 3 }),
                new Cell(1, 0, new List<int> { 1, 2 }),
                new Cell(2, 0, new List<int> { 1 }),
                new Cell(3, 0, new List<int>()),
            };

            Board board = new Board();
            OnlyOptionSolver solver = new OnlyOptionSolverMock(board);

            List<Cell> result = OnlyOptionSolver.FindSingles(cells);
            Assert.Single(result);
            Assert.Equal(3, result.First().Number);
        }
    }
}
