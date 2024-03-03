namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Relação entre Pedidos e Produtos
    /// </summary>
    public class ProductOrder
    {
        private int _id = 0;
        private int _orderId = 0;
        private int _variationId = 0;
        private string _description = string.Empty;
        private int _quantity = 0;
        private DateTime _createdDate = DateTime.Now;
        private DateTime? _updatedDate = null;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public int VariationId
        {
            get { return _variationId; }
            set { _variationId = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
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

        public ProductOrder() { }
    }
}
