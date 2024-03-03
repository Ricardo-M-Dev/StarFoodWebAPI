using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Entidade Mesas
    /// </summary>
    public class Tables
    {
        private int _id = 0;
        private int _number = 0;
        private string _barcode = string.Empty;
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

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
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
        public Restaurants Restaurant { get; set; }

        public Tables() { }
    }
}
