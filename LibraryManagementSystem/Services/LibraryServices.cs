using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.CustomException;

namespace LibraryManagementSystem.Services
{
    public class LibraryServices
    {
        private readonly string filePath = "libraryData.txt";
        private List<ILibraryItem> items = new List<ILibraryItem>();

        public LibraryServices()
        {
            LoadFromFile();
        }

        // ---------------- FILE I/O ----------------

        private void LoadFromFile()
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(',');

                if (parts.Length < 5)
                    continue;

                ILibraryItem item = parts[0] switch
                {
                    "Book" => new Book
                    {
                        Title = parts[1],
                        Publisher = parts[2],
                        PublicationYear = int.Parse(parts[3]),
                        Author = parts[4]
                    },

                    "Magazine" => new Magazine
                    {
                        Title = parts[1],
                        Publisher = parts[2],
                        PublicationYear = int.Parse(parts[3]),
                        IssueNumber = int.Parse(parts[4])
                    },

                    "Newspaper" => new Newspaper
                    {
                        Title = parts[1],
                        Publisher = parts[2],
                        PublicationYear = int.Parse(parts[3]),
                        Editor = parts[4]
                    },

                    _ => null
                };

                if (item != null)
                    items.Add(item);
            }
        }

        private void SaveToFile()
        {
            using StreamWriter writer = new StreamWriter(filePath, false);
            foreach (var item in items)
            {
                writer.WriteLine(item.ToFileString());
            }
        }

        // ---------------- ADD ITEM ----------------

        public void AddItem(ILibraryItem newItem)
        {
            foreach (var existingItem in items)
            {
                if (IsDuplicate(existingItem, newItem))
                    throw new DuplicateItemException("Duplicate item found.");
            }

            items.Add(newItem);
            SaveToFile();
        }

        // ---------------- REMOVE ITEM ----------------

        public void RemoveItem(string title)
        {
            var item = items.FirstOrDefault(i => i.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                items.Remove(item);
                SaveToFile();
            }
        }

        // ---------------- LIST ITEMS ----------------

        public void ListItems()
        {
            foreach (var item in items)
            {
                item.DisplayInfo();
            }
        }

        // ---------------- SEARCH ----------------

        public List<ILibraryItem> Search(string keyword)
        {
            return items
                .Where(i =>
                    i.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    (i is Book b && b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        // ---------------- SORT ----------------

        public List<ILibraryItem> SortByTitle()
        {
            return items.OrderBy(i => i.Title).ToList();
        }

        public List<ILibraryItem> SortByYear()
        {
            return items.OrderBy(i => i.PublicationYear).ToList();
        }

        // ---------------- DUPLICATE CHECK (REFACTORED) ----------------

        private bool IsDuplicate(ILibraryItem existingItem, ILibraryItem newItem)
        {
            // Different types → not duplicate
            if (existingItem.GetItemType() != newItem.GetItemType())
                return false;

            // Check common properties
            if (existingItem.Title != newItem.Title ||
                existingItem.Publisher != newItem.Publisher ||
                existingItem.PublicationYear != newItem.PublicationYear)
                return false;

            // Check item-specific properties
            if (existingItem is Book b1 && newItem is Book b2)
                return b1.Author == b2.Author;

            if (existingItem is Magazine m1 && newItem is Magazine m2)
                return m1.IssueNumber == m2.IssueNumber;

            if (existingItem is Newspaper n1 && newItem is Newspaper n2)
                return n1.Editor == n2.Editor;

            return false;
        }
    }
}
