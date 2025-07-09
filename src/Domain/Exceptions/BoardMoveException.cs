using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BoardMoveException : BaseException
    {
        public override int StatusCode => (int)HttpStatusCode.InternalServerError;
        public override string Error => "Board Move Error";

        public BoardMoveException(string message) : base(message)
        {
        }
    }
}
