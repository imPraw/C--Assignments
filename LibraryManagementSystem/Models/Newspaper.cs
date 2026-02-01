using System;
using LibraryManagementSystem.Abstraction;
using LibraryManagementSystem.CustomException;

namespace LibraryManagementSystem.Models
{
    public class Newspaper : LibraryItemBase
    {
        private string editor;

        public string Editor
        {
            get => editor;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Editor name cannot be empty.");
                editor = value;
            }
        }

        public override string GetItemType()
        {
            return "Newspaper";
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Newspaper] Title: {Title}, Editor: {Editor}, Publisher: {Publisher}, Year: {PublicationYear}");
        }

        public override string ToFileString()
        {
            return $"Newspaper,{Title},{Publisher},{PublicationYear},{Editor}";
        }
    }
}
