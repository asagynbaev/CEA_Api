using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly CeaContext _context;
        // private readonly DataAccess objds;
        public OrganizationsController(CeaContext context)
        {
            _context = context;
            // objds = d;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // var res = objds.GetOrganizations();
                var result = _context.Organizations.ToList();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _context.Organizations.Where(x => x.CategoryId == id).ToList();
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrganization([FromBody]Organizations helper)
        {
            try
            {
                _context.Organizations.Add(helper);
                await _context.SaveChangesAsync();
                return Ok(helper);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Int32 id, [FromBody]Organizations helpers)
        {
            try
            {
                var existingHelper = await _context.Organizations.Where(x => x.Id == helpers.Id).SingleOrDefaultAsync();
                if(helpers == null)
                    return BadRequest();
                
                existingHelper.Name = helpers.Name;
                existingHelper.Address = helpers.Address;
                existingHelper.DressCode = helpers.DressCode;
                existingHelper.Phone = helpers.Phone;
                existingHelper.CategoryId = helpers.CategoryId;
                await _context.SaveChangesAsync(true);
                return Ok(existingHelper);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
        }
    }
}