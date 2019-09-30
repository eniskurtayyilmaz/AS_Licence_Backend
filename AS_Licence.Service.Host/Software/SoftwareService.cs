using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Entites.Validation.Software;
using AS_Licence.Entities.ViewModel.Operations;
using AS_Licence.Helpers.Extension;
using AS_Licence.Service.Host.Subscription;
using AS_Licence.Service.Interface.Software;
using AS_Licence.Service.Interface.Subscription;
using FluentValidation.Results;

namespace AS_Licence.Service.Host.Software
{

  public class SoftwareService : ISoftwareManager
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionManager _subscriptionManager;

    public SoftwareService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      _subscriptionManager = new SubscriptionService(_unitOfWork, this);
    }

    public async Task<OperationResponse<List<Entities.Model.Software.Software>>> GetSoftwareList(
      Expression<Func<Entities.Model.Software.Software, bool>> filter = null,
      Func<IQueryable<Entities.Model.Software.Software>, IOrderedQueryable<Entities.Model.Software.Software>> orderBy =
        null, string includeProperties = "")
    {
      OperationResponse<List<Entities.Model.Software.Software>> response = new OperationResponse<List<Entities.Model.Software.Software>>();
      try
      {
        response.Data = await _unitOfWork.SoftwareRepository.Get(filter, orderBy, includeProperties);
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }



    public async Task<OperationResponse<Entities.Model.Software.Software>> SaveSoftware(
      Entities.Model.Software.Software software)
    {
      OperationResponse<Entities.Model.Software.Software> response = new OperationResponse<Entities.Model.Software.Software>();

      try
      {
        if (software == null)
        {
          throw new Exception("Software nesnesi null olamaz");
        }

        var valid = new SoftwareValidator().Validate(software);
        if (valid.IsValid == false)
        {
          throw new Exception(valid.GetErrorMessagesOnSingleLine());
        }

        Entities.Model.Software.Software softwareExists = null;
        if (software.SoftwareId > 0)
        {
          var existsSoftware = await _unitOfWork.SoftwareRepository.GetById(software.SoftwareId);
          softwareExists = existsSoftware ?? throw new Exception("Sistemde kayıtlı bir yazılım bilgisi bulunamadı.");
        }
        else
        {
          //Check software by name
          //İf exists good bye..
          var existsSoftware = await _unitOfWork.SoftwareRepository.GetSoftwareByName(software.SoftwareName);
          if (existsSoftware != null)
          {
            throw new Exception("Sistemde zaten bu isimde ile kayıtlı bir yazılım bilgisi var.");
          }
        }


        if (softwareExists != null)
        {
          softwareExists.SoftwareIsActive = software.SoftwareIsActive;
          softwareExists.SoftwareLastVersion = software.SoftwareLastVersion;
          softwareExists.SoftwareName = software.SoftwareName;
          softwareExists.UpdatedDateTime = DateTime.Now;

          await _unitOfWork.SoftwareRepository.Update(softwareExists);
        }
        else
        {
          software.CreatedDateTime = DateTime.Now;
          await _unitOfWork.SoftwareRepository.Insert(software);
        }

        response.Data = software;
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

    public async Task<OperationResponse<Entities.Model.Software.Software>> DeleteSoftwareBySoftwareId(int id)
    {
      OperationResponse<Entities.Model.Software.Software> response = new OperationResponse<Entities.Model.Software.Software>();

      try
      {

        var existsSoftware = await _unitOfWork.SoftwareRepository.GetById(id);
        if (existsSoftware == null)
        {
          throw new Exception("Sistemde kayıtlı bir yazılım bilgisi bulunamadı.");
        }

        var existsSubscriptions = await _subscriptionManager.GetSubscriptionListBySoftwareId(id);
        if (existsSubscriptions.Status == false)
        {
          throw new Exception(existsSubscriptions.Message);
        }

        if (existsSubscriptions.Data.Count > 0)
        {
          throw new Exception("Bu yazılımı silemezsiniz çünkü buna bağlı en az bir müşteride lisans tanımı yapılmış.");
        }
        await _unitOfWork.SoftwareRepository.Delete(existsSoftware);
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

    public async Task<OperationResponse<Entities.Model.Software.Software>> GetBySoftwareId(int id)
    {
      OperationResponse<Entities.Model.Software.Software> response = new OperationResponse<Entities.Model.Software.Software>();

      try
      {
        var existsSoftware = await _unitOfWork.SoftwareRepository.GetById(id);
        response.Data = existsSoftware ?? throw new Exception("Sistemde kayıtlı bir yazılım bilgisi bulunamadı.");
        response.Status = true;
      }
      catch (Exception e)
      {
        response.Status = false;
        response.Message = e.Message;
      }
      return response;
    }


    public async Task<OperationResponse<Entities.Model.Software.Software>> GetBySoftwareName(string softwareName)
    {
      OperationResponse<Entities.Model.Software.Software> response = new OperationResponse<Entities.Model.Software.Software>();

      try
      {
        var existsSoftware = await _unitOfWork.SoftwareRepository.GetSoftwareByName(softwareName);
        response.Data = existsSoftware ?? throw new Exception("Sistemde kayıtlı bir yazılım bilgisi bulunamadı.");
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
