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
    public abstract class EqualOptionsReducer : Reducer
    {
        public EqualOptionsReducer(Board field) : base(field)
        {

        }

        public List<Cell> FindParallelCandidates(List<Cell> cells)
        {
            Dictionary<int, List<int>> cellsForNumber = new Dictionary<int, List<int>>();
            for (int number = 1; number <= 9; number++)
            {
                cellsForNumber[number] = new List<int>();
                for (int i = 0; i < cells.Count; i++)
                {
                    if (cells[i].Candidates.Contains(number))
                    {
                        cellsForNumber[number].Add(i);
                    }
                }
            }

            bool changed = false;

            List<int> processedNumbers = new List<int>();
            for (int number = 1; number <= 9; number++)
            {
                if (processedNumbers.Contains(number)) continue;
                if (cellsForNumber[number].Count < 2) continue;

                List<int> parallelNumbers = new List<int> { number };
                for (int otherNumber = number + 1; otherNumber <= 9; otherNumber++)
                {
                    if (processedNumbers.Contains(otherNumber)) continue;

                    if (AreListsEqual(cellsForNumber[number], cellsForNumber[otherNumber]))
                    {
                        parallelNumbers.Add(otherNumber);
                    }
                }

                // a) number of options == number of parallels
                if (parallelNumbers.Count == cellsForNumber[number].Count)
                {
                    foreach (int i in cellsForNumber[number])
                    {
                        if (cells[i].Candidates.Count > parallelNumbers.Count)
                        {
                            changed = true;
                            cells[i].Candidates = parallelNumbers;
                        }
                    }
                    processedNumbers.AddRange(parallelNumbers);

                    continue;
                }

                // b) we have more cells than parallel numbers, but count(parallels) cells are exclusive
                List<int> exclusiveParallelCells = cellsForNumber[number].Where(x => cells[x].Candidates.Count == parallelNumbers.Count).ToList();
                if (exclusiveParallelCells.Count != parallelNumbers.Count) continue;

                foreach (int cell in cellsForNumber[number])
                {
                    if (cells[cell].Candidates.Count > parallelNumbers.Count)
                    {
                        changed = true;
                        foreach (int removeNumber in parallelNumbers)
                        {
                            cells[cell].Candidates.Remove(removeNumber);
                        }
                    }
                }
                processedNumbers.AddRange(parallelNumbers);
            }

            if (changed) return cells;

            return new List<Cell>();
        }

        private bool AreListsEqual(List<int> left, List<int> right)
        {
            if (left.Count != right.Count) return false;

            for (int i = 0; i < left.Count; i++)
            {
                if (left[i] != right[i]) return false;
            }

            return true;
        }
    }
}
