using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Models;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UserMoneyCalculationStrategyTester")]
    public class SuperUserMoneyCalculationStrategyTester
    {
        [Fact]
        public void SuperUserWithMoreThan10OfMoneyIsModifiedTest()
        {
            var user = new User
            {
                Money = 101
            };
            
            SetSuperUserMoneyCalculationStrategyAndCalculate(user);
            
            Assert.Equal((decimal)121.2, user.Money);
        }

        private static void SetSuperUserMoneyCalculationStrategyAndCalculate(User user)
        {
            user.SetMoneyCalculationStrategy(new SuperUserMoneyCalculationStrategy());
            user.CalculateFinalMoney();
        }

        [Fact]
        public void SuperlUserWith100OfMoneyIsNotModifiedTest()
        {
            var user = new User
            {
                Money = 100
            };
            
            SetSuperUserMoneyCalculationStrategyAndCalculate(user);

            Assert.Equal(100, user.Money);
        }

        [Fact]
        public void SuperlUserWithLessThan100OfMoneyIsNotModifiedTest()
        {
            var user = new User
            {
                Money = 1
            };

            SetSuperUserMoneyCalculationStrategyAndCalculate(user);

            Assert.Equal(1, user.Money);
        }
    }
}
