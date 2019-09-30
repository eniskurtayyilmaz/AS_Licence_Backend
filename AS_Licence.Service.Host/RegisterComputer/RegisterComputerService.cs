using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Entities.ViewModel.Operations;
using AS_Licence.Helpers.Extension;
using AS_Licence.Service.Interface.RegisterComputer;
using AS_Licence.Service.Interface.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AS_Licence.Entites.Map.CustomerComputerInfo;
using AS_Licence.Entites.Validation.RegisterComputer;
using AS_Licence.Service.Interface.Customer;
using AS_Licence.Service.Interface.Software;

namespace AS_Licence.Service.Host.RegisterComputer
{

  public class RegisterComputerService : IRegisterComputerManager
  {
    private readonly ISoftwareManager _softwareManager;
    private readonly ICustomerManager _customerManager;
    private readonly ISubscriptionManager _subscriptionManager;
    private readonly ICustomerComputerInfoManager _customerComputerInfoManager;

    public RegisterComputerService(ISoftwareManager softwareManager, ICustomerManager customerManager, ISubscriptionManager subscriptionManager, ICustomerComputerInfoManager customerComputerInfoManager)
    {
      _softwareManager = softwareManager;
      _customerManager = customerManager;
      _subscriptionManager = subscriptionManager;
      _customerComputerInfoManager = customerComputerInfoManager;
    }

    public async Task<OperationResponse<Entities.ViewModel.Register.RegisterComputer>> SaveRegisterComputer(
      Entities.ViewModel.Register.RegisterComputer registerComputer)
    {
      OperationResponse<Entities.ViewModel.Register.RegisterComputer> response = new OperationResponse<Entities.ViewModel.Register.RegisterComputer>();

      try
      {
        if (registerComputer == null)
        {
          throw new Exception("RegisterComputer nesnesi null olamaz");
        }

        var valid = new RegisterComputerValidator().Validate(registerComputer);
        if (valid.IsValid == false)
        {
          throw new Exception(valid.GetErrorMessagesOnSingleLine());
        }

        #region Yazılım adını kontrol et ve aktif mi kontrol et?
        var softwareExistsResult = await _softwareManager.GetBySoftwareName(registerComputer.SoftwareName);
        if (softwareExistsResult.Status == false)
        {
          throw new Exception(softwareExistsResult.Message);
        }

        if (softwareExistsResult.Data.SoftwareIsActive == false)
        {
          throw new Exception("Bu yazılım artık aktif değil.");
        }

        #endregion


        #region Müşteriyi kontrol et ve aktif mi kontrol et?
        var customerExistsResult = await _customerManager.GetCustomerByEmail(registerComputer.CustomerEMail);
        if (customerExistsResult.Status == false)
        {
          throw new Exception(customerExistsResult.Message);
        }

        if (customerExistsResult.Data.CustomerIsActive == false)
        {
          throw new Exception("Bu müşteri artık aktif değil.");
        }
        #endregion

        //Subscription durumunu önce kontrol et
        var subscriptionExistsResult = await
          _subscriptionManager.GetBySubscriptionStatusBySoftwareIdAndCustomerId(softwareExistsResult.Data.SoftwareId,
            customerExistsResult.Data.CustomerId);
        if (subscriptionExistsResult.Status == false)
        {
          throw new Exception(subscriptionExistsResult.Message);
        }

        if (subscriptionExistsResult.Data.SubscriptionIsActive == false)
        {
          throw new Exception("Bu abonelik artık aktif değil.");
        }

        //Lisans hakkı var ise gönderdiği HDD, MAC, Process bilgisi ile zaten o bilgisayar mı onu kontrol et
        var customerComputerInfoExists = await
          _customerComputerInfoManager.GetByCustomerComputerHddAndMacAndProcessSerialCode(
            registerComputer.ComputerInfoHddSerialCode, registerComputer.ComputerInfoMacSerialCode,
            registerComputer.ComputerInfoProcessSerialCode);
        if (customerComputerInfoExists.Status)
        {
          response.Status = true;
          response.Message = $"Lisans {subscriptionExistsResult.Data.SubScriptionEndDate.ToString("dd/MM/yyyy HH:mm:ss")} tarihine kadar aktif.";
          return response;
        }

        var alreadyComputerCountsResult = await _customerComputerInfoManager.GetAlreadyComputerCountsBySubscriptionId(subscriptionExistsResult.Data.SubscriptionId);
        if (alreadyComputerCountsResult.Status == false)
        {
          throw new Exception(alreadyComputerCountsResult.Message);
        }

        //Bilgiler doğru değil ise sisteme kaydetmeden Subscription Lisans hakkı var mı kontrol et. veya hoşçakal de.
        if (subscriptionExistsResult.Data.SubScriptionLicenceCount <= alreadyComputerCountsResult.Data)
        {
          throw new Exception($"Lisans hakkınızın tamamını kullanmışsınız {alreadyComputerCountsResult.Data}/{subscriptionExistsResult.Data.SubScriptionLicenceCount}");
        }


        //Demek ki hala kayıt edebilme hakkı var.
        var saveCustomerComputerInfo = await _customerComputerInfoManager.SaveCustomerComputerInfo(registerComputer.MapRegisterComputerToCustomerComputerInfo(subscriptionExistsResult.Data.SubscriptionId));
        if (saveCustomerComputerInfo.Status == false)
        {
          throw new Exception(saveCustomerComputerInfo.Message);
        }

        response.Status = true;
        response.Message = "Yeni bilgisayar sisteme tanımlandı";
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }

      return response;
    }
  }
}
