using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class StepNotAllowedException : BaseException
    {
        public override int StatusCode => (int)HttpStatusCode.Conflict;
        public override string Error => "Not Allowed";

        public StepNotAllowedException(string message) : base(message)
        {
        }
    }
}
