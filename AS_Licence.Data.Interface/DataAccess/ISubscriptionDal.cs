using System.Collections.Generic;
using AS_Licence.Data.Interface.GenericRepository;
using AS_Licence.Entities.Model.Subscription;

namespace AS_Licence.Data.Interface.DataAccess
{
  public interface ISubscriptionDal : IGenericRepository<Subscription>
  {
    List<Entities.Model.Subscription.Subscription> GetSubscriptionListBySoftwareId(int softwareId);
    List<Entities.Model.Subscription.Subscription> GetSubscriptionListByCustomerId(int customerId);
    Entities.Model.Subscription.Subscription GetSubscriptionBySoftwareIdAndCustomerId(int softwareId, int customerId);
  }
}