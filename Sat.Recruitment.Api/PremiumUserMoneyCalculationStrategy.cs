using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api
{
    public class PremiumUserMoneyCalculationStrategy : IMoneyCalculatationStrategy
    {
        public decimal Calulate(User user)
        {
            if (user.Money > 100)
            {
                var gif = user.Money * 2;
                return user.Money + gif;
            }
            return user.Money;
        }
    }
}
