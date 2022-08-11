using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.UserMoneyCalculation;
using Xunit;

namespace Sat.Recruitment.Test.MoneyCalculatationStrategyTesters
{
    [CollectionDefinition("UserMoneyCalculationStrategyTester")]
    public class PremiumUserMoneyCalculationStrategyTester
    {
        [Fact]
        public void PremiumUserWithMoreThan10OfMoneyIsModifiedTest()
        {
            var user = new User
            {
                Money = 101
            };

            SetPremiumUserMoneyCalculationStrategyAndCalculate(user);

            Assert.Equal(303, user.Money);
        }

        private static void SetPremiumUserMoneyCalculationStrategyAndCalculate(User user)
        {
            user.SetMoneyCalculationStrategy(new PremiumUserMoneyCalculationStrategy());
            user.CalculateFinalMoney();
        }

        [Fact]
        public void SuperlUserWith100OfMoneyIsNotModifiedTest()
        {
            var user = new User
            {
                Money = 100
            };

            SetPremiumUserMoneyCalculationStrategyAndCalculate(user);

            Assert.Equal(100, user.Money);
        }

        [Fact]
        public void SuperlUserWithLessThan100OfMoneyIsNotModifiedTest()
        {
            var user = new User
            {
                Money = 1
            };

            SetPremiumUserMoneyCalculationStrategyAndCalculate(user);

            Assert.Equal(1, user.Money);
        }
    }
}
