using System;
using System.Collections.Generic;
using System.Text;

namespace User.Shared.Validation
{
    public class CreateUserException : Exception
    {
        public CreateUserException(string message = "") : base(message)
        {

        }
    }
}
