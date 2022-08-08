using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
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
        public async Task<IActionResult> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = "";

            ValidateErrors(name, email, address, phone, ref errors);

            if (errors != null && errors != "")
                return BadRequest(errors);

            User newUser = UserFactory.Create(name, email, address, phone, userType, money);

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
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return Problem("The user is duplicated");
            }
        }

        //Validate errors
        private void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }
    }
}
