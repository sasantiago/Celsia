using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using celsia.Data;
using celsia.Models;
using celsia.Services.Interfaces;

using Microsoft.Extensions.Options;

namespace celsia.Controllers.Auth
{

  public class AuthLoginController : Controller
  {
    private readonly IAuthRepository _authRepository;
    public AuthLoginController(IAuthRepository authRepository)
    {
      _authRepository = authRepository;
    }



    public IActionResult Index()
    {
      return View();
    }


    [HttpPost]
    [Route("login")]
    public IActionResult LoginAdmin([FromBody] ClienteCred usercred)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        var user = _authRepository.Login(usercred.Email, usercred.Password);
        if (user == null)
        {
          return Unauthorized("Credenciales inválidas");
        }

        var tokenString = _authRepository.GenerateToken(user);
        return Ok(new { token = tokenString, userId = user.Id, email = user.Correo });
      }
      catch (Exception e)
      {
        return StatusCode(500, $"Error interno del servidor: {e.Message}");
      }
    }

    //data notation request

  }
}