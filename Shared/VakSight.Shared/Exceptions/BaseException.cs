using System;

namespace VakSight.Shared.Exceptions
{
    public class BaseException : Exception
    {
        protected BaseException(string message) : base(message)
        {
        }

        public string ErrorCode { protected set; get; }
    }
}
