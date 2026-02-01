using System;

namespace LibraryManagementSystem.CustomException
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }
}
