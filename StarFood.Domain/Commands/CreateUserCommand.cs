namespace StarFood.Domain.Commands
{
    public class CreateUserCommand
    {
        public int Id { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Alias { get; set; }
        public Roles Role { get; set; }
        public int RestaurantId { get; set; }
        public bool IsActive { get; set; }
    }
}
