using System;

namespace LibraryManagementSystem.CustomException
{
    public class InvalidItemDataException : Exception
    {
        public InvalidItemDataException(string message) : base(message) { }
    }
}
