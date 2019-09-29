﻿using AS_Licence.Data.Interface.GenericRepository;
using AS_Licence.Entities.Model.CustomerComputerInfo;

namespace AS_Licence.Data.Interface.DataAccess
{
  public interface ICustomerComputerInfoDal : IGenericRepository<CustomerComputerInfo>
  {
    CustomerComputerInfo GetByCustomerComputerHddAndMacAndProcessSerialCode(string hddCode, string macCode,
      string processCode);
  }
}