using Sat.Recruitment.Api.Models;
using System;

namespace Sat.Recruitment.Api
{
    public class UserFactory
    {
        public static User Create(User newUser)
        {

            if (newUser.UserType == "Normal")
            {
                newUser.SetMoneyCalculationStrategy(new NormalUserMoneyCalculationStrategy());
            }
            if (newUser.UserType == "SuperUser")
            {
                newUser.SetMoneyCalculationStrategy(new SuperUserMoneyCalculationStrategy());
            }
            if (newUser.UserType == "Premium")
            {
                newUser.SetMoneyCalculationStrategy(new PremiumUserMoneyCalculationStrategy());
            }
            newUser.CalculateFinalMoney();
            newUser.Email = EmailNormalizer.Normalize(newUser.Email);
            return newUser;
        }
    }
}
