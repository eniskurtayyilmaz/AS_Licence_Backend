using System;
using System.Collections.Generic;
using System.Text;
using AS_Licence.Entities.ViewModel.Register;

namespace AS_Licence.Entites.Map.CustomerComputerInfo
{
  public static class RegisterComputerToCustomerComputerInfoExtension
  {
    public static Entities.Model.CustomerComputerInfo.CustomerComputerInfo MapRegisterComputerToCustomerComputerInfo(this RegisterComputer registerComputer,
      int subscriptionId)
    {
      Entities.Model.CustomerComputerInfo.CustomerComputerInfo customerComputerInfo =
        new Entities.Model.CustomerComputerInfo.CustomerComputerInfo()
        {
          CustomerComputerInfoId = 0,
          SubscriptionId = subscriptionId,
          CustomerComputerInfoHddSerialCode = registerComputer.ComputerInfoHddSerialCode,
          CustomerComputerInfoMacSerialCode = registerComputer.ComputerInfoMacSerialCode,
          CustomerComputerInfoProcessSerialCode = registerComputer.ComputerInfoProcessSerialCode,
        };

      return customerComputerInfo;
    }
  }
}
