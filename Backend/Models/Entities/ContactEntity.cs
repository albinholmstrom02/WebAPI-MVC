namespace Backend.Models.Entities
{
    public class ContactEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
    }
}
