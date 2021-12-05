using Supermarket.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);   
    }
}
