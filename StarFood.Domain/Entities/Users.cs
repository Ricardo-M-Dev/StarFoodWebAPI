using System.ComponentModel.DataAnnotations;

namespace StarFood.Domain.Entities
{
    /// <summary>
    /// Entidade Usuários
    /// </summary>
    public class Users
    {
        private int _id = 0;
        private string _name = string.Empty;
        private string _imgUrl = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private DateTime? _birthDate = null;
        private string _gender = string.Empty;
        private string _role = string.Empty;
        private bool _active = false;
        private DateTime _createdDate = DateTime.Now;
        private DateTime? _updatedDate = null;
        private DateTime? _deletedDate = null;
        private int _restaurantId = 0;

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

        public string ImgUrl
        {
            get { return _imgUrl; }
            set { _imgUrl = value; }
        }

        [Required]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
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

        public int RestaurantId
        {
            get { return _restaurantId; }
            set { _restaurantId = value; }
        }

        public Users() { }
    }
}
