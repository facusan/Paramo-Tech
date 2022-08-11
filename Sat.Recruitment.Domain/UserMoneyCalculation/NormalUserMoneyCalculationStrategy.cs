using System;

namespace Sat.Recruitment.Domain.UserMoneyCalculation
{
    public class NormalUserMoneyCalculationStrategy : IMoneyCalculatationStrategy
    {
        public decimal Calulate(User user)
        {
            if (user.Money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                var gif = user.Money * percentage;
                return user.Money + gif;
            }
            if (user.Money > 10 && user.Money < 100)
            {
                var percentage = Convert.ToDecimal(0.8);
                var gif = user.Money * percentage;
                return user.Money + gif;
            }
            return user.Money;
        }
    }
}
