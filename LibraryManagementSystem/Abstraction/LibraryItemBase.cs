using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.CustomException;

namespace LibraryManagementSystem.Abstraction
{
    public abstract class LibraryItemBase : ILibraryItem
    {
        // Private fields for encapsulation
        private string title;
        private string publisher;
        private int publicationYear;

        // Common property: Title
        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Title cannot be empty.");
                title = value;
            }
        }

        // Common property: Publisher
        public string Publisher
        {
            get => publisher;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Publisher cannot be empty.");
                publisher = value;
            }
        }

        // Common property: PublicationYear
        public int PublicationYear
        {
            get => publicationYear;
            set
            {
                if (value <= 0)
                    throw new InvalidItemDataException("Publication year must be a positive number.");
                publicationYear = value;
            }
        }

        // Abstract methods to be implemented by child classes
        public abstract string GetItemType();
        public abstract string ToFileString();
        public abstract void DisplayInfo();
    }
}
