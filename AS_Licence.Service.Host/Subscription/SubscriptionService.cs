using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Entites.Validation.Entity.Subscription;
using AS_Licence.Entities.ViewModel.Operations;
using AS_Licence.Helpers.Extension;
using AS_Licence.Service.Host.Customer;
using AS_Licence.Service.Host.Software;
using AS_Licence.Service.Interface.Customer;
using AS_Licence.Service.Interface.Software;
using AS_Licence.Service.Interface.Subscription;
using FluentValidation.Results;

namespace AS_Licence.Service.Host.Subscription
{

  public class SubscriptionService : ISubscriptionManager
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISoftwareManager _softwareManager;
    private readonly ICustomerManager _customerManager;

    public SubscriptionService(IUnitOfWork unitOfWork, ISoftwareManager softwareManager)
    {
      _unitOfWork = unitOfWork;
      _softwareManager = softwareManager;
      _customerManager = new CustomerService(_unitOfWork);
    }

    public async Task<OperationResponse<List<Entities.Model.Subscription.Subscription>>> GetSubscriptionList(
      Expression<Func<Entities.Model.Subscription.Subscription, bool>> filter = null,
      Func<IQueryable<Entities.Model.Subscription.Subscription>,
        IOrderedQueryable<Entities.Model.Subscription.Subscription>> orderBy = null, string includeProperties = "")
    {
      OperationResponse<List<Entities.Model.Subscription.Subscription>> response = new OperationResponse<List<Entities.Model.Subscription.Subscription>>();
      try
      {
        response.Data = await _unitOfWork.SubscriptionRepository.Get(filter, orderBy, includeProperties);
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }



    public async Task<OperationResponse<Entities.Model.Subscription.Subscription>> SaveSubscription(
      Entities.Model.Subscription.Subscription subscription)
    {
      OperationResponse<Entities.Model.Subscription.Subscription> response = new OperationResponse<Entities.Model.Subscription.Subscription>();

      try
      {
        if (subscription == null)
        {
          throw new Exception("Subscription nesnesi null olamaz");
        }

        var valid = await new SubscriptionValidator().ValidateAsync(subscription);
        if (valid.IsValid == false)
        {
          throw new Exception(valid.GetErrorMessagesOnSingleLine());
        }

        var softwareExists = await _softwareManager.GetBySoftwareId(subscription.SoftwareId);
        if (softwareExists.Status == false)
        {
          throw new Exception(softwareExists.Message);
        }

        var customerExists = await _customerManager.GetByCustomerId(subscription.CustomerId);
        if (customerExists.Status == false)
        {
          throw new Exception(customerExists.Message);
        }

        Entities.Model.Subscription.Subscription subscriptionExists = null;
        if (subscription.SubscriptionId > 0)
        {
          var existsSubscription = await _unitOfWork.SubscriptionRepository.GetById(subscription.SubscriptionId);
          subscriptionExists = existsSubscription ?? throw new Exception("Sistemde kayıtlı bir abonelik bilgisi bulunamadı.");
        }
        else
        {
          /* Insert işlemi peki ya var ise? */
          var existsSubscription = await _unitOfWork.SubscriptionRepository.GetSubscriptionBySoftwareIdAndCustomerId(subscription.SoftwareId, subscription.CustomerId);
          if (existsSubscription != null)
          {
            throw new Exception("Sistemde zaten kayıtlı bir abonelik bilgisi var. Var olanı seçip güncelleyin..");
          }
        }

        if (subscriptionExists != null)
        {
          subscriptionExists.SoftwareId = subscription.SoftwareId;
          subscriptionExists.CustomerId = subscription.CustomerId;
          subscriptionExists.SubscriptionIsActive = subscription.SubscriptionIsActive;
          subscriptionExists.SubScriptionStartDate = subscription.SubScriptionStartDate;
          subscriptionExists.SubScriptionEndDate = subscription.SubScriptionEndDate;
          subscriptionExists.SubScriptionLicenceCount = subscription.SubScriptionLicenceCount;
          subscriptionExists.UpdatedDateTime = DateTime.Now;

          await _unitOfWork.SubscriptionRepository.Update(subscriptionExists);
        }
        else
        {
          subscription.CreatedDateTime = DateTime.Now;
          await _unitOfWork.SubscriptionRepository.Insert(subscription);
        }

        response.Data = subscription;
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

    public async Task<OperationResponse<Entities.Model.Subscription.Subscription>>
      DeleteSubscriptionBySubscriptionId(int id)
    {
      OperationResponse<Entities.Model.Subscription.Subscription> response = new OperationResponse<Entities.Model.Subscription.Subscription>();

      try
      {
        var existsSubscription = await _unitOfWork.SubscriptionRepository.GetById(id);
        if (existsSubscription == null)
        {
          throw new Exception("Sistemde kayıtlı bir abonelik bilgisi bulunamadı.");
        }

        await _unitOfWork.SubscriptionRepository.Delete(existsSubscription);
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

    public async Task<OperationResponse<Entities.Model.Subscription.Subscription>> GetBySubscriptionId(int id)
    {
      OperationResponse<Entities.Model.Subscription.Subscription> response = new OperationResponse<Entities.Model.Subscription.Subscription>();

      try
      {
        var existsSubscription = await _unitOfWork.SubscriptionRepository.GetById(id);
        response.Data = existsSubscription ?? throw new Exception("Sistemde kayıtlı bir abonelik bilgisi bulunamadı.");
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<OperationResponse<List<Entities.Model.Subscription.Subscription>>>
      GetSubscriptionListBySoftwareId(int softwareId)
    {
      OperationResponse<List<Entities.Model.Subscription.Subscription>> response = new OperationResponse<List<Entities.Model.Subscription.Subscription>>();
      try
      {
        response.Data = await _unitOfWork.SubscriptionRepository.GetSubscriptionListBySoftwareId(softwareId);
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<OperationResponse<List<Entities.Model.Subscription.Subscription>>>
      GetSubscriptionListByCustomerId(int customerId)
    {
      OperationResponse<List<Entities.Model.Subscription.Subscription>> response = new OperationResponse<List<Entities.Model.Subscription.Subscription>>();
      try
      {
        response.Data = await _unitOfWork.SubscriptionRepository.GetSubscriptionListByCustomerId(customerId);
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public async Task<OperationResponse<Entities.Model.Subscription.Subscription>>
      GetBySubscriptionStatusBySoftwareIdAndCustomerId(int softwareId, int customerId)
    {
      OperationResponse<Entities.Model.Subscription.Subscription> response = new OperationResponse<Entities.Model.Subscription.Subscription>();

      try
      {
        var existsSubscription = await _unitOfWork.SubscriptionRepository.GetSubscriptionBySoftwareIdAndCustomerId(softwareId, customerId);
        response.Data = existsSubscription ?? throw new Exception("Sistemde müşteriye ait kayıtlı bir abonelik bilgisi bulunamadı.");
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
