using OnlineStore.BLL.Interfaces;

namespace OnlineStore.BLL.Services
{
    public class FixedDiscount : IDiscount
    {
        private readonly decimal _discountAmount;

        public FixedDiscount(decimal discountAmount)
        {
            _discountAmount = discountAmount;
        }

        public decimal ApplyDiscount(decimal totalPrice, int quantity)
        {
            return totalPrice - _discountAmount;
        }
    }

    public class PercentageDiscount : IDiscount
    {
        private readonly decimal _percentage;

        public PercentageDiscount(decimal percentage)
        {
            _percentage = percentage;
        }

        public decimal ApplyDiscount(decimal totalPrice, int quantity)
        {
            return totalPrice * (1 - _percentage / 100);
        }
    }
}