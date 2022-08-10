using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }
        public void Add(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ExistsAsync(User user)
        {
            var users = await FindAllAsync();
            foreach (var currentUser in users)
            {
                if (EmailOrPhoneExist(user, currentUser) || NameAndAddressExist(user, currentUser))
                    return true;
            }
            return false;
        }

        private static bool EmailOrPhoneExist(User user, User currentUser) =>
            user.Email == currentUser.Email || user.Phone == currentUser.Phone;

        private static bool NameAndAddressExist(User user, User currentUser) =>
            user.Name == currentUser.Name && user.Address == currentUser.Address;

        public async Task<List<User>> FindAllAsync()
        {
            List<User> users = new List<User>();
            var path = Directory.GetCurrentDirectory() + _configuration.GetValue<string>("Settings:UserFilePath");

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var lineSplitted = line.Split(',');
                var user = new User
                {
                    Name = lineSplitted[0].ToString(),
                    Email = lineSplitted[1].ToString(),
                    Phone = lineSplitted[2].ToString(),
                    Address = lineSplitted[3].ToString(),
                    UserType = lineSplitted[4].ToString(),
                    Money = decimal.Parse(lineSplitted[5].ToString()),
                };
                users.Add(user);
            }
            reader.Close();
            return users;
        }
    }
}
