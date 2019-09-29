using FluentValidation.Results;
using System;
using System.Linq;

namespace AS_Licence.Helpers.Extension
{
  public static class ValidationResultExtention
  {
    public static string GetErrorMessagesOnSingleLine<T>(this T result) where T : ValidationResult
    {
      return string.Join(Environment.NewLine, result.Errors.Select(x => x.ErrorMessage).ToArray<string>());
    }
  }
}
