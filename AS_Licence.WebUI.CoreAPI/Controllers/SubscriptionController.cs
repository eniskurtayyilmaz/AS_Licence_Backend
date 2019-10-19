using AS_Licence.Entities.Model.Subscription;
using AS_Licence.Service.Interface.Subscription;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AS_Licence.WebUI.CoreAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class SubscriptionController : ControllerBase
  {
    private ISubscriptionManager _subscriptionManager;

    public SubscriptionController(ISubscriptionManager subscriptionManager)
    {
      _subscriptionManager = subscriptionManager;
    }


    [HttpPost]
    [Route("SaveSubscription")]
    public async Task<IActionResult> SaveSubscription([FromBody] Subscription subscription)
    {
      var subscriptionResult = await _subscriptionManager.SaveSubscription(subscription);
      if (subscriptionResult.Status == false)
      {
        return BadRequest(subscriptionResult);
      }

      return Ok(subscriptionResult);
    }

    [HttpGet]
    [Route("GetSubscriptionSummaryListByCustomerId")]
    public async Task<IActionResult> GetSubscriptionSummaryListByCustomerId(int customerId)
    {
      var subscriptionResult = await _subscriptionManager.GetSubscriptionSummaryListByCustomerId(customerId);
      if (subscriptionResult.Status == false)
      {
        return BadRequest(subscriptionResult);
      }

      return Ok(subscriptionResult);
    }

    [HttpGet]
    [Route("GetSubscriptionBySubscriptionId")]
    public async Task<IActionResult> GetSubscriptionBySubscriptionId(int subscriptionId)
    {
      var subscriptionResult = await _subscriptionManager.GetSubscriptionBySubscriptionId(subscriptionId);
      if (subscriptionResult.Status == false)
      {
        return BadRequest(subscriptionResult);
      }

      return Ok(subscriptionResult);
    }

    [HttpDelete]
    [Route("DeleteSubscriptionBySubscriptionId/{subscriptionId}")]
    public async Task<IActionResult> DeleteSubscriptionBySubscriptionId(int subscriptionId)
    {
      var subscriptionResult = await _subscriptionManager.DeleteSubscriptionBySubscriptionId(subscriptionId);
      if (subscriptionResult.Status == false)
      {
        return BadRequest(subscriptionResult);
      }

      return Ok(subscriptionResult);
    }
  }
}