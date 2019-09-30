using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AS_Licence.Entities.ViewModel.Operations
{
  public class OperationResponse<T>
  {
    public OperationResponse()
    {
      Status = false;
      Message = "";
      Data = default(T);
    }

    public bool Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
  }
}
