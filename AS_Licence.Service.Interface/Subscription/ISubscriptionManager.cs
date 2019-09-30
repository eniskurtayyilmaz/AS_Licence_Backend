using AS_Licence.Entities.ViewModel.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AS_Licence.Service.Interface.Subscription
{
  public interface ISubscriptionManager
  {
    Task<OperationResponse<List<Entities.Model.Subscription.Subscription>>> GetSubscriptionList(
      Expression<Func<Entities.Model.Subscription.Subscription, bool>> filter = null,
      Func<IQueryable<Entities.Model.Subscription.Subscription>,
        IOrderedQueryable<Entities.Model.Subscription.Subscription>> orderBy = null, string includeProperties = "");
    Task<OperationResponse<Entities.Model.Subscription.Subscription>> SaveSubscription(
      Entities.Model.Subscription.Subscription subscription);
    Task<OperationResponse<Entities.Model.Subscription.Subscription>> DeleteSubscriptionBySubscriptionId(int id);
    Task<OperationResponse<Entities.Model.Subscription.Subscription>> GetBySubscriptionId(int id);
    Task<OperationResponse<List<Entities.Model.Subscription.Subscription>>> GetSubscriptionListBySoftwareId(
      int softwareId);
    Task<OperationResponse<List<Entities.Model.Subscription.Subscription>>> GetSubscriptionListByCustomerId(
      int customerId);
    Task<OperationResponse<Entities.Model.Subscription.Subscription>> GetBySubscriptionStatusBySoftwareIdAndCustomerId(
      int softwareId, int customerId);
  }
}
