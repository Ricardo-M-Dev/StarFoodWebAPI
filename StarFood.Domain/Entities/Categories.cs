namespace StarFood.Domain.Entities
{
    public class Categories
    {
        public int Id { get; private set; }
        public string CategoryName { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int RestaurantId { get; set; }
        public bool IsAvailable { get; set; }

        public void SetId(int id)
        {
            this.Id = id;
        }
        public Categories() { }
    }
}
