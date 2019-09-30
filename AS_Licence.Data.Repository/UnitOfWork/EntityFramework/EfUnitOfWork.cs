using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AS_Licence.Data.Interface.DataAccess;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Data.Repository.Host.EntityFramework;
using AS_Licence.Entities.ViewModel.Operations;

namespace AS_Licence.Data.Repository.UnitOfWork.EntityFramework
{
  public class EfUnitOfWork : IUnitOfWork
  {
    private readonly EfAsLicenceContext _context;
    private ICustomerDal _customerRepository;
    private ICustomerComputerInfoDal _customerComputerInfoRepository;
    private ISoftwareDal _softwareRepository;
    private ISubscriptionDal _subscriptionRepository;


    public EfUnitOfWork(EfAsLicenceContext context)
    {
      this._context = context;
    }


    public ICustomerDal CustomerRepository
    {
      get
      {
        if (_customerRepository == null)
        {
          _customerRepository = new EfCustomerDal(_context);
        }

        return _customerRepository;
      }
    }

    public ICustomerComputerInfoDal CustomerComputerInfoRepository
    {
      get
      {
        if (_customerComputerInfoRepository == null)
        {
          _customerComputerInfoRepository = new EfCustomerComputerInfoDal(_context);
        }

        return _customerComputerInfoRepository;
      }
    }

    public ISoftwareDal SoftwareRepository
    {
      get
      {
        if (_softwareRepository == null)
        {
          _softwareRepository = new EfSoftwareDal(_context);
        }

        return _softwareRepository;
      }
    }

    public ISubscriptionDal SubscriptionRepository
    {
      get
      {
        if (_subscriptionRepository == null)
        {
          _subscriptionRepository = new EfSubscriptionDal(_context);
        }

        return _subscriptionRepository;
      }
    }

    //TODO: Logging için helper yazılacak ve buraya try catch içine eklenecek.
    public async Task<OperationResponse<string>> Save()
    {
      OperationResponse<string> response = new OperationResponse<string>();
      try
      {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
          await this._context.SaveChangesAsync();
          scope.Complete();
          response.Status = true;
          response.Message = "İşlem başarılı";
        }
      }
      catch (Exception e)
      {
        Debug.WriteLine(e);
        response.Status = false;
        response.Message = e.Message + e.InnerException?.Message;
      }

      return response;
    }
  }
}
