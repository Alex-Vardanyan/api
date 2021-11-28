using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class SupplierSpecParams
    {
        public string? Sort { get; set; }

        private string _search = string.Empty;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
