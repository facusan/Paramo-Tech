using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Exceptions;
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
                    var userNotCreatedBecauseIsDuplicatedMessage = string
                        .Format(Messages.UserNotCreatedBecauseIsDuplicated, user.Email);
                    _logger.LogError(userNotCreatedBecauseIsDuplicatedMessage);
                    return Conflict(userNotCreatedBecauseIsDuplicatedMessage);
                }

                _userRepository.Add(user);
                var userCreatedMessage = string.Format(Messages.UserCreated,user.Email);
                _logger.LogInformation(userCreatedMessage);
                return Ok(userCreatedMessage);
            }
            catch(UserNormalizationException e)
            {
                _logger.LogError(e.ToString());
                return Conflict(Messages.UserNormalizationError);
            }
            catch(Exception e)
            {
                _logger.LogCritical(e.ToString());
                return Problem();
            }
        }
    }
}
