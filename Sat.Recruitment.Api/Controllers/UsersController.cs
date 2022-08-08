using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        public UsersController()
        {
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var errors = "";

            ValidateErrors(name, email, address, phone, ref errors);

            if (errors != null && errors != "")
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };

            User newUser = UserFactory.Create(name, email, address, phone, userType, money);

            try
            {

                var isDuplicated = new DuplicatedUserFinder().Find(newUser);
                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");

                    return new Result()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
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
