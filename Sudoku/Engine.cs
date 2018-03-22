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
    using Reducers;
    using Solvers;

    public class Engine
    {
        private int iterations;
        private int initialUnknown;
        private int maxBacktrackLevel;

        private List<Cell> assumedCells;

        public Engine()
        {
            Field = new Board();
            iterations = 0;
            initialUnknown = 0;
            maxBacktrackLevel = 1;
            assumedCells = new List<Cell>();
        }

        private Board Field { get; set; }

        public void Input()
        {
            IEnumerable<string> lines = System.IO.File.ReadAllLines("example.txt");
            for (int row = 0; row < 9; row++)
            {
                string line = lines.ElementAt(row); // Console.ReadLine();
                IEnumerable<string> cells = line.Split(' ');

                for (int column = 0; column < 9; column++)
                {
                    int value = int.Parse(cells.ElementAt(column));
                    Cell cell = Field.GetCell(column, row);
                    cell.Number = value;
                    Field.ReplaceCell(cell);
                }
            }

            initialUnknown = Field.UnknownValues();

            Console.WriteLine("Input:");
            PrintField();
        }

        public void Solve()
        {
            SolveHelper(Field);
            assumedCells.Reverse();
        }

        public void Output()
        {
            Console.WriteLine("Result:");
            PrintField();

            bool isValid = Field.Validate();

            Console.WriteLine("Board is valid: {0}", isValid ? "yes" : "no");
            Console.WriteLine("Iterations: {0}", iterations);
            Console.WriteLine("Determined {0} out of {1} unknowns", initialUnknown - Field.UnknownValues(), initialUnknown);
            Console.WriteLine();
            if (isValid)
            {
                Console.WriteLine("{0} cells had to be guessed{1}", assumedCells.Count, assumedCells.Count == 0 ? String.Empty : ":");
                foreach (Cell cell in assumedCells)
                {
                    Console.WriteLine("Number {0} at column {1}, row {2}", cell.Number, cell.Column, cell.Row);
                }
            }
            else
            {
                Console.WriteLine("Guessed up to {0} cells, but this did not work out.", maxBacktrackLevel);
            }
        }

        private bool SolveHelper(Board board)
        {
            // Create Reducers
            OtherRowsAreBlockedReducer otherRowsAreBlockedReducer = new OtherRowsAreBlockedReducer(board);
            OtherColumnsAreBlockedReducer otherColumnsAreBlockedReducer = new OtherColumnsAreBlockedReducer(board);
            EqualOptionsInRowReducer equalOptionsInRowReducer = new EqualOptionsInRowReducer(board);
            EqualOptionsInColumnReducer equalOptionsInColumnReducer = new EqualOptionsInColumnReducer(board);
            EqualOptionsInQuadReducer equalOptionsInQuadReducer = new EqualOptionsInQuadReducer(board);

            // Create Solvers
            OnlyCandidateInCellSolver onlyCandidateInCellSolver = new OnlyCandidateInCellSolver(board);
            OnlyOptionInRowSolver onlyOptionInRowSolver = new OnlyOptionInRowSolver(board);
            OnlyOptionInColumnSolver onlyOptionInColumnSolver = new OnlyOptionInColumnSolver(board);
            OnlyOptionInQuadSolver onlyOptionInQuadSolver = new OnlyOptionInQuadSolver(board);

            while (true)
            {
                iterations++;

                bool repeatLoop = false;

                // Limit insertion candidates
                repeatLoop = repeatLoop || otherRowsAreBlockedReducer.Run();
                repeatLoop = repeatLoop || otherColumnsAreBlockedReducer.Run();
                repeatLoop = repeatLoop || equalOptionsInRowReducer.Run();
                repeatLoop = repeatLoop || equalOptionsInColumnReducer.Run();
                repeatLoop = repeatLoop || equalOptionsInQuadReducer.Run();

                // Fill cells
                repeatLoop = repeatLoop || onlyCandidateInCellSolver.Run();
                repeatLoop = repeatLoop || onlyOptionInRowSolver.Run();
                repeatLoop = repeatLoop || onlyOptionInColumnSolver.Run();
                repeatLoop = repeatLoop || onlyOptionInQuadSolver.Run();

                if (repeatLoop)
                {
                    continue;
                }

                if (board.UnknownValues() == 0)
                {
                    return true;
                }
                else if (board.BacktrackLevel < maxBacktrackLevel)
                {
                    foreach (Cell cell in board.AllCells())
                    {
                        foreach (int candidate in cell.Candidates)
                        {
                            Board newBoard = new Board(board);
                            Cell assumedCell = new Cell(cell);
                            assumedCell.Number = candidate;
                            newBoard.ReplaceCell(assumedCell);

                            if (SolveHelper(newBoard))
                            {
                                assumedCells.Add(assumedCell);

                                foreach (Cell solvedCell in newBoard.AllCells())
                                {
                                    board.ReplaceCell(solvedCell);
                                }

                                return true;
                            }
                        }
                    }
                }

                return false;
            }
        }

        private void PrintField()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    int value = Field.GetCell(col, row).Number;

                    Console.Write(value);
                    Console.Write(' ');

                    if (col % 3 == 2)
                    {
                        Console.Write(' ');
                    }
                }

                Console.Write(Environment.NewLine);

                if (row % 3 == 2)
                {
                    Console.Write(Environment.NewLine);
                }
            }
        }
    }
}
