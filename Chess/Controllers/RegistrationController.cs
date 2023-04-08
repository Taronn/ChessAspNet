using Chess.DTOs;
using Chess.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            RegistrationResult result = await _registrationService.RegisterAsync(registrationDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}