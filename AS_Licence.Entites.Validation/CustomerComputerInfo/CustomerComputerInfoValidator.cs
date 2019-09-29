using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace AS_Licence.Entites.Validation.CustomerComputerInfo
{
  public class CustomerComputerInfoValidator : AbstractValidator<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>
  {
    public CustomerComputerInfoValidator()
    {
      When(x => x.CustomerComputerInfoId != 0, () =>
         {
           RuleFor(x => x.SubscriptionId).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
         });
      RuleFor(x => x.CustomerComputerInfoHddSerialCode).NotNull().NotEmpty().MinimumLength(5);
      RuleFor(x => x.CustomerComputerInfoMacSerialCode).NotNull().NotEmpty().MinimumLength(5);
      RuleFor(x => x.CustomerComputerInfoProcessSerialCode).NotNull().NotEmpty().MinimumLength(5);
    }
  }
}
