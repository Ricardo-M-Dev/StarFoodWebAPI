namespace StarFood.Domain.Entities
{
    public class Restaurants
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }

        public Restaurants() { }
    }
}
