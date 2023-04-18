namespace Backend.Models.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public ProductType ProductType { get; set; }

    }

    public enum ProductType
    {
        Featured, Popular, New
    }
}
