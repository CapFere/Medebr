using Medebr.Data.Entities;
using System.Collections.Generic;

namespace Medebr.Data.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        List<Checkout> GetOrders();
    }
}