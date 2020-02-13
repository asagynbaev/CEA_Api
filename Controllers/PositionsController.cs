using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
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

        [EnableCors("AllowOrigin")]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Positions.ToList();
            return Ok(result);
        }
        
        [EnableCors("AllowOrigin")]
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

        [EnableCors("AllowOrigin")]
        [HttpPost]
        public async Task<IActionResult> SavePosition([FromBody]PositionModel helper)
        {
            try
            {
                Positions positions = new Positions();
                positions.OrganizationId = helper.OrganizationId;
                positions.Name = helper.Name;
                
                if(!string.IsNullOrEmpty(helper.TFrom))
                {
                    helper.TFrom = helper.TFrom + ":00";
                    positions.TFrom = DateTime.ParseExact(helper.TFrom, "HH:mm:ss", CultureInfo.InvariantCulture);
                }

                if(!string.IsNullOrEmpty(helper.TTo))
                {
                    helper.TTo = helper.TTo + ":00";
                    positions.TTo = DateTime.ParseExact(helper.TTo, "HH:mm:ss", CultureInfo.InvariantCulture);
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

        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Int32 id, [FromBody]Positions helpers)
        {
            try
            {
                var existingHelper = await _context.Positions.Where(x => x.Id == helpers.Id).SingleOrDefaultAsync();
                if(helpers == null)
                    return BadRequest();
                
                existingHelper.Name = helpers.Name;
                existingHelper.TFrom = helpers.TFrom;
                existingHelper.TTo = helpers.TTo;
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