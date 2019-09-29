using System;
using System.Collections.Generic;
using System.Text;
using AS_Licence.Entites.Validation.CustomerComputerInfo;
using AS_Licence.Entites.Validation.Software;
using AS_Licence.Entities.Model.CustomerComputerInfo;
using AS_Licence.Entities.Model.Software;
using Xunit;
using Xunit.Abstractions;

namespace AS_Licence.ValidatorTests
{
  public class SoftwareValidatorTests
  {
    /*      RuleFor(x => x.SoftwareName).NotNull().NotEmpty().MinimumLength(5);
      RuleFor(x => x.SoftwareLastVersion).NotNull().NotEmpty().MinimumLength(1);*/
    private ITestOutputHelper outputHelper;

    public SoftwareValidatorTests(ITestOutputHelper outputHelper)
    {
      this.outputHelper = outputHelper;
    }

    [Fact]
    public void Can_Pass_SoftwareValidator_Tests()
    {
      //Arrange
      var model = new Software()
      {
        SoftwareName = "TwitterBot",
        SoftwareLastVersion = "v1.20",
      };

      //Action
      var result = new SoftwareValidator().Validate(model);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.True(result.IsValid);
    }

    [Fact]
    public void Can_Not_Pass_SoftwareValidator_Tests()
    {
      //Arrange
      var model = new Software()
      {
        SoftwareName = "1234",
        SoftwareLastVersion = "",
      };

      //Action
      var result = new SoftwareValidator().Validate(model);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.False(result.IsValid);

    }
  }
}
