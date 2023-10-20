using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Entites.Validation.Entity.Subscription;
using AS_Licence.Entities.ViewModel.Operations;
using AS_Licence.Entities.ViewModel.Subscription;
using AS_Licence.Helpers.Extension;
using AS_Licence.Service.Host.Customer;
using AS_Licence.Service.Host.CustomerComputerInfo;
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
        private readonly ICustomerComputerInfoManager _customerComputerInfoManager;
        public SubscriptionService(IUnitOfWork unitOfWork, ISoftwareManager softwareManager)
        {
            _unitOfWork = unitOfWork;
            _softwareManager = softwareManager;
            _customerManager = new CustomerService(_unitOfWork);
            _customerComputerInfoManager = new CustomerComputerInfoService(_unitOfWork, this);
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

                var existsComputer = await _customerComputerInfoManager.GetAlreadyComputerCountsBySubscriptionId(id);
                if (existsComputer.Status == false)
                {
                    throw new Exception(existsComputer.Message);
                }

                if (existsComputer.Data > 0)
                {
                    throw new Exception("Bu abonelik silinemez çünkü kayıt edilmiş bilgisayarları var. Mevcut aboneliği kaldırmak yerine pasif hale getiriniz.");
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

        public async Task<OperationResponse<Entities.Model.Subscription.Subscription>> GetSubscriptionBySubscriptionId(int id)
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

        public async Task<OperationResponse<List<SubscriptionInfo>>> GetSubscriptionSummaryListByCustomerId(int customerId)
        {
            OperationResponse<List<SubscriptionInfo>> response = new OperationResponse<List<SubscriptionInfo>>();
            try
            {
                var subscriptionList = await _unitOfWork.SubscriptionRepository.GetSubscriptionListByCustomerId(customerId);
                var softwareList = await _softwareManager.GetSoftwareList();

                List<SubscriptionInfo> list = new List<SubscriptionInfo>();
                foreach (var t in subscriptionList)
                {
                    Entities.Model.Software.Software soft = null;
                    if (softwareList.Status)
                    {
                        soft = softwareList.Data.FirstOrDefault(x => x.SoftwareId == t.SoftwareId);
                    }

                    var r = await _customerComputerInfoManager.GetAlreadyComputerCountsBySubscriptionId(t.SubscriptionId);

                    int alreadyCount = r.Status ? r.Data : 0;
                    var subscription = new SubscriptionInfo()
                    {
                        SoftwareId = t.SoftwareId,
                        CustomerId = t.CustomerId,
                        SubscriptionId = t.SubscriptionId,
                        SubScriptionEndDate = t.SubScriptionEndDate,
                        SubScriptionStartDate = t.SubScriptionStartDate,
                        SubscriptionIsActive = t.SubscriptionIsActive,
                        SubScriptionLicenceCount = t.SubScriptionLicenceCount,
                        SoftwareName = soft != null ? soft.SoftwareName : "",
                        SubScriptionCurrentLicenceCount = alreadyCount,
                    };

                    list.Add(subscription);

                }
                response.Data = list.OrderBy(x => x.SoftwareName).ToList();
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
