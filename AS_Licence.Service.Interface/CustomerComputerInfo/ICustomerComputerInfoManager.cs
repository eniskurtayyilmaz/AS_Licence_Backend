using AS_Licence.Entities.ViewModel.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AS_Licence.Service.Interface.Customer
{
  public interface ICustomerComputerInfoManager
  {
    OperationResponse<List<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>> GetCustomerComputerInfoList(Expression<Func<Entities.Model.CustomerComputerInfo.CustomerComputerInfo, bool>> filter = null, Func<IQueryable<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>, IOrderedQueryable<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>> orderBy = null, string includeProperties = "");
    OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo> SaveCustomerComputerInfo(Entities.Model.CustomerComputerInfo.CustomerComputerInfo customer);
    OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo> DeleteCustomerComputerInfoByCustomerComputerInfoId(int id);
    OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo> GetByCustomerComputerInfoId(int id);
    OperationResponse<List<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>> GetByCustomerComputerInfoListBySubscriptionId(int SubscriptionId);
    OperationResponse<int> GetAlreadyComputerCountsBySubscriptionId(int subscriptionId);
    OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo> GetByCustomerComputerHddAndMacAndProcessSerialCode(string hddCode, string macCode, string processCode);

  }
}
