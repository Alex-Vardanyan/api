using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class UserOverviewSpecification : BaseSpecification<User>
    {
        public UserOverviewSpecification(string email) : base(x => x.Email == email)
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Customer);
            AddInclude(x => x.Customer.Address);
            AddInclude(x => x.Employee.Address);
        }
    }
}
