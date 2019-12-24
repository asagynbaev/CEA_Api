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
    public class PositionsController : ControllerBase
    {
        private readonly CeaContext _context;
        public PositionsController(CeaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Positions.ToList();
            return Ok(result);
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _context.Positions.Where(x => x.OrganizationId == id).ToList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SavePosition([FromBody]Positions helper)
        {
            try
            {
                _context.Positions.Add(helper);
                await _context.SaveChangesAsync();
                return Ok(helper);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Int32 id, [FromBody]Positions helpers)
        {
            try
            {
                var existingHelper = await _context.Positions.Where(x => x.Id == helpers.Id).SingleOrDefaultAsync();
                if(helpers == null)
                    return BadRequest();
                
                existingHelper.PositionName = helpers.PositionName;
                existingHelper.DefaultTime = helpers.DefaultTime;
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