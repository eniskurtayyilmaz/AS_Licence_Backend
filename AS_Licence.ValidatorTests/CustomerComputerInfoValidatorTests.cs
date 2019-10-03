using AS_Licence.Entites.Validation.Entity.CustomerComputerInfo;
using AS_Licence.Entities.Model.CustomerComputerInfo;
using Xunit;
using Xunit.Abstractions;

namespace AS_Licence.ValidatorTests
{
  public class CustomerComputerInfoValidatorTests
  {
    private ITestOutputHelper outputHelper;

    public CustomerComputerInfoValidatorTests(ITestOutputHelper outputHelper)
    {
      this.outputHelper = outputHelper;
    }

    [Fact]
    public void Can_Pass_CustomerComputerInfoValidator_Tests()
    {
      //Arrange
      var customerComputerInfo = new CustomerComputerInfo()
      {
        SubscriptionId = 2,
        CustomerComputerInfoHddSerialCode = "HDDNUMBER123456",
        CustomerComputerInfoMacSerialCode = "MACNUMBER123456",
        CustomerComputerInfoProcessSerialCode = "PROCESSNUMBER123456",
      };

      //Action
      var result = new CustomerComputerInfoValidator().Validate(customerComputerInfo);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.True(result.IsValid);

    }

    [Fact]
    public void Can_Not_Pass_CustomerComputerInfoValidator_Tests()
    {
      //Arrange
      var customerComputerInfo = new CustomerComputerInfo()
      {
        SubscriptionId = 0,
        CustomerComputerInfoHddSerialCode = "1234",
        CustomerComputerInfoMacSerialCode = "1234",
        CustomerComputerInfoProcessSerialCode = "1234",
      };

      //Action
      var result = new CustomerComputerInfoValidator().Validate(customerComputerInfo);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.False(result.IsValid);
    }
  }
}
