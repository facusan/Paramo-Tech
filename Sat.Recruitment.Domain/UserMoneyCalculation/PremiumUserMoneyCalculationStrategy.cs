
namespace Sat.Recruitment.Domain.UserMoneyCalculation
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
