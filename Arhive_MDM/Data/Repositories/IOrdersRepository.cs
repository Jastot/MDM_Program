using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arhive_MDM.Models;

namespace Arhive_MDM.Data.Repositories
{
    interface IOrdersRepository
    {
        /// <summary>Gets order.</summary>
        /// <param name="orderId">Id of the order.</param>
        /// <returns>Returns order.</returns>
        Task<Orders> GetOrder(int orderId);

        /// <summary>Get order's content.</summary>
        /// <param name="orderId">Id of the order.</param>
        /// <returns>Returns order's content.</returns>
        Task<List<OrderContent>> GetListOrdersContent(int orderId);
        Task<OrderContent> GetOrdersContent(int orderContentId);
        /// <summary>Gets all orders.</summary>
        /// <returns>Returns all orders.</returns>
        Task<List<Orders>> GetOrders();
        Task<List<Orders>> GetOrdersWithWorker(int workerId);
        Task<List<Orders>> GetOrdersWithWorkerAndEndDate(int workerId,DateTime endTime);
        Task<List<Orders>> GetOrdersinDates(DateTime datastart, DateTime dataend);

        Task<List<Orders>> GetClientsOrders(int clientId);

        /// <summary>Get all worker Case.</summary>
        /// <param name="workerId">Id of the worker Cases.</param>
        /// <returns>Returns all worker's Cases.</returns>
        Task<List<Orders>> GetWorkerOrders(int workerId);

        Task<OrderContent> GetOrderContentWithFile(int FileId);

        /// <summary>Creates order.</summary>
        /// <param name="order">Order data.</param>
        /// <returns>Returns id of the new order.</returns>
        Task<int> CreateOrder(Orders order);

        /// <summary>Creates order content.</summary>
        /// <param name="orderContent">Order content to create.</param>
        Task CreateListOrderContent(List<OrderContent> orderContent);
        Task CreateOrderContent(OrderContent orderContent);

        /// <summary>Updates order.</summary>
        /// <param name="order">Order data.</param>
        Task UpdateOrder(Orders order);

        Task UpdateOrderContentList(List<OrderContent> orderContent);
        /// <summary>Updates order content.</summary>
        /// <param name="orderContent">Order content to update.</param>
        Task UpdateOrderContent(OrderContent orderContent);

        /// <summary>Removes order and it's content.</summary>
        /// <param name="order">Order data.</param>
        Task RemoveOrder(Orders order);

        /// <summary>Removes order and it's content.</summary>
        /// <param name="orderContent">Order content to remove.</param>
        Task RemoveOrderContent(List<OrderContent> orderContent);

        
    }
}
