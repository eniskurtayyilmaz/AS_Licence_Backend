using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AS_Licence.Entites.Dto;
using FluentValidation;

namespace AS_Licence.Entites.Validation.Dto.User
{
 public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
  {
    public UserForLoginDtoValidator()
    {
      RuleFor(x => x.Username).NotNull().NotEmpty();
      RuleFor(x => x.Password).NotNull().NotEmpty();

    }
  }
}
