using System;
using System.Globalization;
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
            try
            {
                var result = _context.Positions.Where(x => x.OrganizationId == id).ToList();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePosition([FromBody]PositionModel helper)
        {
            try
            {
                Positions positions = new Positions();
                positions.OrganizationId = helper.OrganizationId;
                positions.Name = helper.Name;
                
                if(!string.IsNullOrEmpty(helper.DefaultTime))
                {
                    helper.DefaultTime = helper.DefaultTime + ":00";
                    positions.DefaultTime = DateTime.ParseExact(helper.DefaultTime, "HH:mm:ss", CultureInfo.InvariantCulture);
                }

                if(!string.IsNullOrEmpty(helper.DefaultTime2))
                {
                    helper.DefaultTime2 = helper.DefaultTime2 + ":00";
                    positions.DefaultTime2 = DateTime.ParseExact(helper.DefaultTime2, "HH:mm:ss", CultureInfo.InvariantCulture);
                }

                _context.Positions.Add(positions);
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
                
                existingHelper.Name = helpers.Name;
                existingHelper.DefaultTime = helpers.DefaultTime;
                existingHelper.DefaultTime2 = helpers.DefaultTime2;
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