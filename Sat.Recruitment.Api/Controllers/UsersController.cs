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
        public const string CreateUserRoute = "/create-user";
        public UsersController()
        {
        }

        [HttpPost]
        [Route(CreateUserRoute)]
        public async Task<IActionResult> CreateUser(User user)
        {
            User newUser = UserFactory.Create(user);
            try
            {
                var isDuplicated = await new DuplicatedUserFinder().FindAsync(newUser);
                if (!isDuplicated)
                {
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
