using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test.MoneyCalculatationStrategyTesters
{
    [CollectionDefinition("UserMoneyCalculationStrategyTester")]
    public class NormalUserMoneyCalculationStrategyTester
    {
        [Fact]
        public void NormalUserWith100OfMoneyIsNotModifiedTest()
        {
            var user = new User
            {
                Money = 100
            };

            SetNormalUserMoneyCalculationStrategyAndCalculate(user);

            Assert.Equal(100, user.Money);
        }

        private static void SetNormalUserMoneyCalculationStrategyAndCalculate(User user)
        {
            user.SetMoneyCalculationStrategy(new NormalUserMoneyCalculationStrategy());
            user.CalculateFinalMoney();
        }

        [Fact]
        public void NormalUserWithLessThan10OfMoneyIsNotModifiedTest()
        {
            var user = new User
            {
                Money = 5
            };

            SetNormalUserMoneyCalculationStrategyAndCalculate(user);

            Assert.Equal(5, user.Money);
        }

        [Fact]
        public void NormalUserWithMoreThan100OfMoneyIsModifiedTest()
        {
            var user = new User
            {
                Money = (decimal)100.1
            };

            SetNormalUserMoneyCalculationStrategyAndCalculate(user);

            Assert.Equal((decimal)112.112, user.Money);
        }

        [Fact]
        public void NormalUserWithMoneyBetween10And100IsModifiedTest()
        {
            var user = new User
            {
                Money = 50
            };

            SetNormalUserMoneyCalculationStrategyAndCalculate(user);

            Assert.Equal(90, user.Money);
        }

    }
}
