namespace Sudoku.Test.Reducers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sudoku.Reducers;

    public class EqualOptionsReducerMock : EqualOptionsReducer
    {
        public EqualOptionsReducerMock(Board field)
            : base(field)
        {
        }

        public override List<Cell> Find()
        {
            throw new NotImplementedException();
        }
    }
}
