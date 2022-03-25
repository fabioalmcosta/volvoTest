using System;
using System.Net;

namespace TruckWebApi.Exceptions
{
    public class HttpStatusException : RankException
    {
        public HttpStatusCode Status { get; private set; }

        public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}
