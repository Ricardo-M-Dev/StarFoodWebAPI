namespace StarFood.Domain.ViewModels
{
    public class RestaurantsViewModel
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string? Name { get; set; }
        public bool IsAvailable { get; set; }

    }
}
