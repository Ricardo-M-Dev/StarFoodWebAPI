using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Entidade Categorias
    /// </summary>
    public class Categories
    {
        private int _id = 0;
        private string _name = string.Empty;
        private DateTime _createdDate = DateTime.Now;
        private DateTime? _updatedDate = null;
        private DateTime? _deletedDate = null;
        private bool _status = true;
        private bool _deleted = false;
        private int _restaurantId = 0;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        public int RestaurantId
        {
            get { return _restaurantId; }
            set { _restaurantId = value; }
        }

        [JsonIgnore]
        public List<Products> Products { get; set; }

        [JsonIgnore]
        public Restaurants Restaurant { get; set; }

        public Categories() { }
    }
}
