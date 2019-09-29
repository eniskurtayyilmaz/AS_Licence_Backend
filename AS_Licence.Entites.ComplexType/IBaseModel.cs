using System;

namespace AS_Licence.Entites.ComplexType
{
  public interface IBaseModel
  {
    Nullable<DateTime> CreatedDateTime { get; set; }
    Nullable<DateTime> UpdatedDateTime { get; set; }
  }
}
