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
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class Reducer
    {
        public Reducer(Board board)
        {
            Field = board;
        }

        protected Board Field { get; set; }

        public bool Run()
        {
            List<Cell> cells = Find();

            Field.ReplaceCells(cells);

            return cells.Any();
        }

        public abstract List<Cell> Find();
    }
}
