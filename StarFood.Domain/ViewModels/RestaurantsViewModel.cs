namespace StarFood.Domain.ViewModels
{
    public class RestaurantsViewModel
    {
        public int RestaurantId { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }

    }
}
