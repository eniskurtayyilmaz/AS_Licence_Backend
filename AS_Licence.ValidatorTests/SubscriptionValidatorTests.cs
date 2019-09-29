using System;
using System.Collections.Generic;
using System.Text;
using AS_Licence.Entites.Validation.Software;
using AS_Licence.Entites.Validation.Subscription;
using AS_Licence.Entities.Model.Software;
using AS_Licence.Entities.Model.Subscription;
using Xunit;
using Xunit.Abstractions;

namespace AS_Licence.ValidatorTests
{
  public class SubscriptionValidatorTests
  {
    /*
     * RuleFor(x => x.SoftwareId).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
      RuleFor(x => x.CustomerId).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
      RuleFor(x => x.SubScriptionStartDate).NotNull().NotEmpty();
      RuleFor(x => x.SubScriptionEndDate).NotNull().NotEmpty().GreaterThan(x => x.SubScriptionStartDate);
      RuleFor(x => x.SubScriptionLicenceCount).NotNull().NotEmpty().GreaterThanOrEqualTo(1);
     */
    private ITestOutputHelper outputHelper;

    public SubscriptionValidatorTests(ITestOutputHelper outputHelper)
    {
      this.outputHelper = outputHelper;
    }

    [Fact]
    public void Can_Pass_SoftwareValidator_Tests()
    {
      //Arrange
      var model = new Subscription()
      {
        SoftwareId = 1,
        CustomerId = 1,
        SubScriptionStartDate = DateTime.Now,
        SubScriptionEndDate = DateTime.Now.AddDays(1),
        SubScriptionLicenceCount = 2,
      };

      //Action
      var result = new SubscriptionValidator().Validate(model);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.True(result.IsValid);
    }

    [Fact]
    public void Can_Not_Pass_SoftwareValidator_Tests()
    {
      //Arrange
      var model = new Subscription()
      {
        SoftwareId = 0,
        CustomerId = 0,
        SubScriptionStartDate = DateTime.Now,
        SubScriptionEndDate = DateTime.Now,
        SubScriptionLicenceCount = 0,
      };

      //Action
      var result = new SubscriptionValidator().Validate(model);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.False(result.IsValid);
    }
  }
}
