// Copyright (C) 2018 Jakob Sack <mail@jakobsack.de>
//
// This file is part of Sudoku.
//
// Sudoku is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Sudoku is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Sudoku.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Sudoku
{
    /// <summary>
    /// This class contains the board with all its cells.
    /// </summary>
    public class Board
    {
        private List<Cell> Cells;

        public int BacktrackLevel { private set; get; }

        public Board()
        {
            BacktrackLevel = 0;

            Cells = new List<Cell>();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Cells.Add(new Cell()
                    {
                        Number = 0,
                        Column = col,
                        Row = row,
                        Candidates = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                    });
                }
            }
        }

        public Board(Board board)
        {
            BacktrackLevel = board.BacktrackLevel + 1;

            Cells = new List<Cell>();
            foreach (Cell cell in board.Cells)
            {
                Cells.Add(new Cell(cell));
            }
        }

        public List<Cell> AllCells()
        {
            return Cells.Select(x => new Cell(x)).ToList();
        }

        public List<Cell> Row(int row)
        {
            List<Cell> cells = new List<Cell>();

            for (int column = 0; column < 9; column++)
            {
                cells.Add(GetCell(column, row));
            }

            return cells;
        }

        public List<Cell> Column(int column)
        {
            List<Cell> cells = new List<Cell>();

            for (int row = 0; row < 9; row++)
            {
                cells.Add(GetCell(column, row));
            }

            return cells;
        }

        public List<Cell> Quad(int quadX, int quadY)
        {
            List<Cell> cells = new List<Cell>();

            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    cells.Add(GetCell(column + 3 * quadX, row + 3 * quadY));
                }
            }

            return cells;
        }

        /// <summary>
        /// Calculates the number of cells with unknown number.
        /// </summary>
        /// <returns>Number of cells with unknown number</returns>
        public int UnknownValues()
        {
            return Cells.Where(x => x.Number == 0).Count();
        }

        /// <summary>
        /// Returns a clone of the cell at the given position
        /// </summary>
        /// <param name="column">Column in the grid</param>
        /// <param name="row">Row on the grid</param>
        /// <returns>A clone of the cell</returns>
        public Cell GetCell(int column, int row)
        {
            return new Cell(Cells[CellPosition(column, row)]);
        }

        /// <summary>
        /// Replaces a cell in the grid.
        ///
        /// Before inserting the cell in the grid all candidates that were not part of the previous cell are removed.
        /// </summary>
        /// <param name="cell">Cell with the new data</param>
        /// <exception cref="Exception">Thrown if previous cell contained a number but new one does not.</exception>
        public void ReplaceCell(Cell cell)
        {
            Cell oldCell = GetCell(cell.Column, cell.Row);

            if (oldCell.Number != 0 && oldCell.Number != cell.Number)
            {
                throw new Exception("Cannot change value of cell");
            }

            if (cell.Number != 0)
            {
                cell.Candidates.Clear();
            }

            // Because we update the candidates on the fly it is possible that we get old candidates
            cell.Candidates.RemoveAll(x => !oldCell.Candidates.Contains(x));
            Cells[CellPosition(cell.Column, cell.Row)] = cell;

            if (oldCell.Number != cell.Number)
            {
                UpdateCandidates(cell);
            }
        }

        /// <summary>
        /// Replaces several cells in the grid.
        ///
        /// See <see cref="ReplaceCell" /> for more information
        /// </summary>
        /// <param name="cells">Collection of the new cells</param>
        public void ReplaceCells(IEnumerable<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                ReplaceCell(cell);
            }
        }

        public bool Validate()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!ValidateCells(Row(i))) return false;
                if (!ValidateCells(Column(i))) return false;
            }

            for (int quadY = 0; quadY < 3; quadY++)
            {
                for (int quadX = 0; quadX < 3; quadX++)
                {
                    if (!ValidateCells(Quad(quadX, quadY))) return false;
                }
            }

            return true;
        }

        private bool ValidateCells(List<Cell> cells)
        {
            for (int number = 1; number <= cells.Count; number++)
            {
                if (cells.Where(x => x.Number == number).Count() > 1) return false;
            }
            return true;
        }

        /// <summary>
        /// Removes the number of the changed cell from the list of candidates in all affected cells.
        /// </summary>
        /// <param name="changedCell">Cell with the updated values</param>
        private void UpdateCandidates(Cell changedCell)
        {
            List<Cell> cellsToCheck = new List<Cell>();
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (column == changedCell.Column ||
                        row == changedCell.Row ||
                        (column / 3 == changedCell.QuadColumn && row / 3 == changedCell.QuadRow))
                    {
                        cellsToCheck.Add(Cells[CellPosition(column, row)]);
                    }
                }
            }

            foreach (Cell cell in cellsToCheck)
            {
                cell.Candidates.Remove(changedCell.Number);
            }
        }

        /// <summary>
        /// Calculates the linear position of the Cell
        /// </summary>
        /// <param name="column">Column in the grid</param>
        /// <param name="row">Row on the grid</param>
        /// <returns>Int</returns>
        private int CellPosition(int column, int row)
        {
            return row * 9 + column;
        }
    }
}
