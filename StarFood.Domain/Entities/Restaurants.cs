namespace StarFood.Domain.Entities
{
    public class Restaurants
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }

        public Restaurants() { }

        public Restaurants(int id, string name, bool isAvailable)
        {
            Id = id;
            Name = name;
            IsAvailable = isAvailable;
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}
