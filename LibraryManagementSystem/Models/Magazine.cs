using System;
using LibraryManagementSystem.Abstraction;
using LibraryManagementSystem.CustomException;

namespace LibraryManagementSystem.Models
{
    public class Magazine : LibraryItemBase
    {
        private int issueNumber;

        public int IssueNumber
        {
            get => issueNumber;
            set
            {
                if (value <= 0)
                    throw new InvalidItemDataException("Issue number must be a positive number.");
                issueNumber = value;
            }
        }

        public override string GetItemType()
        {
            return "Magazine";
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Magazine] Title: {Title}, Issue: {IssueNumber}, Publisher: {Publisher}, Year: {PublicationYear}");
        }

        public override string ToFileString()
        {
            return $"Magazine,{Title},{Publisher},{PublicationYear},{IssueNumber}";
        }
    }
}
