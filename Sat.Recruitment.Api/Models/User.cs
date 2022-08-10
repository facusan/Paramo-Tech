﻿using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        public const string NormalUserType = "Normal";
        public const string SuperUserType = "Super";
        public const string PremiumUserType = "Premium";

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
            SetMoneyCalculationStrategyBasedOnUserType();
            CalculateFinalMoney();
            Email = EmailNormalizer.Normalize(Email);
        }

        private void SetMoneyCalculationStrategyBasedOnUserType()
        {
            if (UserType == NormalUserType)
            {
                SetMoneyCalculationStrategy(new NormalUserMoneyCalculationStrategy());
            }
            if (UserType == SuperUserType)
            {
                SetMoneyCalculationStrategy(new SuperUserMoneyCalculationStrategy());
            }
            if (UserType == PremiumUserType)
            {
                SetMoneyCalculationStrategy(new PremiumUserMoneyCalculationStrategy());
            }
        }
    }
}
