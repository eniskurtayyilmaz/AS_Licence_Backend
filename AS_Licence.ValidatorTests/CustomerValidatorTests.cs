using AS_Licence.Entites.Validation.Entity.Customer;
using AS_Licence.Entities.Model.Customer;
using Xunit;
using Xunit.Abstractions;

namespace AS_Licence.ValidatorTests
{
  public class CustomerValidatorTests
  {

    private ITestOutputHelper outputHelper;

    public CustomerValidatorTests(ITestOutputHelper outputHelper)
    {
      this.outputHelper = outputHelper;
    }


    [Fact]
    public void Can_Pass_CustomerValidator_Tests()
    {
      //Arrange
      var customer = new Customer()
      {
        CustomerEMail = "kurtayyilmaz@gmail.com",
        CustomerIsActive = false,
        CustomerName = "Enis Kurtay",
        CustomerPhone = "12312093",
      };

      //Arrange
      var result = new CustomerValidator().Validate(customer);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.True(result.IsValid);
    }

    [Fact]
    public void Can_Not_Pass_CustomerValidator_Tests()
    {
      //Arrange
      var customer = new Customer()
      {
        CustomerEMail = "testMail@",
        CustomerIsActive = false,
        CustomerName = "123",
        CustomerPhone = "345",
      };

      //Arrange
      var result = new CustomerValidator().Validate(customer);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.False(result.IsValid);
    }
  }
}
