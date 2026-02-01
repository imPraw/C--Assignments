using System;
using LibraryManagementSystem.Abstraction;
using LibraryManagementSystem.CustomException;

namespace LibraryManagementSystem.Models
{
    public class Book : LibraryItemBase
    {
        private string author;

        public string Author
        {
            get => author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Author cannot be empty.");
                author = value;
            }
        }

        public override string GetItemType()
        {
            return "Book";
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Book] Title: {Title}, Author: {Author}, Publisher: {Publisher}, Year: {PublicationYear}");
        }

        public override string ToFileString()
        {
            // CSV format
            return $"Book,{Title},{Publisher},{PublicationYear},{Author}";
        }
    }
}
