using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS_Licence.Entities.ViewModel.Register;
using AS_Licence.Service.Interface.RegisterComputer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AS_Licence.WebUI.CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
      private readonly IRegisterComputerManager _registerComputerManager;

      public RegisterController(IRegisterComputerManager registerComputerManager)
      {
        _registerComputerManager = registerComputerManager;
      }

      [HttpPost]
      [Route("CheckLicenceStatus")]
      public async Task<IActionResult> SaveRegisterComputer([FromBody] RegisterComputer registerComputer)
      {
        var registerComputerResult = _registerComputerManager.SaveRegisterComputer(registerComputer);
        if (registerComputerResult.Status == false)
        {
          return BadRequest(registerComputerResult);
        }

        return Ok(registerComputerResult);
      }
  }
}