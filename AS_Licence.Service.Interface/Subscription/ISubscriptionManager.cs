using AS_Licence.Entities.ViewModel.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AS_Licence.Service.Interface.Subscription
{
  public interface ISubscriptionManager
  {
    OperationResponse<List<Entities.Model.Subscription.Subscription>> GetSubscriptionList(Expression<Func<Entities.Model.Subscription.Subscription, bool>> filter = null, Func<IQueryable<Entities.Model.Subscription.Subscription>, IOrderedQueryable<Entities.Model.Subscription.Subscription>> orderBy = null, string includeProperties = "");
    OperationResponse<Entities.Model.Subscription.Subscription> SaveSubscription(Entities.Model.Subscription.Subscription subscription);
    OperationResponse<Entities.Model.Subscription.Subscription> DeleteSubscriptionBySubscriptionId(int id);
    OperationResponse<Entities.Model.Subscription.Subscription> GetBySubscriptionId(int id);
    OperationResponse<List<Entities.Model.Subscription.Subscription>> GetSubscriptionListBySoftwareId(int softwareId);
    OperationResponse<List<Entities.Model.Subscription.Subscription>> GetSubscriptionListByCustomerId(int customerId);
    OperationResponse<Entities.Model.Subscription.Subscription> GetBySubscriptionStatusBySoftwareIdAndCustomerId(int softwareId, int customerId);
  }
}
