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

    public class OnlyCandidateInCellSolver : Solver
    {
        public OnlyCandidateInCellSolver(Board field)
            : base(field)
        {
        }

        public override List<Cell> Find()
        {
            List<Cell> cells = new List<Cell>();

            foreach (Cell cell in Field.AllCells().Where(x => x.Candidates.Count == 1))
            {
                cell.Number = cell.Candidates.First();
                cells.Add(cell);
            }

            return cells;
        }
    }
}
