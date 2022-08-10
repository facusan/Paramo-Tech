using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        public const string CreateUserRoute = "/users";
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                user.Normalize();
                var isDuplicated = await _userRepository.ExistsAsync(user);
                if (!isDuplicated)
                {
                    _userRepository.Add(user);
                    Debug.WriteLine("User Created");
                    return Ok("User Created");
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");
                    return Conflict("The user is duplicated");
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return Problem();
            }
        }
    }
}
