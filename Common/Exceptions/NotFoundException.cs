using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class NotFoundException : HttpRequestException
    {
        public NotFoundException() : base("Not found", null, HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string? message) : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
