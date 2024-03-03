
using System.ComponentModel.DataAnnotations;

namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Entidade Forma de Pagamento
    /// </summary>
    public class Payment
    {
        private int _id = 0;
        private int _orderId = 0;
        private decimal _amount = 0.0m;
        private PaymentTypes _type = PaymentTypes.Cash;
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

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public PaymentTypes Type
        {
            get { return _type; }
            set { _type = value; }
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
    }
}
