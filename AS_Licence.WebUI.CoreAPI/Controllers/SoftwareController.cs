using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Licence.Entities.Model.Software;
using AS_Licence.Service.Interface.Software;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AS_Licence.WebUI.CoreAPI.Controllers
{
  //TODO:Auth ekle.
  [Route("api/[controller]")]
  [ApiController]
  public class SoftwareController : ControllerBase
  {
    
    private readonly ISoftwareManager _softwareManager;
    public SoftwareController(ISoftwareManager softwareManager)
    {
      _softwareManager = softwareManager;
    }

    [HttpGet]
    [Route("GetSoftwareLists")]
    //TODO:async hatalarını çözümle
    public async Task<IActionResult> Get()
    {
      var softwareResult = _softwareManager.GetSoftwareList();
      if (softwareResult.Status == false)
      {
        return BadRequest(softwareResult);
      }

      return Ok(softwareResult);
    }

    [HttpPost]
    [Route("SaveSoftware")]
    public async Task<IActionResult> Post([FromBody] Software software)
    {
      var softwareResult = _softwareManager.SaveSoftware(software);
      if (softwareResult.Status == false)
      {
        return BadRequest(softwareResult);
      }

      return Ok(softwareResult);
    }

    [HttpDelete]
    [Route("DeleteSoftware")]
    public async Task<IActionResult> Post(int softwareId)
    {
      var softwareResult = _softwareManager.DeleteSoftwareBySoftwareId(softwareId);
      if (softwareResult.Status == false)
      {
        return BadRequest(softwareResult);
      }

      return Ok(softwareResult);
    }
  }
}