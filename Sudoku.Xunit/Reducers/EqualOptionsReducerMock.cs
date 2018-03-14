using System;
using Sudoku;
using Sudoku.Reducers;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Xunit.Reducers
{
    public class EqualOptionsReducerMock : EqualOptionsReducer
    {
        public EqualOptionsReducerMock(Board field) : base(field)
        {
        }

        public override List<Cell> Find()
        {
            throw new NotImplementedException();
        }
    }
}
