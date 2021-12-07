using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Interfaces
{
    public interface IRegistrationInterface
    {
        public Task<User> Register(string email, string number, string username, string firstname, string lastname, string role, AddressLocation location,
            int salary = 0);
    }
}
