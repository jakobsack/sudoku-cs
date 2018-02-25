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
                        Value = 0,
                        X = col,
                        Y = row,
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

        public void UpdateCandidates(Cell changedCell)
        {
            List<Cell> cellsToCheck = new List<Cell>();
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (column == changedCell.X ||
                        row == changedCell.Y ||
                        (column / 3 == changedCell.QuadX &&
                        row / 3 == changedCell.QuadY))
                    {
                        cellsToCheck.Add(Cells[CellPosition(column, row)]);
                    }
                }
            }

            foreach (Cell cell in cellsToCheck)
            {
                if (!cell.Candidates.Contains(changedCell.Value)) continue;
                cell.Candidates.Remove(changedCell.Value);
            }
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

            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < 3; row++)
                {
                    cells.Add(GetCell(column + 3 * quadX, row + 3 * quadY));
                }
            }

            return cells;
        }

        public int UnknownValues()
        {
            return Cells.Where(x => x.Value == 0).Count();
        }

        public List<Cell> AllCells()
        {
            return Cells.Select(x => new Cell(x)).ToList();
        }

        public bool NumberInRow(int row, int number)
        {
            return Row(row).Exists(x => x.Value == number);
        }

        public bool NumberInColumn(int column, int number)
        {
            return Column(column).Exists(x => x.Value == number);
        }

        public bool NumberInQuad(int quadX, int quadY, int number)
        {
            return Quad(quadX, quadY).Exists(x => x.Value == number);
        }

        public Cell GetCell(int x, int y)
        {
            return new Cell(Cells[CellPosition(x, y)]);
        }

        public void InsertCell(Cell cell)
        {
            Cell oldCell = GetCell(cell.X, cell.Y);

            if (oldCell.Value != 0 && oldCell.Value != cell.Value)
            {
                throw new Exception("Cannot change value of cell");
            }

            // Because we update the candidates on the fly it is possible that we get old candidates
            cell.Candidates.RemoveAll(x => !oldCell.Candidates.Contains(x));
            Cells[CellPosition(cell.X, cell.Y)] = cell;

            if (oldCell.Value != cell.Value)
            {
                UpdateCandidates(cell);
            }
        }

        public void InsertCells(IEnumerable<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                InsertCell(cell);
            }
        }

        private int CellPosition(int column, int row)
        {
            return row * 9 + column;
        }
    }
}
