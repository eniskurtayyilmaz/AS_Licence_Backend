using AS_Licence.Entites.Validation.Entity.User;
using AS_Licence.Entities.Model.User;
using Xunit;
using Xunit.Abstractions;

namespace AS_Licence.ValidatorTests
{

  public class UserValidatorTests
  {
    private ITestOutputHelper outputHelper;

    public UserValidatorTests(ITestOutputHelper outputHelper)
    {
      this.outputHelper = outputHelper;
    }


    [Fact]
    public void Can_Pass_UserValidator_Tests()
    {
      //Arrange
      var user = new User()
      {
        UserName = "kurtayyilmaz"
      };

      //Arrange
      var result = new UserValidator().Validate(user);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.True(result.IsValid);
    }

    [Fact]
    public void Can_Not_Pass_UserValidator_Tests()
    {
      //Arrange
      var user = new User()
      {
        UserName = ""
      };

      //Arrange
      var result = new UserValidator().Validate(user);

      //Assert
      OutputHelpers.WriteLineError(result, outputHelper);
      Assert.False(result.IsValid);
    }
  }
}
