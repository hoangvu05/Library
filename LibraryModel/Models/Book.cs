namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string Author { get; set; }
        public string ReleaseYear {  get; set; }
        public string Publishing {  get; set; }
        public string ISBN { get; set; }

        public int? UserId { get; set; }

        public Boolean? Status { get; set; }

        public virtual User? User { get; set; }
    }
}
