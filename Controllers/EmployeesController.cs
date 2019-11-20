using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CeaContext _context;
        public EmployeesController(CeaContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpGet("get")]
        public IActionResult Get()
        {
            var result = _context.Employees.ToList();
            return Ok(result);
        }

        [HttpPost("saveemployer")]
        public async Task<IActionResult> SaveEmployer([FromBody]Employees employer)
        {
            employer.CreatedAt = DateTime.Now;
            _context.Employees.Add(employer);
            await _context.SaveChangesAsync();
            return Ok(employer);
        }
    }
}