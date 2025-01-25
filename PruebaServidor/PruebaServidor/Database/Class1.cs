using Microsoft.EntityFrameworkCore;

namespace PruebaServidor.Database
{
    [PrimaryKey(nameof(Id))]
    [Index(nameof(Name), IsUnique =)]
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Author Author { get; set; }
    }
}
