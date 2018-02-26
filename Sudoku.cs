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
using Sudoku.Solvers;
using Sudoku.Reducers;

namespace Sudoku
{
    public class Sudoku
    {
        private Board Field;
        private int Iterations;
        private int InitialUnknown;
        private int MaxBacktrackLevel;

        private List<Cell> AssumedCells;

        public Sudoku()
        {
            Field = new Board();
            Iterations = 0;
            InitialUnknown = 0;
            MaxBacktrackLevel = 4;
            AssumedCells = new List<Cell>();
        }

        public void Input()
        {
            IEnumerable<string> lines = System.IO.File.ReadAllLines("example.txt");
            for (int row = 0; row < 9; row++)
            {
                string line = lines.ElementAt(row); //Console.ReadLine();
                IEnumerable<string> cells = line.Split(' ');

                for (int column = 0; column < 9; column++)
                {
                    int value = int.Parse(cells.ElementAt(column));
                    Cell cell = Field.GetCell(column, row);
                    cell.Number = value;
                    Field.ReplaceCell(cell);
                }
            }

            InitialUnknown = Field.UnknownValues();

            Console.WriteLine("Input:");
            PrintField();
        }

        public void Solve()
        {
            SolveHelper(Field);
            AssumedCells.Reverse();
        }

        private bool SolveHelper(Board board)
        {
            while (true)
            {
                Iterations++;

                bool repeatLoop = false;

                // Limit insertion candidates
                //repeatLoop = repeatLoop || new OtherRowsAreBlocked(board).Run();
                //repeatLoop = repeatLoop || new OtherColumnsAreBlocked(board).Run();
                repeatLoop = repeatLoop || new EqualOptionsInRowReducer(board).Run();
                repeatLoop = repeatLoop || new EqualOptionsInColumnReducer(board).Run();
                repeatLoop = repeatLoop || new EqualOptionsInQuadReducer(board).Run();

                // Fill cells
                repeatLoop = repeatLoop || new OnlyCandidateInCellSolver(board).Run();
                repeatLoop = repeatLoop || new OnlyOptionInRowSolver(board).Run();
                repeatLoop = repeatLoop || new OnlyOptionInColumnSolver(board).Run();
                repeatLoop = repeatLoop || new OnlyOptionInQuadSolver(board).Run();

                if (repeatLoop) continue;

                if (board.UnknownValues() == 0)
                {
                    return true;
                }
                else if (board.BacktrackLevel < MaxBacktrackLevel)
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
                                AssumedCells.Add(assumedCell);

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

        public void Output()
        {
            Console.WriteLine("Result:");
            PrintField();

            Console.WriteLine(String.Format("Board is valid: {0}", Field.Validate() ? "yes" : "no"));
            Console.WriteLine(String.Format("Iterations: {0}", Iterations));
            Console.WriteLine(String.Format("Determined {0} out of {1} unknowns", InitialUnknown - Field.UnknownValues(), InitialUnknown));
            Console.WriteLine();
            Console.WriteLine("{0} cells had to be guessed{1}", AssumedCells.Count, AssumedCells.Count == 0 ? "" : ":");
            foreach (Cell cell in AssumedCells)
            {
                Console.WriteLine("Number {0} at column {1}, row {2}", cell.Number, cell.Column, cell.Row);
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
