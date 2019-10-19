using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AS_Licence.Entites.Dto;
using AS_Licence.Entites.Validation.Dto.User;
using AS_Licence.Entities.Model.User;
using AS_Licence.Entities.ViewModel.Operations;
using AS_Licence.Helpers.Extension;
using AS_Licence.Service.Interface.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AS_Licence.WebUI.CoreAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IUserManager _userManager;
    private readonly IConfiguration _configuration;
    public AuthController(IUserManager userManager, IConfiguration configuration)
    {
      _userManager = userManager;
      _configuration = configuration;
    }
    
    [Authorize]
    [HttpPost("ChangeUserPassword")]
    public async Task<IActionResult> ChangeUserPassword([FromBody] UserForPasswordChangeDto userForPasswordChangeDto)
    {
    

      var valid = await new UserForPasswordChangeDtoValidators().ValidateAsync(userForPasswordChangeDto);
      if (!valid.IsValid)
      {
        OperationResponse<bool> validResponse = new OperationResponse<bool>()
        {
          Status = false,
          Message = valid.GetErrorMessagesOnSingleLine()
        };
        return BadRequest(validResponse);
      }


      var userExistsResult = await _userManager.ChangeUserPassword(User.Identity.Name, userForPasswordChangeDto.CurrentPassword, userForPasswordChangeDto.NewPassword, userForPasswordChangeDto.AgainNewPassword);
      if (userExistsResult.Status == false)
      {
        return BadRequest(userExistsResult);
      }

      return Ok(userExistsResult);
    }


    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
      var valid = await new UserForRegisterDtoValidator().ValidateAsync(userForRegisterDto);
      if (!valid.IsValid)
      {
        OperationResponse<bool> validResponse = new OperationResponse<bool>()
        {
          Status = false,
          Message = valid.GetErrorMessagesOnSingleLine()
        };
        return BadRequest(validResponse);
      }

      userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

      var userExistsResult = await _userManager.UserExists(userForRegisterDto.Username);
      if (userExistsResult.Status == true)
      {
        return BadRequest(userExistsResult);
      }


      var userToCreate = new User()
      {
        UserName = userForRegisterDto.Username,
        UserIsActive = false, // Önemli 
        CreatedDateTime = DateTime.Now
      };

      var createdUser = await _userManager.RegisterUser(userToCreate, userForRegisterDto.Password);
      if (createdUser.Status == false)
      {
        return BadRequest(createdUser);
      }

      return Ok(createdUser);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
      var valid = await new UserForLoginDtoValidator().ValidateAsync(userForLoginDto);
      if (!valid.IsValid)
      {
        OperationResponse<bool> validResponse = new OperationResponse<bool>()
        {
          Status = false,
          Message = valid.GetErrorMessagesOnSingleLine()
        };
        return BadRequest(validResponse);
      }

      

      var userFromRepo = await _userManager.LoginUser(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
      if (userFromRepo.Status == false)
      {
        return Unauthorized(userFromRepo);
      }

      var claims = new[] {
        new Claim (ClaimTypes.NameIdentifier, userFromRepo.Data.UserId.ToString ()),
        new Claim (ClaimTypes.Name, userFromRepo.Data.UserName.ToString ()),
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddMinutes(15),
        SigningCredentials = creds
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return Ok(new { token = tokenHandler.WriteToken(token), message = "Hoşgeldiniz.. " + userFromRepo.Data.UserName });
    }

    [Authorize]
    [HttpGet("GetUserInformation")]
    public async Task<IActionResult> GetUserInformation()
    {
      var response = await _userManager.GetUserInformationByUsername(User.Identity.Name);
      if (response.Status == false)
      {
        return BadRequest(response);
      }
      return Ok(response);
    }
  }
}