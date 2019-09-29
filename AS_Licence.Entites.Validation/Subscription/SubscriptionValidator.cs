using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace AS_Licence.Entites.Validation.Subscription
{
  public class SubscriptionValidator : AbstractValidator<Entities.Model.Subscription.Subscription>
  {
    public SubscriptionValidator()
    {
      RuleFor(x => x.SoftwareId).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
      RuleFor(x => x.CustomerId).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
      RuleFor(x => x.SubScriptionStartDate).NotNull().NotEmpty();
      RuleFor(x => x.SubScriptionEndDate).NotNull().NotEmpty().GreaterThan(x => x.SubScriptionStartDate);
      RuleFor(x => x.SubScriptionLicenceCount).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
    }
  }
}
