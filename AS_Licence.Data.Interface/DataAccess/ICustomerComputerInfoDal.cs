using System.Threading.Tasks;
using AS_Licence.Data.Interface.GenericRepository;
using AS_Licence.Entities.Model.CustomerComputerInfo;

namespace AS_Licence.Data.Interface.DataAccess
{
  public interface ICustomerComputerInfoDal : IGenericRepository<CustomerComputerInfo>
  {
    Task<CustomerComputerInfo> GetByCustomerComputerHddAndMacAndProcessSerialCode(int subscriptionId, string hddCode, string macCode,
      string processCode);
  }
}