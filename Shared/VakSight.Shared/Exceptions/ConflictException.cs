using System.Collections.Generic;

namespace VakSight.Shared.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(string message, Dictionary<string, string> conflictFieldErrors) : base(message)
        {
            ErrorCode = ErrorCodes.Conflict;
            ConflictFieldErrors = conflictFieldErrors ?? new Dictionary<string, string>();
        }

        public Dictionary<string, string> ConflictFieldErrors { get; }
    }
}
