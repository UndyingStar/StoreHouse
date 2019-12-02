using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFOrdersRepository : IOrdersRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Orders> Orders
        {
            get { return context.Orders; }
        }

        public void CreateOrder(Cart cart, ShippingDetails shippingDetails)
        {
            Orders orders = new Orders();
            foreach(var line in cart.Lines)
            {
                orders.TotalPrice += line.Material.Price * line.Quantity;
            }
            orders.ClientFullName = shippingDetails.Name;
            orders.Date = DateTime.Now;
            orders.Status = "Оплачен";
            orders.Type = "Заказ";
            orders.Email = shippingDetails.email;
            context.Orders.Add(orders);
            context.SaveChanges();
        }

        public void CreateDelievery(Cart cart, ShippingDetails shippingDetails)
        {
            Orders orders = new Orders();
            foreach (var line in cart.Lines)
            {
                orders.TotalPrice += line.Material.Price * line.Quantity;
            }
            orders.ClientFullName = shippingDetails.Name;
            orders.Date = DateTime.Now;
            orders.Status = "Поставлен";
            orders.Type = "Поставка";
            orders.Email = shippingDetails.email;
            context.Orders.Add(orders);
            context.SaveChanges();
        }

        public void EngageOrder(Orders order)
        {
            Orders dbEntry = context.Orders.Find(order.OrderID);
            if (dbEntry != null)
            {
                dbEntry.Status = "Передан службе доставки";
            }
            context.SaveChanges();
        }

        public void DelieveredOrder(Orders order)
        {
            Orders dbEntry = context.Orders.Find(order.OrderID);
            if (dbEntry != null)
            {
                dbEntry.Status = "Доставлен";
            }
            context.SaveChanges();
        }

    }
}
