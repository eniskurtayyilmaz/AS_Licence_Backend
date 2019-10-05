using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Licence.Entities.ViewModel.Register;
using AS_Licence.Helpers.Encryption;
using AS_Licence.Service.Interface.RegisterComputer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AS_Licence.WebUI.CoreAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [AllowAnonymous]
  public class RegisterController : ControllerBase
  {
    private readonly IRegisterComputerManager _registerComputerManager;
    private readonly IConfiguration _configuration;
    private readonly EncryptionHelper _encryptionHelper;
    public RegisterController(IRegisterComputerManager registerComputerManager, IConfiguration configuration, EncryptionHelper encryptionHelper)
    {
      _registerComputerManager = registerComputerManager;
      _configuration = configuration;
      _encryptionHelper = encryptionHelper;
    }

    [HttpPost]
    [Route("CheckLicenceStatus")]
    public async Task<IActionResult> SaveRegisterComputer([FromBody] RegisterComputer registerComputer)
    {
      string encryptionText = _configuration["AppSettings:EncryptionKey"];

      var registerComputerResult = await _registerComputerManager.SaveRegisterComputer(registerComputer);

      string resultJson = JsonConvert.SerializeObject(registerComputerResult);
      encryptionText = _encryptionHelper.EncryptString(resultJson, encryptionText);

      if (registerComputerResult.Status == false)
      {
        return BadRequest(encryptionText);
      }

      return Ok(encryptionText);
    }
  }
}