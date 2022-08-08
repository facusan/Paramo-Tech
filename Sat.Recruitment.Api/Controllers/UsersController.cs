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

        public UsersController()
        {
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User newUser = UserFactory.Create(user);

            try
            {
                var isDuplicated = new DuplicatedUserFinder().Find(newUser);
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
