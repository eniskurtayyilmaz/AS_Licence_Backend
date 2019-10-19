using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Entites.Validation.Entity.CustomerComputerInfo;
using AS_Licence.Entities.ViewModel.Operations;
using AS_Licence.Helpers.Extension;
using AS_Licence.Service.Interface.Customer;
using AS_Licence.Service.Interface.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AS_Licence.Service.Host.CustomerComputerInfo
{

  public class CustomerComputerInfoService : ICustomerComputerInfoManager
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionManager _subscriptionManager;

    public CustomerComputerInfoService(IUnitOfWork unitOfWork, ISubscriptionManager subscriptionManager)
    {
      _unitOfWork = unitOfWork;
      _subscriptionManager = subscriptionManager;
    }

    public async Task<OperationResponse<List<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>>>
      GetCustomerComputerInfoList(
        Expression<Func<Entities.Model.CustomerComputerInfo.CustomerComputerInfo, bool>> filter = null,
        Func<IQueryable<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>,
          IOrderedQueryable<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>> orderBy = null,
        string includeProperties = "")
    {
      OperationResponse<List<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>> response = new OperationResponse<List<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>>();
      try
      {
        response.Data = await _unitOfWork.CustomerComputerInfoRepository.Get(filter, orderBy, includeProperties);
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>>
      SaveCustomerComputerInfo(Entities.Model.CustomerComputerInfo.CustomerComputerInfo customer)
    {
      OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo> response = new OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>();

      try
      {
        if (customer == null)
        {
          throw new Exception("Customer nesnesi null olamaz");
        }

        var valid = await new CustomerComputerInfoValidator().ValidateAsync(customer);
        if (valid.IsValid == false)
        {
          throw new Exception(valid.GetErrorMessagesOnSingleLine());
        }

        var subscriptionExists = await _subscriptionManager.GetSubscriptionBySubscriptionId(customer.SubscriptionId);
        if (subscriptionExists.Status == false)
        {
          throw new Exception(subscriptionExists.Message);
        }

        Entities.Model.CustomerComputerInfo.CustomerComputerInfo customerExists = null;
        if (customer.CustomerComputerInfoId > 0)
        {
          var existsCustomer = await _unitOfWork.CustomerComputerInfoRepository.GetById(customer.CustomerComputerInfoId);
          customerExists = existsCustomer ?? throw new Exception("Sistemde kayıtlı bir müşteri bilgisi bulunamadı.");
        }
        else
        {
          var existsCustomer =
            await _unitOfWork.CustomerComputerInfoRepository.GetByCustomerComputerHddAndMacAndProcessSerialCode(
              customer.CustomerComputerInfoHddSerialCode, customer.CustomerComputerInfoMacSerialCode,
              customer.CustomerComputerInfoProcessSerialCode);
          if (existsCustomer != null)
          {
            throw new Exception("HDD, Mac ve Serial bilgisine göre zaten bir kayıt mevcut. Var olan kayıt üzerinden devam ediniz.");
          }
        }

        if (customerExists != null)
        {
          customerExists.SubscriptionId = customerExists.SubscriptionId;
          customerExists.CustomerComputerInfoHddSerialCode = customerExists.CustomerComputerInfoHddSerialCode;
          customerExists.CustomerComputerInfoMacSerialCode = customerExists.CustomerComputerInfoMacSerialCode;
          customerExists.CustomerComputerInfoProcessSerialCode = customerExists.CustomerComputerInfoProcessSerialCode;
          customerExists.UpdatedDateTime = DateTime.Now;
          await _unitOfWork.CustomerComputerInfoRepository.Update(customerExists);
        }
        else
        {
          customer.CreatedDateTime = DateTime.Now;
          await _unitOfWork.CustomerComputerInfoRepository.Insert(customer);
        }

        response.Data = customer;
        var responseUnitOfWork = await _unitOfWork.Save();
        response.Status = responseUnitOfWork.Status;
        response.Message = responseUnitOfWork.Message;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }

      return response;
    }

    public async Task<OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>>
      DeleteCustomerComputerInfoByCustomerComputerInfoId(int id)
    {
      OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo> response = new OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>();

      try
      {
        var existsCustomer = await _unitOfWork.CustomerComputerInfoRepository.GetById(id);
        if (existsCustomer == null)
        {
          throw new Exception("Sistemde kayıtlı bir müşteri bilgisi bulunamadı.");
        }

        await _unitOfWork.CustomerComputerInfoRepository.Delete(existsCustomer);
        var responseUnitOfWork = await _unitOfWork.Save();
        response.Status = responseUnitOfWork.Status;
        response.Message = responseUnitOfWork.Message;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>>
      GetByCustomerComputerInfoId(int id)
    {
      OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo> response = new OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>();

      try
      {
        var existsCustomer = await _unitOfWork.CustomerComputerInfoRepository.GetById(id);
        response.Data = existsCustomer ?? throw new Exception("Sistemde kayıtlı bir müşteri bilgisi bulunamadı.");
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<OperationResponse<List<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>>>
      GetByCustomerComputerInfoListBySubscriptionId(int SubscriptionId)
    {
      OperationResponse<List<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>> response = new OperationResponse<List<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>>();
      try
      {
        response.Data = await _unitOfWork.CustomerComputerInfoRepository.Get(x => x.SubscriptionId == SubscriptionId, o => o.OrderBy(info => info.CreatedDateTime));
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<OperationResponse<int>> GetAlreadyComputerCountsBySubscriptionId(int subscriptionId)
    {
      OperationResponse<int> response = new OperationResponse<int>();

      try
      {
        var customerComputerInfoListResult = await GetCustomerComputerInfoList(x => x.SubscriptionId == subscriptionId);
        if (customerComputerInfoListResult.Status == false)
        {
          throw new Exception(customerComputerInfoListResult.Message);
        }

        response.Status = true;
        response.Data = customerComputerInfoListResult.Data.Count;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>>
      GetByCustomerComputerHddAndMacAndProcessSerialCode(string hddCode, string macCode, string processCode)
    {
      OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo> response = new OperationResponse<Entities.Model.CustomerComputerInfo.CustomerComputerInfo>();

      try
      {
        var existsCustomer = await _unitOfWork.CustomerComputerInfoRepository.GetByCustomerComputerHddAndMacAndProcessSerialCode(hddCode, macCode, processCode);
        response.Data = existsCustomer ?? throw new Exception("Sistemde kayıtlı bir müşteri bilgisayar bilgisi bulunamadı.");
        response.Status = true;
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
