using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Entidade Pedidos
    /// </summary>
    public class Orders
    {
        private int _id = 0;
        private OrderStatus _status = OrderStatus.Waiting;
        private int _tableId = 0;
        private int _userId = 0;
        private bool _paid = false;
        private DateTime _createdDate = DateTime.Now;
        private DateTime? _updatedDate = null;
        private DateTime? _deletedDate = null;
        private bool _deleted = true;
        private int _restaurantId = 0;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public OrderStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int TableId
        {
            get { return _tableId; }
            set { _tableId = value; }
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public bool Paid
        {
            get { return _paid; }
            set { _paid = value; }
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

        public Orders() {}
    }
}
