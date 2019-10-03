using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.GenericRepository;
using AS_Licence.Entities.Model.User;

namespace AS_Licence.Data.Interface.DataAccess
{
  public interface IAuthDal : IGenericRepository<User>
  {
    Task<User> Register(User user, string password);
    Task<User> Login(string username, string password);
    Task<bool> UserExists(string username);
  }
}
