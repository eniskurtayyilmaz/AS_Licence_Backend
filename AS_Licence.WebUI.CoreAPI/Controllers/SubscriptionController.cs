using AS_Licence.Entities.Model.Subscription;
using AS_Licence.Service.Interface.Subscription;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AS_Licence.WebUI.CoreAPI.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    //TODO: Auth yazılacak
    public class SubscriptionController : ControllerBase
    {
      private ISubscriptionManager _subscriptionManager;

      public SubscriptionController(ISubscriptionManager subscriptionManager)
      {
        _subscriptionManager = subscriptionManager;
      }


      [HttpPost]
      [Route("SaveSubscription")]
      public async Task<IActionResult> Post([FromBody] Subscription subscription)
      {
        var subscriptionResult = await _subscriptionManager.SaveSubscription(subscription);
        if (subscriptionResult.Status == false)
        {
          return BadRequest(subscriptionResult);
        }

        return Ok(subscriptionResult);
      }

  }
}