namespace StarFood.Domain.Entities
{
    public class ProductCategories
    {
        public int Id { get; private set; }
        public string CategoryName { get; set; }
        public int RestaurantId { get; set; }
        public bool IsAvailable { get; set; }

        //public Restaurants Restaurant { get; set; }

        public void SetId(int id)
        {
            this.Id = id;
        }
        public ProductCategories() { }
    }
}
