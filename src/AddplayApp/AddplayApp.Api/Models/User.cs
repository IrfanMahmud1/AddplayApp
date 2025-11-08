namespace AddplayApp.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Age { get; set; }
        public string Email { get; set; } = default!;
        public DateTime TimeStamp { get; set; }
    }

}
