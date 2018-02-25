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
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        private int number;
        public int Value
        {
            get { return number; }
            set
            {
                number = value;

                if (value != 0)
                {
                    Candidates = new List<int>();
                }
            }
        }

        public List<int> Candidates { get; set; }

        public int QuadX
        {
            get
            {
                return X / 3;
            }
        }

        public int QuadY
        {
            get
            {
                return Y / 3;
            }
        }

        public Cell()
        {
            X = 0;
            Y = 0;
            Value = 0;
            Candidates = new List<int>();
        }

        public Cell(Cell cell)
        {
            X = cell.X;
            Y = cell.Y;
            Value = cell.Value;
            Candidates = cell.Candidates.ToList();
        }
    }
}
