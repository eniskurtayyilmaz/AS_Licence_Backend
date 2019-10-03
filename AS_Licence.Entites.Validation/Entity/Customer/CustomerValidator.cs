using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FluentValidation;

namespace AS_Licence.Entites.Validation.Entity.Customer
{
  public class CustomerValidator : AbstractValidator<Entities.Model.Customer.Customer>
  {
    public CustomerValidator()
    {
      RuleFor(x => x.CustomerName).NotNull().NotEmpty().MinimumLength(5);
      RuleFor(x => x.CustomerEMail).NotNull().NotEmpty().EmailAddress();
      RuleFor(x => x.CustomerPhone).NotNull().NotEmpty().MinimumLength(5);
    }
  }
}
