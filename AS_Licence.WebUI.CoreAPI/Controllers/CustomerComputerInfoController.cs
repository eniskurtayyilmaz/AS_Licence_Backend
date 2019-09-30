using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Licence.Entities.Model.CustomerComputerInfo;
using AS_Licence.Service.Interface.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AS_Licence.WebUI.CoreAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  //TODO: AUTH Yazılacak.
  public class CustomerComputerInfoController : ControllerBase
  {
    private ICustomerComputerInfoManager _customerComputerInfoManager;

    public CustomerComputerInfoController(ICustomerComputerInfoManager customerComputerInfoManager)
    {
      _customerComputerInfoManager = customerComputerInfoManager;
    }

    [HttpGet]
    [Route("GetCustomerComputerInfoLists")]
    public async Task<IActionResult> Get()
    {
      var customerComputerInfoResult = await _customerComputerInfoManager.GetCustomerComputerInfoList();
      if (customerComputerInfoResult.Status == false)
      {
        return BadRequest(customerComputerInfoResult);
      }

      return Ok(customerComputerInfoResult);
    }

    [HttpPost]
    [Route("SaveCustomerComputerInfo")]
    public async Task<IActionResult> Post([FromBody] CustomerComputerInfo customerComputerInfo)
    {
      var customerComputerInfoResult = await _customerComputerInfoManager.SaveCustomerComputerInfo(customerComputerInfo);
      if (customerComputerInfoResult.Status == false)
      {
        return BadRequest(customerComputerInfoResult);
      }

      return Ok(customerComputerInfoResult);
    }
  }
}