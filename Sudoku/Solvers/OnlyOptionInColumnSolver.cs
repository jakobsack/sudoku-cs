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

namespace Sudoku.Solvers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class OnlyOptionInColumnSolver : OnlyOptionSolver
    {
        public OnlyOptionInColumnSolver(Board field)
            : base(field)
        {
        }

        public override List<Cell> Find()
        {
            List<Cell> cells = new List<Cell>();

            for (int column = 0; column < 9; column++)
            {
                cells.AddRange(FindSingles(Field.Column(column)));
            }

            return cells;
        }
    }
}
