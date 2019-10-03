using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace AS_Licence.Entites.Validation.Entity.Software
{
  public class SoftwareValidator : AbstractValidator<Entities.Model.Software.Software>
  {
    public SoftwareValidator()
    {
      RuleFor(x => x.SoftwareName).NotNull().NotEmpty().MinimumLength(5);
      RuleFor(x => x.SoftwareLastVersion).NotNull().NotEmpty().MinimumLength(1);
    }
  }
}
