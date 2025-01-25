namespace PruebaServidor.Database
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = [];
    }
}
