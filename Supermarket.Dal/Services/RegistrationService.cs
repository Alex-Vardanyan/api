using Supermarket.Models.Entities;
using Supermarket.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Dal.Services
{
    public class RegistrationService : IRegistrationInterface
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Register(string email, string number,string username, string firstname, string lastname, string role, AddressLocation location,
            int salary = 0)
        {
            
                _unitOfWork.Repository<AddressLocation>().Add(location);
                var user = new User { Email = email, Username = username };
                _unitOfWork.Repository<User>().Add(user);
            if (role == "Client")
            {
                var customer = new Customer
                {
                    Address = location,
                    User = user,
                    FirstName = firstname,
                    LastName = lastname,
                    PhoneNumber = number,
                    CreatedDate = DateTime.Now
                };
                _unitOfWork.Repository<Customer>().Add(customer);
            }
            else
            {
                var proffession = new Proffesion { ProfName = role };
                var employee = new Employee
                {
                    Address = location,
                    User = user,
                    FirstName = firstname,
                    LastName = lastname,
                    PhoneNumber = number,
                    Profession = proffession,
                    Salary = salary,
                    CreatedDate = DateTime.Now
                };
                _unitOfWork.Repository<Employee>().Add(employee);
            }
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                return null;
            }
            return user;
        }


    }
}
