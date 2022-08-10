using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string UserType { get; set; }
        public decimal Money { get; set; }

        private IMoneyCalculatationStrategy _moneyCalculatationStrategy;

        public void SetMoneyCalculationStrategy(IMoneyCalculatationStrategy strategy)
        {
            _moneyCalculatationStrategy = strategy;
        }

        public void CalculateFinalMoney()
        {
            Money = _moneyCalculatationStrategy.Calulate(this);
        }

        public void Normalize()
        {
            if (UserType == "Normal")
            {
                SetMoneyCalculationStrategy(new NormalUserMoneyCalculationStrategy());
            }
            if (UserType == "SuperUser")
            {
                SetMoneyCalculationStrategy(new SuperUserMoneyCalculationStrategy());
            }
            if (UserType == "Premium")
            {
                SetMoneyCalculationStrategy(new PremiumUserMoneyCalculationStrategy());
            }
            CalculateFinalMoney();
            Email = EmailNormalizer.Normalize(Email);
        }
    }
}
