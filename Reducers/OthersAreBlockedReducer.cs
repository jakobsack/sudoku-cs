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
    public abstract class OthersAreBlockedReducer : Reducer
    {
        public OthersAreBlockedReducer(Board field) : base(field)
        {

        }

        public List<Cell> FindOtherwiseBlockedCells(List<List<Cell>> cellLists)
        {
            bool changes = false;
            List<(int, int)> availableQuads = cellLists[0].Select(x => x.Quad).Distinct().ToList();

            for (int number = 1; number <= 9; number++)
            {
                List<List<(int, int)>> quadOptions = new List<List<(int, int)>>();
                for (int lane = 0; lane < 3; lane++)
                {
                    // we add the number of all quads where it is possible to insert the number
                    quadOptions.Add(cellLists[lane].Where(x => x.Candidates.Contains(number)).Select(x => x.Quad).Distinct().ToList());
                }

                // The number exists in all rows
                if (!quadOptions.Any(x => x.Any())) continue;

                foreach ((int, int) quad in availableQuads)
                {
                    if (quadOptions.Where(x => x.Contains(quad)).Count() != 1) continue;

                    // only one quad has this number!
                    for (int lane = 0; lane < 3; lane++)
                    {
                        if (quadOptions[lane].Contains(quad))
                        {
                            // this is the lane with the quad. Remove candidates in other quads
                            foreach (Cell cell in cellLists[lane].Where(x => x.Candidates.Contains(number) && !x.Quad.Equals(quad)))
                            {
                                changes = true;
                                cell.Candidates.Remove(number);
                            }
                        }
                        else
                        {
                            // This is not the lane with the quad. Remove candidates in this quad
                            foreach (Cell cell in cellLists[lane].Where(x => x.Candidates.Contains(number) && x.Quad.Equals(quad)))
                            {
                                changes = true;
                                cell.Candidates.Remove(number);
                            }
                        }
                    }
                }
            }

            if (!changes) return new List<Cell>();

            List<Cell> cells = new List<Cell>();
            for (int row = 0; row < 3; row++)
            {
                cells.AddRange(cellLists[row]);
            }
            return cells;
        }
    }
}
