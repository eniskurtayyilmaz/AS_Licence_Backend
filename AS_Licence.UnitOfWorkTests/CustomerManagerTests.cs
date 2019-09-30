using AS_Licence.Data.Repository.Host.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AS_Licence.Data.Repository.UnitOfWork.EntityFramework;
using Xunit;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Entities.Model.Customer;
using AS_Licence.Service.Host.Customer;
using AS_Licence.Service.Interface.Customer;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace AS_Licence.UnitOfWorkTests
{
  public class CustomerManagerTests
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerManager _customerManager;
    private ITestOutputHelper _outputHelper;
    public CustomerManagerTests(ITestOutputHelper outputHelper)
    {
      this._outputHelper = outputHelper;
      var optionsBuilder = new DbContextOptionsBuilder<EfAsLicenceContext>();
      optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AS_LICENCE;Data Source=.");

      EfAsLicenceContext context = new EfAsLicenceContext(optionsBuilder.Options);

      _unitOfWork = new EfUnitOfWork(context);

      _customerManager = new CustomerService(_unitOfWork);
    }
    [Fact]
    public async Task Can_Add_Customer_Thought_UnitOfWork()
    {
      //Arrange
      var customerModel = new Customer()
      {
        CustomerId = 0,
        CustomerEMail = "kurtayyilmaz@gmail.com",
        CustomerName = "AdýSoyadý",
        CustomerIsActive = true,
        CustomerPhone = "Telephone",
      };

      //Action
      var result = await _customerManager.SaveCustomer(customerModel);

      //Asserts
      if (result.Status == false)
      {
        _outputHelper.WriteLine(result.Message);
      }

      Assert.True(result.Status);
      Assert.True(customerModel.CustomerId > 0);

     await Can_Delete_Exists_Customer_Thought_UnitOfWork(customerModel.CustomerId);
    }


    private async Task Can_Delete_Exists_Customer_Thought_UnitOfWork(int customerId)
    {
      //Arrange


      //Action
      var result = await _customerManager.DeleteCustomerByCustomerId(customerId);

      //Asserts
      if (result.Status == false)
      {
        _outputHelper.WriteLine(result.Message);
      }

      Assert.True(result.Status);

    }
  }
}
