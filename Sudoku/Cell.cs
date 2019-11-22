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

namespace Sudoku
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Cell
    {
        private int number;

        public Cell(int column, int row, List<int> candidates = null)
        {
            Column = column;
            Row = row;
            Number = 0;
            Candidates = candidates ?? new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public Cell(Cell cell)
        {
            if (cell == null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            Column = cell.Column;
            Row = cell.Row;
            Number = cell.Number;
            Candidates = cell.Candidates.ToList();
        }

        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;

                if (value != 0)
                {
                    Candidates = new List<int>();
                }
            }
        }

        public int Column { get; }

        public int Row { get; }

        public List<int> Candidates { get; private set; }

        public int QuadColumn
        {
            get
            {
                return Column / 3;
            }
        }

        public int QuadRow
        {
            get
            {
                return Row / 3;
            }
        }

        public (int, int) Quad
        {
            get
            {
                return (QuadColumn, QuadRow);
            }
        }
    }
}
