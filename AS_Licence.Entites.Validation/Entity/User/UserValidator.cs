using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace AS_Licence.Entites.Validation.Entity.User
{
  public class UserValidator : AbstractValidator<Entities.Model.User.User>
  {
    public UserValidator()
    {
      RuleFor(x => x.UserName).NotNull().NotEmpty().MinimumLength(0);
    }
  }
}
