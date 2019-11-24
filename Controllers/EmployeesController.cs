using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost("saveemployee")]
        public async Task<IActionResult> SaveEmployer([FromBody]Employees employer)
        {
            try
            {
                employer.CreatedAt = DateTime.Now;
                employer.Status = 1;
                _context.Employees.Add(employer);
                await _context.SaveChangesAsync();
                return Ok(employer);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Int32 id, [FromBody]Employees employees)
        {
            try
            {
                var existingEmployee = await _context.Employees.Where(x => x.Id == employees.Id).SingleOrDefaultAsync();
                if(existingEmployee == null)
                {
                    return BadRequest();
                }
                
                existingEmployee.FirstName = employees.FirstName;
                existingEmployee.LastName = employees.LastName;
                existingEmployee.BirthDate = employees.BirthDate;
                existingEmployee.Gender = employees.Gender;
                existingEmployee.Phone = employees.Phone;
                existingEmployee.English = employees.English;
                existingEmployee.Czech = employees.Czech;
                existingEmployee.Hotels = employees.Hotels;
                existingEmployee.Buses = employees.Buses;
                existingEmployee.Shops = employees.Shops;
                existingEmployee.Insurance = employees.Insurance;
                existingEmployee.VisaType = employees.VisaType;
                existingEmployee.WorkExperience = employees.WorkExperience;

                await _context.SaveChangesAsync(true);
                return new NoContentResult();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
        }
    }
}