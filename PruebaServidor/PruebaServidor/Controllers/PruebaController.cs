using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaServidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<int> GetNumbers()
        {
            return [1, 2, 3, 4];
        }
    }
}
