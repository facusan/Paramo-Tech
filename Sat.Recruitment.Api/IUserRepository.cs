using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api
{
    public interface IUserRepository
    {
        public Task<bool> ExistsAsync(User user);
        public void Add(User user);
        public Task<List<User>> FindAllAsync();
    }
}
