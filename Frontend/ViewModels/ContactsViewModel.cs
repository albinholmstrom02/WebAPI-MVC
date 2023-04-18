namespace Frontend.ViewModels
{
    public class ContactsViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
