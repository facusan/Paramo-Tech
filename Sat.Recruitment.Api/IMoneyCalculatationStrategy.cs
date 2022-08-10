using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api
{
    public interface IMoneyCalculatationStrategy
    {
        public decimal Calulate(User user);
    }
}
