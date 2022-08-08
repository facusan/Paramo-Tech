using System;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;

using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTester
    {
        [Fact]
        public async void CreateUserOkTest()
        {
            var userController = new UsersController();

            var result = await userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async void CreateDuplicatedUserErrorTest()
        {
            var userController = new UsersController();

            var result = await userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var conflictObjectResult = result as ConflictObjectResult;

            Assert.NotNull(conflictObjectResult);
            Assert.Equal(StatusCodes.Status409Conflict, conflictObjectResult.StatusCode);
            Assert.Equal("The user is duplicated", conflictObjectResult.Value);
        }
    }
}
