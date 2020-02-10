using Models.Requests;
using System.Collections.Generic;

namespace Validators
{
    using static ValidatorMethods;
    public class RegisterModelValidator : ValidatorBase<RegisterModel>
    {
        protected override IEnumerable<Rule> Rules
        {
            get
            {
                return new[] {
                    new Rule { Test = m => IsValidEmail(m.Email) },
                    new Rule { Test = m => IsValidPassword(m.Password)},
                    new Rule { Test = m => !string.IsNullOrEmpty(m.Name)}
                };
            }
        }
    }
}
