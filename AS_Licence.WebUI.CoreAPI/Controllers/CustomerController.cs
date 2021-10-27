using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Licence.Entities.Model.Customer;
using AS_Licence.Service.Interface.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AS_Licence.WebUI.CoreAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class CustomerController : ControllerBase
  {
    private ICustomerManager _customerManager;
    public CustomerController(ICustomerManager customerManager)
    {
      _customerManager = customerManager;
    }


    [HttpGet]
    [Route("GetCustomerLists")]
    public async Task<IActionResult> Get()
    {
      
      var customerResult = await _customerManager.GetCustomerList(null, x=> x.OrderBy(y=> y.CustomerName));
      if (customerResult.Status == false)
      {
        return BadRequest(customerResult);
      }

      return Ok(customerResult);
    }

    [HttpGet]
    [Route("GetCustomerById")]
    public async Task<IActionResult> Get(int customerId)
    {
      var customerResult = await _customerManager.GetByCustomerId(customerId);
      if (customerResult.Status == false)
      {
        return BadRequest(customerResult);
      }

      return Ok(customerResult);
    }

    [HttpPost]
    [Route("SaveCustomer")]
    public async Task<IActionResult> Post([FromBody] Customer customer)
    {
      var customerResult = await _customerManager.SaveCustomer(customer);
      if (customerResult.Status == false)
      {
        return BadRequest(customerResult);
      }

      return Ok(customerResult);
    }


  }
}