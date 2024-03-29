﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IOrdersRepository
    {
        IEnumerable<Orders> Orders { get; }
        void CreateOrder(Cart cart, ShippingDetails shippingDetails);
        void CreateDelievery(Cart cart, ShippingDetails shippingDetails);
        void EngageOrder(Orders order);
        void DelieveredOrder(Orders order);

    }
}
