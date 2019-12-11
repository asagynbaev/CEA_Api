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
    public class HelpersController : ControllerBase
    {
        private readonly CeaContext _context;
        public HelpersController(CeaContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Helpers.ToList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveHelper([FromBody]HelpersModel helper)
        {
            try
            {
                _context.Helpers.Add(helper);
                await _context.SaveChangesAsync();
                return Ok(helper);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Int32 id, [FromBody]HelpersModel helpers)
        {
            try
            {
                var existingHelper = await _context.Helpers.Where(x => x.Id == helpers.Id).SingleOrDefaultAsync();
                if(helpers == null)
                    return BadRequest();
                
                existingHelper.HelperName = helpers.HelperName;
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