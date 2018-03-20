namespace Sudoku.Test.Solvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sudoku.Solvers;
    using Xunit;

    public class OnlyOptionSolverMock : OnlyOptionSolver
    {
        public OnlyOptionSolverMock(Board field)
            : base(field)
        {
        }

        public override List<Cell> Find()
        {
            throw new NotImplementedException();
        }
    }
}
