using System.ComponentModel;

namespace StarFood.Domain.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Alias { get; set; }
        public Roles Role { get; set; }
        public int RestaurantId { get; set; }
        [DefaultValue(false)]
        public bool IsActive { get; set; }
    }
}
