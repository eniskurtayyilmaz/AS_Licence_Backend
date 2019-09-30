using AS_Licence.Data.Repository.Host.EntityFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AS_Licence.Data.Interface.DataAccess;
using AS_Licence.Data.Repository.UnitOfWork.EntityFramework;
using Xunit;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Entities.Model.Customer;
using AS_Licence.Entities.Model.CustomerComputerInfo;
using AS_Licence.Entities.Model.Software;
using AS_Licence.Entities.Model.Subscription;
using AS_Licence.Service.Host.Customer;
using AS_Licence.Service.Host.CustomerComputerInfo;
using AS_Licence.Service.Host.Software;
using AS_Licence.Service.Host.Subscription;
using AS_Licence.Service.Interface.Customer;
using AS_Licence.Service.Interface.Software;
using AS_Licence.Service.Interface.Subscription;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace AS_Licence.UnitOfWorkTests
{
  public class CustomerComputerInfoTests
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionManager _subscriptionManager;
    private readonly ISoftwareManager _softwareManager;
    private readonly ICustomerComputerInfoManager _customerComputerInfoManager;
    private readonly ICustomerManager _customerManager;

    private ITestOutputHelper _outputHelper;
    public CustomerComputerInfoTests(ITestOutputHelper outputHelper)
    {
      this._outputHelper = outputHelper;
      var optionsBuilder = new DbContextOptionsBuilder<EfAsLicenceContext>();
      optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AS_LICENCE;Data Source=.");

      EfAsLicenceContext context = new EfAsLicenceContext(optionsBuilder.Options);

      _unitOfWork = new EfUnitOfWork(context);

      _customerManager = new CustomerService(_unitOfWork);
      _softwareManager = new SoftwareService(_unitOfWork);
      _subscriptionManager = new SubscriptionService(_unitOfWork, _softwareManager);
      _customerComputerInfoManager = new CustomerComputerInfoService(_unitOfWork, _subscriptionManager);


    }
    [Fact]
    public async Task Can_Add_Subscription_Thought_UnitOfWork()
    {
      //Arrange
      var customer = new Customer() { CustomerEMail = "aliveli@hotmail.com", CustomerIsActive = true, CustomerName = "Adý soyadý", CustomerPhone = "123123" };
      var customerResult = await _customerManager.SaveCustomer(customer);
      Assert.True(customerResult.Status);

      var software = new Software() { SoftwareLastVersion = "v123", SoftwareName = "twitter", SoftwareIsActive = true, };
      var softwareResult = await _softwareManager.SaveSoftware(software);
      Assert.True(softwareResult.Status);

      var subscription = new Subscription() { CustomerId = customer.CustomerId, SoftwareId = software.SoftwareId, SubscriptionId = 0, SubScriptionEndDate = DateTime.Now.AddDays(2), SubScriptionLicenceCount = 2, SubScriptionStartDate = DateTime.Now, };
      var subscriptionResult = await _subscriptionManager.SaveSubscription(subscription);
      Assert.True(subscriptionResult.Status);

      var customerComputerInfo = new CustomerComputerInfo()
      {
        SubscriptionId = subscription.SubscriptionId,
        CustomerComputerInfoId = 0,
        CustomerComputerInfoHddSerialCode = "hddcode1234",
        CustomerComputerInfoMacSerialCode = "maccode1234",
        CustomerComputerInfoProcessSerialCode = "processserialcode1234",
      };

      //Action
      var result = await _customerComputerInfoManager.SaveCustomerComputerInfo(customerComputerInfo);

      //Asserts
      if (result.Status == false)
      {
        _outputHelper.WriteLine(result.Message);
      }

      Assert.True(result.Status);
      Assert.True(customerComputerInfo.CustomerComputerInfoId > 0);

      await Can_Delete_Exists_CustomerComputerInfo_Thought_UnitOfWork(customerComputerInfo.CustomerComputerInfoId);

      var customerDeleteResult = await _customerManager.DeleteCustomerByCustomerId(customer.CustomerId);
      Assert.True(customerDeleteResult.Status);

      var subscriptionDeleteResult = await _subscriptionManager.DeleteSubscriptionBySubscriptionId(subscription.SubscriptionId);
      Assert.True(subscriptionDeleteResult.Status);

      var softwareDeleteResult = await _softwareManager.DeleteSoftwareBySoftwareId(software.SoftwareId);
      Assert.True(softwareDeleteResult.Status);


    }


    private async Task Can_Delete_Exists_CustomerComputerInfo_Thought_UnitOfWork(int customerComputerInfoId)
    {
      //Arrange


      //Action
      var result = await _customerComputerInfoManager.DeleteCustomerComputerInfoByCustomerComputerInfoId(customerComputerInfoId);

      //Asserts
      if (result.Status == false)
      {
        _outputHelper.WriteLine(result.Message);
      }

      Assert.True(result.Status);

    }
  }
}
