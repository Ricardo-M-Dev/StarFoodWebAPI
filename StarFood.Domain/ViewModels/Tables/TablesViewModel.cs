namespace StarFood.Domain.ViewModels.Tables
{
    public class TablesViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool Status { get; set; }
        public bool Active { get; set; }
        public int RestaurantId { get; set; }
    }
}
