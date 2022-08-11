using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        public const string CreateUserRoute = "/users";
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                user.Normalize();
                var isDuplicated = await _userRepository.ExistsAsync(user);
                if(isDuplicated)
                {
                    var userNotCreatedBecauseIsDuplicatedMessage = $"User {user.Email} not created because is duplicated";
                    _logger.LogError(userNotCreatedBecauseIsDuplicatedMessage, user);
                    return Conflict(userNotCreatedBecauseIsDuplicatedMessage);
                }

                _userRepository.Add(user);
                var userCreatedMessage = $"User {user.Email} created";
                _logger.LogInformation(userCreatedMessage);
                return Ok(userCreatedMessage);
            }
            catch(Exception e)
            {
                _logger.LogCritical(e.ToString());
                return Problem();
            }
        }
    }
}
