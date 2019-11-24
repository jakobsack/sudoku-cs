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

namespace Sudoku.Reducers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class EqualOptionsReducer : Reducer
    {
        public EqualOptionsReducer(Board field)
            : base(field)
        {
        }

        public static List<Cell> FindParallelCandidates(List<Cell> cells)
        {
            if (cells == null)
            {
                throw new ArgumentNullException(nameof(cells));
            }

            bool changed = false;

            List<int> processedNumbers = new List<int>();
            for (int number = 1; number <= 9; number++)
            {
                if (processedNumbers.Contains(number))
                {
                    continue;
                }

                List<Cell> cellsForNumber = cells.Where(x => x.Candidates.Contains(number)).ToList();
                if (cellsForNumber.Count < 2)
                {
                    continue;
                }

                List<int> parallelNumbers = new List<int>();
                for (int otherNumber = 1; otherNumber <= 9; otherNumber++)
                {
                    if (processedNumbers.Contains(otherNumber))
                    {
                        continue;
                    }

                    List<Cell> cellsForOtherNumber = cells.Where(x => x.Candidates.Contains(otherNumber)).ToList();
                    if (AreListsEqual(cellsForNumber, cellsForOtherNumber))
                    {
                        parallelNumbers.Add(otherNumber);
                    }
                }

                if (parallelNumbers.Count < 2 || parallelNumbers.Count != cellsForNumber.Count)
                {
                    continue;
                }

                foreach (Cell cell in cellsForNumber)
                {
                    if (cell.Candidates.Count > parallelNumbers.Count)
                    {
                        changed = true;
                        cell.Candidates.RemoveAll(x => !parallelNumbers.Contains(x));
                    }
                }

                processedNumbers.AddRange(parallelNumbers);
            }

            List<int> remainingCandidates = cells.SelectMany(x => x.Candidates).Distinct().ToList();
            remainingCandidates.Sort();

            List<List<int>> parallelOptions = PermutatePartParallelCells(remainingCandidates);
            foreach (List<int> list in parallelOptions)
            {
                List<Cell> parallelCells = cells.Where(x => AreIntListsEqual(x.Candidates, list)).ToList();
                if (parallelCells.Count != list.Count)
                {
                    continue;
                }

                bool changedCells = false;
                foreach (Cell cell in cells)
                {
                    if (!AreIntListsEqual(cell.Candidates, list))
                    {
                        int before = cell.Candidates.Count;
                        cell.Candidates.RemoveAll(x => list.Contains(x));
                        if (cell.Candidates.Count != before)
                        {
                            changedCells = true;
                            changed = true;
                        }
                    }
                }

                if (changedCells)
                {
                    break;
                }
            }

            if (changed)
            {
                return cells;
            }

            return new List<Cell>();
        }

        private static List<List<int>> PermutatePartParallelCells(List<int> partParallelNumbers)
        {
            return PermutatePartParallelCells(partParallelNumbers, new List<int>());
        }

        private static List<List<int>> PermutatePartParallelCells(List<int> partParallelNumbers, List<int> existingList)
        {
            List<List<int>> list = new List<List<int>>();
            foreach (int partParallelNumber in partParallelNumbers)
            {
                List<int> newList = existingList.Append(partParallelNumber).ToList();
                if (newList.Count > 1)
                {
                    list.Add(newList);
                }

                List<int> remainingNumbers = partParallelNumbers.Where(x => x > partParallelNumber).ToList();
                if (remainingNumbers.Any())
                {
                    list.AddRange(PermutatePartParallelCells(remainingNumbers, newList));
                }
            }

            return list;
        }

        private static bool AreListsEqual(List<Cell> left, List<Cell> right)
        {
            if (left.Count != right.Count)
            {
                return false;
            }

            for (int i = 0; i < left.Count; i++)
            {
                if (left[i].Column != right[i].Column)
                {
                    return false;
                }

                if (left[i].Row != right[i].Row)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AreIntListsEqual(List<int> left, List<int> right)
        {
            if (left.Count != right.Count)
            {
                return false;
            }

            foreach (int number in right)
            {
                if (!left.Contains(number))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
