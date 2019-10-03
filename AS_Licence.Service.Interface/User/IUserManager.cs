using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AS_Licence.Entities.ViewModel.Operations;

namespace AS_Licence.Service.Interface.User
{
  public interface IUserManager
  {
    Task<OperationResponse<Entities.Model.User.User>> RegisterUser(Entities.Model.User.User user, string password);
    Task<OperationResponse<Entities.Model.User.User>> LoginUser(string username, string password);
    Task<OperationResponse<bool>> UserExists(string username);
  }
}
