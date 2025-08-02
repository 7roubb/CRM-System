using System;

namespace CRM.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { }
    }

    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message) { }
    }

    public class InvalidRoleException : Exception
    {
        public InvalidRoleException(string message) : base(message) { }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }

    public class DatabaseOperationException : Exception
    {
        public DatabaseOperationException(string message) : base(message) { }
    }
}
