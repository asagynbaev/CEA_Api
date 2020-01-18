using System;
using System.Collections.Generic;
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
        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Employees.ToList();
            return Ok(result);
        }

        [HttpGet(("autocomplete"))]
        public IActionResult GetAutocmplete()
        {
            var result = _context.Employees.ToList();
            List<NewEmployeeModel> list = new List<NewEmployeeModel>();
            foreach (var item in result)
            {
                NewEmployeeModel newEmployee = new NewEmployeeModel();
                newEmployee.Id = item.Id;
                newEmployee.FullName = item.LastName + " " + item.FirstName;
                newEmployee.Hotels = item.Hotels;
                newEmployee.Shops = item.Shops;
                newEmployee.Buses = item.Buses;
                newEmployee.Status = item.Status;
                newEmployee.BusesTrain = item.BusesTrain;
                newEmployee.HotelsTrain = item.HotelsTrain;
                newEmployee.ShopsTrain = item.ShopsTrain;

                list.Add(newEmployee);
            }

            return Ok(list);
        }

        [HttpPost]
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
                existingEmployee.HotelsTrain = employees.HotelsTrain;
                existingEmployee.BusesTrain = employees.BusesTrain;
                existingEmployee.ShopsTrain = employees.ShopsTrain;
                existingEmployee.Insurance = employees.Insurance;
                existingEmployee.VisaType = employees.VisaType;
                existingEmployee.WorkExperience = employees.WorkExperience;
                existingEmployee.Cityzenship = employees.Cityzenship;
                existingEmployee.Advert = employees.Advert;

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