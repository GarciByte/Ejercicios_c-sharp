using Microsoft.EntityFrameworkCore;

namespace PruebaServidor.Database
{
    public class MyDbContext: DbContext
    {
        private const string DATABASE_PATH = "books.db";

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            optionsBuilder.UseSqlite($"DataSourse={basedir}{DATABASE_PATH}");
        }
    }
}
