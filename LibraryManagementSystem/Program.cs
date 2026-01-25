using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    // Custom exception for invalid item data
    class InvalidItemDataException : Exception
    {
        public InvalidItemDataException(string message) : base(message) { }
    }

    // Custom exception for duplicate items
    class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }

    // Base class representing a library item
    class Item
    {
        private string title;
        private string publisher;
        private int publicationYear;

        public string Title
        {
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Title cannot be empty.");
                title = value;
            }
        }

        public string Publisher
        {
            get { return publisher; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Publisher cannot be empty.");
                publisher = value;
            }
        }

        public int PublicationYear
        {
            get { return publicationYear; }
            set
            {
                if (value <= 0)
                    throw new InvalidItemDataException("Publication year must be a positive number.");
                publicationYear = value;
            }
        }

        // Virtual method to demonstrate polymorphism
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Publisher: {Publisher}");
            Console.WriteLine($"Year: {PublicationYear}");
        }
    }

    // Derived class for books
    class Book : Item
    {
        private string author;

        public string Author
        {
            get { return author; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Author name cannot be empty.");
                author = value;
            }
        }

        // Overriding base class method
        public override void DisplayInfo()
        {
            Console.WriteLine("Item Type: Book");
            base.DisplayInfo();
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine();
        }
    }

    // Derived class for magazines
    class Magazine : Item
    {
        private int issueNumber;

        public int IssueNumber
        {
            get { return issueNumber; }
            set
            {
                if (value <= 0)
                    throw new InvalidItemDataException("Issue number must be a positive number.");
                issueNumber = value;
            }
        }

        // Overriding base class method
        public override void DisplayInfo()
        {
            Console.WriteLine("Item Type: Magazine");
            base.DisplayInfo();
            Console.WriteLine($"Issue Number: {IssueNumber}");
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // List to store library items
            List<Item> libraryItems = new List<Item>();

            try
            {
                // Creating a book object
                Book book1 = new Book
                {
                    Title = "C# Programming",
                    Publisher = "Tech Press",
                    PublicationYear = 2022,
                    Author = "John Smith"
                };

                AddItem(libraryItems, book1);

                // Creating a magazine object
                Magazine mag1 = new Magazine
                {
                    Title = "Tech Monthly",
                    Publisher = "Future Media",
                    PublicationYear = 2023,
                    IssueNumber = 5
                };

                AddItem(libraryItems, mag1);

                // Displaying all items using polymorphism
                Console.WriteLine("Library Items:\n");
                foreach (Item item in libraryItems)
                {
                    item.DisplayInfo();
                }
            }
            catch (InvalidItemDataException ex)
            {
                Console.WriteLine("Input Error: " + ex.Message);
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine("Duplicate Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
            finally
            {
                // This block always executes
                Console.WriteLine("Program execution completed.");
            }

            Console.ReadLine();
        }

        // Method to add items and prevent duplicates
        static void AddItem(List<Item> items, Item newItem)
        {
            foreach (Item item in items)
            {
                if (item.Title == newItem.Title)
                    throw new DuplicateItemException("An item with the same title already exists.");
            }

            items.Add(newItem);
        }
    }
}
