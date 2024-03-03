using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Entidade Restaurantes
    /// </summary>
    public class Restaurants
    {
        private int _id = 0;
        private string? _name = null;
        private int _restaurantId = 0;
        private DateTime _createdDate = DateTime.Now;
        private DateTime? _updatedDate = null;
        private bool _status = true;
        private bool _deleted = false;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Required]
        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int RestaurantId
        {
            get { return _restaurantId; }
            set { _restaurantId = value; }
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
        public List<Products> Products { get; set; }
        [JsonIgnore]
        public List<Categories> Categories { get; set; }
        [JsonIgnore]
        public List<Variations> Variations { get; set; }
        [JsonIgnore]
        public List<Orders> Orders { get; set; }

        public Restaurants() { }

    }
}
