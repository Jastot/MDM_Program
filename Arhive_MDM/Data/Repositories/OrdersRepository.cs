using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Arhive_MDM.Models;

namespace Arhive_MDM.Data.Repositories
{
    class OrdersRepository : IOrdersRepository
    {
        private readonly DataContext _context;

        public OrdersRepository(DataContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public Task<Orders> GetOrder(int orderId) =>
            _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

        /// <inheritdoc />
        public Task<List<OrderContent>> GetOrdersContent(int orderId) =>
            _context.OrderContents.Where(x => x.OrdersId == orderId).ToListAsync();

        /// <inheritdoc />
        public Task<List<Orders>> GetOrders() =>
            _context.Orders.ToListAsync();

        /// <inheritdoc />
        public Task<OrderContent> GetOrderContentWithFile(int fileId) =>
            _context.OrderContents.FirstOrDefaultAsync(x => x.FileId == fileId);

        /// <inheritdoc />
        public async Task<int> CreateOrder(Orders order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        /// <inheritdoc />
        public async Task CreateOrderContent(List<OrderContent> orderContent)
        {
            await _context.OrderContents.AddRangeAsync(orderContent);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task UpdateOrder(Orders order)
        {
            _context.Orders.Update(order);
            return _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task UpdateOrderContent(List<OrderContent> orderContent)
        {
            _context.OrderContents.UpdateRange(orderContent);
            return _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task RemoveOrder(Orders order)
        {
            // как тут тогда надо. haihai выходит сносить бд ? и
            // не, репозитории просто sql запросы.
            // sql create это только dataContext
            // репозитории просто выдают инфу или обновляют её
            //короче отражения не будет на самой бд. окей.
            // да, только на самих данных, так что всё ок.
            var orderContent = await _context.OrderContents.Where(x => x.OrdersId == order.Id).ToListAsync();
            _context.OrderContents.RemoveRange(orderContent);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task RemoveOrderContent(List<OrderContent> orderContent)
        {
            _context.OrderContents.RemoveRange(orderContent);
            return _context.SaveChangesAsync();
        }
    }
}
