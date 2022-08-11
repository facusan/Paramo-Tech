namespace Sat.Recruitment.Domain.UserMoneyCalculation
{
    public interface IMoneyCalculatationStrategy
    {
        public decimal Calulate(User user);
    }
}
