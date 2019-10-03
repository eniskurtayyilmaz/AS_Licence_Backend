using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace AS_Licence.Entites.Validation.Entity.RegisterComputer
{
  public class RegisterComputerValidator : AbstractValidator<Entities.ViewModel.Register.RegisterComputer>
  {
    public RegisterComputerValidator()
    {
      RuleFor(x => x.SoftwareName).NotNull().NotEmpty().MinimumLength(5);
      RuleFor(x => x.CustomerEMail).NotNull().NotEmpty().EmailAddress();
      RuleFor(x => x.ComputerInfoHddSerialCode).NotNull().NotEmpty().MinimumLength(5);
      RuleFor(x => x.ComputerInfoMacSerialCode).NotNull().NotEmpty().MinimumLength(5);
      RuleFor(x => x.ComputerInfoProcessSerialCode).NotNull().NotEmpty().MinimumLength(5);
    }
  }
}
