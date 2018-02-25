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

namespace Sudoku.Reducers
{
    public class OtherColumnsAreBlocked : Reducer
    {
        public OtherColumnsAreBlocked(Board field) : base(field)
        {

        }

        public override List<Cell> Find()
        {
            List<Cell> cells = new List<Cell>();

            for (int quadX = 0; quadX < 3; quadX++)
            {
                List<List<Cell>> quadColumnCells = new List<List<Cell>>();
                for (int row = 0; row < 3; row++)
                {
                    quadColumnCells.Add(Field.Column(quadX * 3 + row));
                }

                bool changes = false;
                for (int number = 1; number <= 9; number++)
                {
                    List<List<int>> quadOptions = new List<List<int>>();
                    for (int column = 0; column < 3; column++)
                    {
                        // we add the number of all quads where it is possible to insert the number
                        quadOptions.Add(quadColumnCells[column].Where(x => x.Candidates.Exists(y => y == number)).Select(x => x.QuadY).Distinct().ToList());
                    }

                    // The number exists in all rows
                    if (!quadOptions.Any(x => x.Any())) continue;

                    // Here we know in which quads the number fits
                    for (int quadY = 0; quadY < 3; quadY++)
                    {
                        if(quadOptions.Where(x => x.Exists(y => y == quadY)).Count() == 1){
                            for(int row = 0; row < 3; row++){
                                if(!quadOptions[row].Exists(x => x == quadY)) continue;

                                foreach(Cell cell in quadColumnCells[row].Where(x => x.Candidates.Exists(y => y == number) && x.QuadY != quadY)){
                                    changes = true;
                                    cell.Candidates.Remove(number);
                                }
                            }
                        }
                    }
                }

                if (!changes) continue;

                for (int row = 0; row < 3; row++)
                {
                    cells.AddRange(quadColumnCells[row]);
                }
            }

            return cells;
        }
    }
}
