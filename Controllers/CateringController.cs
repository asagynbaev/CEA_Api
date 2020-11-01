using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers 
{
    [Route("[controller]")]
    [ApiController]
    public class CateringController : ControllerBase
    {
        private readonly CeaContext _context;
        public CateringController(CeaContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Catering.ToList();
            return Ok(result);
        }
    }
}