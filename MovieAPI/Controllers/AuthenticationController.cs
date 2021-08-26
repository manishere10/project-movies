using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Contract;
using System;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class AuthenticationController : Controller
    {
        private readonly ITokenBuilderService _tokenBuilderService;

        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            ITokenBuilderService tokenBuilderService,
            ILogger<AuthenticationController> logger
            )
        {
            this._tokenBuilderService = tokenBuilderService;
            this._logger = logger;
        }

        
        [HttpGet]
        public async Task<ActionResult> Login(string email, string password)
        {
            try
            {
                var userExists = await this._tokenBuilderService.Authenticate(email, password);

                if (userExists)
                {
                    var token = await this._tokenBuilderService.GetToken(email);
                    return Ok(token);
                }
                    
                return this.Ok("User does not exist.");
            }
            catch (Exception exception)
            {
                this._logger.LogError($"Issue in AuthenticationController.Login :- { exception.Message }");

                return this.BadRequest("Something went wrong.");
            }
        }
    }
}
