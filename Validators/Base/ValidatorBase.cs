using System;
using System.Collections.Generic;
using System.Linq;

namespace Validators
{
    public abstract class ValidatorBase<T>
    {
        public class Rule
        {
            public Func<T, bool> Test { get; set; }
            public string Message { get; set; }
        }

        protected abstract IEnumerable<Rule> Rules { get; }

        public bool Validate(T t)
        {
            return Rules.All(r => r.Test(t));
        }
    }
}
