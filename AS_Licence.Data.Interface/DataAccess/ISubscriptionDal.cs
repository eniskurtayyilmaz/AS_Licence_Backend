using System.Collections.Generic;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.GenericRepository;
using AS_Licence.Entities.Model.Subscription;

namespace AS_Licence.Data.Interface.DataAccess
{
  public interface ISubscriptionDal : IGenericRepository<Subscription>
  {
    Task<List<Subscription>> GetSubscriptionListBySoftwareId(int softwareId);
    Task<List<Subscription>> GetSubscriptionListByCustomerId(int customerId);
    Task<Subscription> GetSubscriptionBySoftwareIdAndCustomerId(int softwareId, int customerId);
  }
}