namespace StarFood.Domain.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int RestaurantId { get; set; }
        public bool IsAvailable { get; set; }

        public Restaurants Restaurant { get; set; }

        public Categories() { }

        public Categories(int id, string categoryName, int restaurantId)
        {
            Id = id;
            CategoryName = categoryName;
            RestaurantId = restaurantId;
        }

        public void Update(string categoryName)
        {
            CategoryName = categoryName;
        }
        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}
