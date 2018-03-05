using System;
using Sudoku;
using Sudoku.Solvers;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Xunit.Solvers
{
    public class OnlyOptionSolverMock : OnlyOptionSolver
    {
        public OnlyOptionSolverMock(Board field) : base(field)
        {
        }

        public override List<Cell> Find()
        {
            throw new NotImplementedException();
        }
    }
}
