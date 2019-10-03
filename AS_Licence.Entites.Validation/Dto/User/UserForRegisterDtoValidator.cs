using System;
using System.Collections.Generic;
using System.Text;
using AS_Licence.Entites.Dto;
using FluentValidation;

namespace AS_Licence.Entites.Validation.Dto.User
{
 public class UserForRegisterDtoValidator : AbstractValidator<UserForRegisterDto>
  {
    public UserForRegisterDtoValidator()
    {
      RuleFor(x => x.Username).NotNull().NotEmpty();
      RuleFor(x => x.Password).NotNull().NotEmpty();
    }
  }
}