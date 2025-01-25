
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaServidor.Database;
using System.Data.Entity;

namespace PruebaServidor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<MyDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetService<MyDbContext>();
                
                if (dbContext.Database.EnsureCreated())
                {
                    Author author = new Author {  Name = "Miguel" };
                    author.Books.Add(new Book { Name = "Quijote" });
                    dbContext.Author.Add(author);
                    dbContext.SaveChanges();

                }

            }

                app.Run();
        }
    }
}
