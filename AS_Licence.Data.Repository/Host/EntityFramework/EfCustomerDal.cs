using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AS_Licence.Data.Interface.DataAccess;
using AS_Licence.Data.Repository.Infrastracture.EntityFramework;
using AS_Licence.Entities.Model.Customer;

namespace AS_Licence.Data.Repository.Host.EntityFramework
{
  public class EfCustomerDal : EfEntityRepositoryBase<Customer>, ICustomerDal
  {
    private readonly EfAsLicenceContext _context;

    public EfCustomerDal(EfAsLicenceContext context) : base(context)
    {
      _context = context;
    }


    public Customer GetCustomerByEmail(string customerEmail)
    {
      return this.Get(x => x.CustomerEMail == customerEmail).SingleOrDefault();
    }
  }
}
