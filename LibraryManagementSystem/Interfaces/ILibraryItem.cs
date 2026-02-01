namespace LibraryManagementSystem.Interfaces
{
    public interface ILibraryItem
    {
        string Title { get; set; }
        string Publisher { get; set; }
        int PublicationYear { get; set; }

        string GetItemType();
        string ToFileString();
        void DisplayInfo();
    }
}
