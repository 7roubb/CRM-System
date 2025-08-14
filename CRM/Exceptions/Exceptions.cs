// CRM.Exceptions.cs
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

    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException(string message) : base(message) { }
    }

    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }
    }

    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }
    }

    // New exceptions for ContactService
    public class ContactNotFoundException : ResourceNotFoundException
    {
        public ContactNotFoundException(int id) : base($"Contact with ID {id} not found") { }
    }

    public class ContactAlreadyExistsException : Exception
    {
        public ContactAlreadyExistsException(string email) : base($"Contact with email '{email}' already exists") { }
    }
<<<<<<< HEAD
=======

    public class NoteNotFoundException : ResourceNotFoundException
    {
        public NoteNotFoundException(string message) : base(message) { }
    }

    public class NotesAlreadyExistsException : Exception
    {
        public NotesAlreadyExistsException(string message) : base(message) { }
    }
>>>>>>> main
}