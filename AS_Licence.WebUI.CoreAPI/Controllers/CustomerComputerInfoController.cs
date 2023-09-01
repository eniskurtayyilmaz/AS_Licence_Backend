using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Licence.Entities.Model.CustomerComputerInfo;
using AS_Licence.Service.Interface.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AS_Licence.WebUI.CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerComputerInfoController : ControllerBase
    {
        private ICustomerComputerInfoManager _customerComputerInfoManager;

        public CustomerComputerInfoController(ICustomerComputerInfoManager customerComputerInfoManager)
        {
            _customerComputerInfoManager = customerComputerInfoManager;
        }

        [HttpGet]
        [Route("GetCustomerComputerInfoListsBySubscriptionId")]
        public async Task<IActionResult> Get(int subscriptionId)
        {
            var customerComputerInfoResult = await _customerComputerInfoManager.GetCustomerComputerInfoList(x => x.SubscriptionId == subscriptionId, orderBy => orderBy.OrderByDescending(x => x.CreatedDateTime));
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

        [HttpPost]
        [Route("UpdateCustomerComputerInfo")]
        public async Task<IActionResult> Put([FromBody] CustomerComputerInfo customerComputerInfo)
        {
            var customerComputerInfoResult = await _customerComputerInfoManager.UpdateCustomerComputerInfo(customerComputerInfo);
            if (customerComputerInfoResult.Status == false)
            {
                return BadRequest(customerComputerInfoResult);
            }

            return Ok(customerComputerInfoResult);
        }

        [HttpDelete]
        [Route("DeleteCustomerComputerInfo/{customerComputerInfoId}")]
        public async Task<IActionResult> Post(int customerComputerInfoId)
        {
            var customerComputerInfoResult = await _customerComputerInfoManager.DeleteCustomerComputerInfoByCustomerComputerInfoId(customerComputerInfoId);
            if (customerComputerInfoResult.Status == false)
            {
                return BadRequest(customerComputerInfoResult);
            }

            return Ok(customerComputerInfoResult);
        }
    }
}