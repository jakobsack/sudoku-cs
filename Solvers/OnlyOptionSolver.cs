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
using System.IO;
using System.Threading.Tasks;
using Sudoku;

namespace Sudoku.Solvers
{
    public abstract class OnlyOptionSolver : Solver
    {
        public OnlyOptionSolver(Board field) : base(field)
        {

        }

        public List<Cell> FindSingles(List<Cell> cells)
        {
            List<Cell> singles = new List<Cell>();

            for (int number = 1; number <= 9; number++)
            {
                if(cells.Exists(x => x.Value == number)) continue;

                List<Cell> options = new List<Cell>();
                foreach(Cell cell in cells)
                {
                    if (cell.Candidates.Exists(x => x == number))
                    {
                        options.Add(cell);
                    }
                }

                if (options.Count != 1) continue;

                Cell singleCell = options.First();
                singleCell.Value = number;
                singles.Add(singleCell);
            }

            return singles;
        }
    }
}
