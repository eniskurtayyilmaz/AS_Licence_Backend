using AS_Licence.Entities.ViewModel.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AS_Licence.Entities.Model.CustomerComputerInfo;

namespace AS_Licence.Service.Interface.Customer
{
    public interface ICustomerComputerInfoManager
    {
        Task<OperationResponse<List<CustomerComputerInfo>>> GetCustomerComputerInfoList(
          Expression<Func<CustomerComputerInfo, bool>> filter = null,
          Func<IQueryable<CustomerComputerInfo>, IOrderedQueryable<CustomerComputerInfo>> orderBy = null,
          string includeProperties = "");
        Task<OperationResponse<CustomerComputerInfo>> SaveCustomerComputerInfo(CustomerComputerInfo customer);
        Task<OperationResponse<CustomerComputerInfo>> DeleteCustomerComputerInfoByCustomerComputerInfoId(int id);
        Task<OperationResponse<CustomerComputerInfo>> GetByCustomerComputerInfoId(int id);
        Task<OperationResponse<CustomerComputerInfo>> UpdateCustomerComputerInfo(CustomerComputerInfo model);
        Task<OperationResponse<List<CustomerComputerInfo>>> GetByCustomerComputerInfoListBySubscriptionId(
          int SubscriptionId);
        Task<OperationResponse<int>> GetAlreadyComputerCountsBySubscriptionId(int subscriptionId);
        Task<OperationResponse<CustomerComputerInfo>> GetByCustomerComputerHddAndMacAndProcessSerialCode(int subscriptionId, string hddCode,
          string macCode, string processCode);

    }
}
