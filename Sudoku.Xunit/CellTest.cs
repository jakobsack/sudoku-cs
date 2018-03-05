using System;
using Xunit;
using Sudoku;

namespace Sudoku.Xunit
{
    public class CellTest
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(2, 0, 0)]
        [InlineData(3, 0, 1)]
        [InlineData(0, 3, 0)]
        public void QuadColumnTest(int cellColumn, int cellRow, int expectedResult)
        {
            Cell testCell = new Cell(cellColumn, cellRow);
            Assert.Equal(expectedResult, testCell.QuadColumn);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 2, 0)]
        [InlineData(0, 3, 1)]
        [InlineData(3, 0, 0)]
        public void QuadRowTest(int cellColumn, int cellRow, int expectedResult)
        {
            Cell testCell = new Cell(cellColumn, cellRow);
            Assert.Equal(expectedResult, testCell.QuadRow);
        }
    }
}
