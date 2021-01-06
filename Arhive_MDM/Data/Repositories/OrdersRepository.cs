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
        public Task<List<OrderContent>> GetListOrdersContent(int orderId) =>
            _context.OrderContents.Where(x => x.OrdersId == orderId).ToListAsync();
        public Task<OrderContent> GetOrdersContent(int orderContentId) =>
            _context.OrderContents.FirstOrDefaultAsync(x => x.Id == orderContentId);
        /// <inheritdoc />
        public Task<List<Orders>> GetOrders() =>
            _context.Orders.ToListAsync();
        public Task<List<Orders>> GetClientsOrders(int clientId) =>
            _context.Orders.Where(x => x.ClientId == clientId).ToListAsync();
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
        public async Task CreateListOrderContent(List<OrderContent> orderContent)
        {
            await _context.OrderContents.AddRangeAsync(orderContent);
            await _context.SaveChangesAsync();
        }
        public async Task CreateOrderContent(OrderContent orderContent)
        {
            await _context.OrderContents.AddAsync(orderContent);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task UpdateOrder(Orders order)
        {
            _context.Orders.Update(order);
            return _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task UpdateOrderContentList(List<OrderContent> orderContent)
        {
            _context.OrderContents.UpdateRange(orderContent);
            return _context.SaveChangesAsync();
        }
        public Task UpdateOrderContent(OrderContent orderContent)
        {
            _context.OrderContents.Update(orderContent);
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
