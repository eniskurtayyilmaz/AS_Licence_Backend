using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Entites.Validation.Subscription;
using AS_Licence.Entities.ViewModel.Operations;
using AS_Licence.Helpers.Extension;
using AS_Licence.Service.Interface.Customer;
using AS_Licence.Service.Interface.Subscription;
using FluentValidation.Results;

namespace AS_Licence.Service.Host.Subscription
{

  public class SubscriptionService : ISubscriptionManager
  {
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public OperationResponse<List<Entities.Model.Subscription.Subscription>> GetSubscriptionList(Expression<Func<Entities.Model.Subscription.Subscription, bool>> filter = null, Func<IQueryable<Entities.Model.Subscription.Subscription>, IOrderedQueryable<Entities.Model.Subscription.Subscription>> orderBy = null, string includeProperties = "")
    {
      OperationResponse<List<Entities.Model.Subscription.Subscription>> response = new OperationResponse<List<Entities.Model.Subscription.Subscription>>();
      try
      {
        response.Data = _unitOfWork.SubscriptionRepository.Get(filter, orderBy, includeProperties);
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }



    public OperationResponse<Entities.Model.Subscription.Subscription> SaveSubscription(Entities.Model.Subscription.Subscription subscription)
    {
      OperationResponse<Entities.Model.Subscription.Subscription> response = new OperationResponse<Entities.Model.Subscription.Subscription>();

      try
      {
        if (subscription == null)
        {
          throw new Exception("Subscription nesnesi null olamaz");
        }

        var valid = new SubscriptionValidator().Validate(subscription);
        if (valid.IsValid == false)
        {
          throw new Exception(valid.GetErrorMessagesOnSingleLine());
        }

        if (subscription.SubscriptionId > 0)
        {
          var existsSubscription = _unitOfWork.SubscriptionRepository.GetById(subscription.SubscriptionId);
          if (existsSubscription == null)
          {
            throw new Exception("Sistemde kayıtlı bir abonelik bilgisi bulunamadı.");
          }
        }

        if (subscription.SubscriptionId > 0)
        {
          subscription.UpdatedDateTime = DateTime.Now;
          _unitOfWork.SubscriptionRepository.Update(subscription);
        }
        else
        {
          subscription.CreatedDateTime = DateTime.Now;
          _unitOfWork.SubscriptionRepository.Insert(subscription);
        }

        response.Data = subscription;
        var responseUnitOfWork = _unitOfWork.Save();
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

    public OperationResponse<Entities.Model.Subscription.Subscription> DeleteSubscriptionBySubscriptionId(int id)
    {
      OperationResponse<Entities.Model.Subscription.Subscription> response = new OperationResponse<Entities.Model.Subscription.Subscription>();

      try
      {
        var existsSubscription = _unitOfWork.SubscriptionRepository.GetById(id);
        if (existsSubscription == null)
        {
          throw new Exception("Sistemde kayıtlı bir abonelik bilgisi bulunamadı.");
        }

        _unitOfWork.SubscriptionRepository.Delete(existsSubscription);
        var responseUnitOfWork = _unitOfWork.Save();
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

    public OperationResponse<Entities.Model.Subscription.Subscription> GetBySubscriptionId(int id)
    {
      OperationResponse<Entities.Model.Subscription.Subscription> response = new OperationResponse<Entities.Model.Subscription.Subscription>();

      try
      {
        var existsSubscription = _unitOfWork.SubscriptionRepository.GetById(id);
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

    public OperationResponse<List<Entities.Model.Subscription.Subscription>> GetSubscriptionListBySoftwareId(int softwareId)
    {
      OperationResponse<List<Entities.Model.Subscription.Subscription>> response = new OperationResponse<List<Entities.Model.Subscription.Subscription>>();
      try
      {
        response.Data = _unitOfWork.SubscriptionRepository.GetSubscriptionListBySoftwareId(softwareId);
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public OperationResponse<List<Entities.Model.Subscription.Subscription>> GetSubscriptionListByCustomerId(int customerId)
    {
      OperationResponse<List<Entities.Model.Subscription.Subscription>> response = new OperationResponse<List<Entities.Model.Subscription.Subscription>>();
      try
      {
        response.Data = _unitOfWork.SubscriptionRepository.GetSubscriptionListByCustomerId(customerId);
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }

    public OperationResponse<Entities.Model.Subscription.Subscription> GetBySubscriptionStatusBySoftwareIdAndCustomerId(int softwareId, int customerId)
    {
      OperationResponse<Entities.Model.Subscription.Subscription> response = new OperationResponse<Entities.Model.Subscription.Subscription>();

      try
      {
        var existsSubscription = _unitOfWork.SubscriptionRepository.GetSubscriptionBySoftwareIdAndCustomerId(softwareId, customerId);
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
