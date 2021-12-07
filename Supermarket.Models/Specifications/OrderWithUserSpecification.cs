using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class OrderWithUserSpecification : BaseSpecification<OrderProduct>
    {
        public OrderWithUserSpecification(User user) : base(x => (x.Order.Customer == user) && x.Order.OrderStatusId == 1)
        {
            AddInclude(x => x.Order);
        }
    }
}
