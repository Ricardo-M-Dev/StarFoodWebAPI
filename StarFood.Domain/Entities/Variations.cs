using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Entidade Variações
    /// </summary>
    public class Variations
    {
        private int _id = 0;
        private string _name = string.Empty;
        private decimal _value = 0.0m;
        private DateTime _createdDate = DateTime.Now;
        private DateTime? _updatedDate = null;
        private DateTime? _deletedDate = null;
        private int _productId = 0;
        private int _restaurantId = 0;
        private bool _status = true;
        private bool _deleted = false;
        private Products _products = new Products();

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Required]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public DateTime? UpdatedDate
        {
            get { return _updatedDate; }
            set { _updatedDate = value; }
        }

        public DateTime? DeletedDate
        {
            get { return _deletedDate; }
            set { _deletedDate = value; }
        }

        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        public int RestaurantId
        {
            get { return _restaurantId; }
            set { _restaurantId = value; }
        }

        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public bool Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        [JsonIgnore]
        public Products Products { get; set; }
        [JsonIgnore]
        public Restaurants Restaurant { get; set; }

        public Variations() { }
    }


}
