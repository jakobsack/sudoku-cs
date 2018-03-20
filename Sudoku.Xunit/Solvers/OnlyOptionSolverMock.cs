namespace Sudoku.Xunit.Solvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Sudoku.Solvers;
    using global::Xunit;

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
