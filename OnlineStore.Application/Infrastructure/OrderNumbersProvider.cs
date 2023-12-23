using OnlineStore.Application.Interfaces.Infrastructure;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain;

namespace OnlineStore.Application.Infrastructure
{
    public class OrderNumbersProvider : IOrderNumbersProvider
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly Random _random;

        public OrderNumbersProvider(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
            _random = new Random();
        }

        public async Task<string> GenerateNumberAsync(Order order, CancellationToken cancellation = default)
        {
            // Part 0: Prefix 2-digit number (number of items in the order)
            int itemsCount = order.Items.Count;
            string prefix = (itemsCount > 100 ? 99 : itemsCount).ToString();

            // Part 1: Current Date 6-digit number (YYMMDD format)
            string datePart = DateTime.Now.ToString("yyMMdd");

            // Part 2: Sequential 5-digit number (orders count for the month)
            string sequentialPart = 
                (await _ordersRepository.CountForLastMonthAsync(cancellation))
                .ToString()
                .PadLeft(5, '0');

            // Part 3: Random 3-digit number
            string randomPart = _random.Next(100, 1000).ToString();

            // Concatenate the parts and remove non-digit characters
            string orderNumber = string.Join(null, prefix, datePart, sequentialPart, randomPart);

            return orderNumber;
        }
    }
}
