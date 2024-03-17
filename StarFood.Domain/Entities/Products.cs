using System.Text.Json.Serialization;

namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Entidade Produtos
    /// </summary>
    public class Products
    {
        private int _id = 0;
        private string _name = string.Empty;
        private string? _description = null;
        private string? _imgUrl = null;
        private DateTime _createdDate = DateTime.Now;
        private DateTime? _updatedDate = null;
        private DateTime? _deletedDate = null;
        private int _categoryId = 0;
        private int _restaurantId = 0;
        private bool _status = true;
        private bool _deleted = false;

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

        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string? ImgUrl
        {
            get { return _imgUrl; }
            set { _imgUrl = value; }
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

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
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
        public Categories Category { get; set; }

        [JsonIgnore]
        public Restaurants Restaurant { get; set; }

        [JsonIgnore]
        public List<Variations> Variations { get; set; }

        public Products() { }
    }
}
