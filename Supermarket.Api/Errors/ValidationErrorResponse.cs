using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Errors
{
    public class ValidationErrorResponse : ApiResponse
    {
        public ValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<String> Errors { get; set; }
    }
}
