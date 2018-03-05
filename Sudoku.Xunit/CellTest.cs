using System;
using Xunit;
using Sudoku;
using System.Collections.Generic;
using System.Linq;

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

        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(0, 2, 0, 0)]
        [InlineData(0, 3, 0, 1)]
        [InlineData(2, 0, 0, 0)]
        [InlineData(3, 0, 1, 0)]
        [InlineData(3, 3, 1, 1)]
        public void QuadTest(int cellColumn, int cellRow, int expectedColumn, int expectedRow)
        {
            Cell testCell = new Cell(cellColumn, cellRow);
            Assert.Equal((expectedColumn, expectedRow), testCell.Quad);
        }

        [Fact]
        public void CloneCellTest()
        {
            Cell testCell = new Cell(4, 5)
            {
                Number = 5
            };
            Cell clonedCell = new Cell(testCell);

            Assert.Equal(testCell.Column, clonedCell.Column);
            Assert.Equal(testCell.Row, clonedCell.Row);
            Assert.Equal(testCell.Number, clonedCell.Number);
            Assert.Equal(testCell.Candidates, clonedCell.Candidates);
        }

        [Fact]
        public void NumberClearsCandidatesTest()
        {
            Cell testCell = new Cell(4, 5);
            Assert.NotEmpty(testCell.Candidates);

            testCell.Number = 5;
            Assert.Empty(testCell.Candidates);
        }
    }
}
