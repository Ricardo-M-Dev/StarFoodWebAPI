namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Entidade que armazena dados das imagens dos Produtos
    /// </summary>
    public class ProductImages
    {
        private int _id = 0;
        private string _name = string.Empty;
        private string _imgUrl = string.Empty;
        private int _size = 0;
        private int _restaurantId = 0;
        private DateTime _createdDate = DateTime.Now;
        private DateTime? _updatedDate = null;
        private DateTime? _deletedDate = null;
        private bool _active = true;

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

        public string ImgUrl
        {
            get { return _imgUrl; }
            set { _imgUrl = value; }
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
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

        public DateTime? DeletedDate
        {
            get { return _deletedDate; }
            set { _deletedDate = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public ProductImages() { }
    }
}
