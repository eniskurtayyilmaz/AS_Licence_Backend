using System;
using System.Collections.Generic;
using System.Text;
using AS_Licence.Entites.Dto;
using FluentValidation;

namespace AS_Licence.Entites.Validation.Dto.User
{
 public class UserForPasswordChangeDtoValidators : AbstractValidator<UserForPasswordChangeDto>
  {
    public UserForPasswordChangeDtoValidators()
    {
      RuleFor(x => x.CurrentPassword).NotNull().NotEmpty().MinimumLength(5);
      RuleFor(x => x.NewPassword).NotNull().NotEmpty().MinimumLength(5).Matches(x => x.AgainNewPassword);
    }
  }
}