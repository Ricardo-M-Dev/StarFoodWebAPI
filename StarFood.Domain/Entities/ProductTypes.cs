namespace StarFood.Domain.Entities
{
    public class ProductTypes
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int RestaurantId { get; set; }
        public bool IsAvailable { get; set; }

        public Restaurants Restaurant { get; set; }

        public ProductTypes() { }

        public ProductTypes(int id, string typeName, int restaurantId)
        {
            Id = id;
            TypeName = typeName;
            RestaurantId = restaurantId;
        }

        public void Update(string typeName)
        {
            TypeName = typeName;
        }

        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}
