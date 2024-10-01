using OnlineStore.BLL.Interfaces;

namespace OnlineStore.BLL.Services
{
    public class FixedDiscount : IDiscount
    {
        private readonly decimal _amount;

        public FixedDiscount(decimal amount)
        {
            _amount = amount;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            return originalPrice - _amount;
        }
    }

    public class PercentageDiscount : IDiscount
    {
        private readonly decimal _percentage;

        public PercentageDiscount(decimal percentage)
        {
            _percentage = percentage;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            return originalPrice * (1 - _percentage / 100);
        }
    }
}