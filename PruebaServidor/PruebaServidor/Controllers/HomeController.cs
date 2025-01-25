using PruebaServidor.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PruebaServidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly MyDbContext _context;

    }

    [HttpGet]
    public IEnumerable<Book> GetBooks()
    {
        return _context.Author;
    }
}
