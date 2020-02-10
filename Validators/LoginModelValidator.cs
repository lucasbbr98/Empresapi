using Models.Requests;
using System.Collections.Generic;

namespace Validators
{
    using static ValidatorMethods;

    public class LoginModelValidator : ValidatorBase<LoginModel>
    {
        protected override IEnumerable<Rule> Rules
        {
            get
            {
                return new[] {
                    new Rule { Test = m => IsValidEmail(m.Email)},
                    new Rule { Test = m => IsValidPassword(m.Password)},
                };
            }
        }
    }
}
