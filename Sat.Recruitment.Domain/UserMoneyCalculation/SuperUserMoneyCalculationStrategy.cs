using System;

namespace Sat.Recruitment.Domain.UserMoneyCalculation
{
    public class SuperUserMoneyCalculationStrategy : IMoneyCalculatationStrategy
    {
        public decimal Calulate(User user)
        {
            if (user.Money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = user.Money * percentage;
                return user.Money + gif;
            }
            return user.Money;
        }
    }
}
