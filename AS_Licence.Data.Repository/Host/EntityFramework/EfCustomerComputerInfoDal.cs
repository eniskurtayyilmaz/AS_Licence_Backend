using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.DataAccess;
using AS_Licence.Data.Repository.Infrastracture.EntityFramework;
using AS_Licence.Entities.Model.CustomerComputerInfo;

namespace AS_Licence.Data.Repository.Host.EntityFramework
{
  public class EfCustomerComputerInfoDal : EfEntityRepositoryBase<CustomerComputerInfo>, ICustomerComputerInfoDal
  {
    private readonly EfAsLicenceContext _context;

    public EfCustomerComputerInfoDal(EfAsLicenceContext context) : base(context)
    {
      _context = context;
    }

    public async Task<CustomerComputerInfo> GetByCustomerComputerHddAndMacAndProcessSerialCode(string hddCode, string macCode,
      string processCode)
    {
      return this.Get(x => x.CustomerComputerInfoHddSerialCode == hddCode && x.CustomerComputerInfoMacSerialCode ==
        macCode && x.CustomerComputerInfoProcessSerialCode == processCode).Result.SingleOrDefault();
    }
  }
}
