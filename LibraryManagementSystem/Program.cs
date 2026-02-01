using System;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.CustomException;

namespace LibraryManagementSystem
{
    class Program
    {
        private static LibraryServices Library;

        static void Main(string[] args)
        {
            Library = new LibraryServices();

            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string choice = Console.ReadLine();
                exit = HandleMenuChoice(choice);
            }
        }

        // ---------------- MENU ----------------

        private static void DisplayMenu()
        {
            Console.WriteLine("\n===== Library Management System =====");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add Magazine");
            Console.WriteLine("3. Add Newspaper");
            Console.WriteLine("4. List Items");
            Console.WriteLine("5. Search by Title");
            Console.WriteLine("6. Search by Author");
            Console.WriteLine("7. Sort by Title");
            Console.WriteLine("8. Sort by Year");
            Console.WriteLine("9. Remove Item");
            Console.WriteLine("0. Exit");
            Console.Write("Enter choice: ");
        }

        // ---------------- MENU HANDLER ----------------

        private static bool HandleMenuChoice(string choice)
        {
            try
            {
                return choice switch
                {
                    "1" => ExecuteAddBook(),
                    "2" => ExecuteAddMagazine(),
                    "3" => ExecuteAddNewspaper(),
                    "4" => ExecuteListItems(),
                    "5" => ExecuteSearchByTitle(),
                    "6" => ExecuteSearchByAuthor(),
                    "7" => ExecuteSortByTitle(),
                    "8" => ExecuteSortByYear(),
                    "9" => ExecuteRemoveItem(),
                    "0" => ExecuteExit(),
                    _   => ExecuteInvalidChoice()
                };
            }
            catch (InvalidItemDataException ex)
            {
                Console.WriteLine($"Input Error: {ex.Message}");
                return false;
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Duplicate Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return false;
            }
        }

        // ---------------- EXECUTE METHODS ----------------

        private static bool ExecuteAddBook()
        {
            Book book = new Book
            {
                Title = GetInput("Title"),
                Publisher = GetInput("Publisher"),
                PublicationYear = GetIntInput("Publication Year"),
                Author = GetInput("Author")
            };

            Library.AddItem(book);
            Console.WriteLine("Book added.");
            return false;
        }

        private static bool ExecuteAddMagazine()
        {
            Magazine magazine = new Magazine
            {
                Title = GetInput("Title"),
                Publisher = GetInput("Publisher"),
                PublicationYear = GetIntInput("Publication Year"),
                IssueNumber = GetIntInput("Issue Number")
            };

            Library.AddItem(magazine);
            Console.WriteLine("Magazine added.");
            return false;
        }

        private static bool ExecuteAddNewspaper()
        {
            Newspaper newspaper = new Newspaper
            {
                Title = GetInput("Title"),
                Publisher = GetInput("Publisher"),
                PublicationYear = GetIntInput("Publication Year"),
                Editor = GetInput("Editor")
            };

            Library.AddItem(newspaper);
            Console.WriteLine("Newspaper added.");
            return false;
        }

        private static bool ExecuteListItems()
        {
            Library.ListItems();
            return false;
        }

        private static bool ExecuteSearchByTitle()
        {
            string title = GetInput("Enter title");
            var results = Library.Search(title);

            foreach (var item in results)
                item.DisplayInfo();

            return false;
        }

        private static bool ExecuteSearchByAuthor()
        {
            string author = GetInput("Enter author");
            var results = Library.Search(author);

            foreach (var item in results)
                item.DisplayInfo();

            return false;
        }

        private static bool ExecuteSortByTitle()
        {
            foreach (var item in Library.SortByTitle())
                item.DisplayInfo();

            return false;
        }

        private static bool ExecuteSortByYear()
        {
            foreach (var item in Library.SortByYear())
                item.DisplayInfo();

            return false;
        }

        private static bool ExecuteRemoveItem()
        {
            string title = GetInput("Enter title to remove");
            Library.RemoveItem(title);
            Console.WriteLine("Item removed (if found).");
            return false;
        }

        private static bool ExecuteExit()
        {
            Console.WriteLine("Exiting application...");
            return true;
        }

        private static bool ExecuteInvalidChoice()
        {
            Console.WriteLine("Invalid choice.");
            return false;
        }

        // ---------------- INPUT HELPERS ----------------

        private static string GetInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine();
        }

        private static int GetIntInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return int.Parse(Console.ReadLine());
        }
    }
}
