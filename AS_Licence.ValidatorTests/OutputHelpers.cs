using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AS_Licence.Helpers.Extension;
using FluentValidation.Results;
using Xunit.Abstractions;

namespace AS_Licence.ValidatorTests
{
  public static class OutputHelpers
  {


    public static void WriteLineError(ValidationResult result, ITestOutputHelper outputHelper)
    {
      if (result?.IsValid == false)
      {
        if (outputHelper != null)
        {
         
          outputHelper.WriteLine(result.GetErrorMessagesOnSingleLine());
          //outputHelper.WriteLine(string.Join(Environment.NewLine, result.Errors.Select(x => x.ErrorMessage).ToArray<string>()));
          return;
          /*
          foreach (var error in result.Errors)
          {
            outputHelper.WriteLine(error.ErrorCode + " | " + error.ErrorMessage);
          }
          */
        }
      }
    }
  }
}
