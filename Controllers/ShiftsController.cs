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
    public class ShiftsController : ControllerBase
    {
        private readonly CeaContext _context;
        public ShiftsController(CeaContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Shifts.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Int32 id)
        {
            var result = _context.Shifts.Where(x => x.OrganizationId == id).ToList();
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult GetByDate([FromBody]ShiftModel helper)
        {
            var result = _context.Shifts.Where(x => x.ShiftDate == helper.ShiftDate).ToList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]ShiftModel helper)
        {
            try
            { 
                for(int i = 0; i < helper.positionId.Length; i++)
                {
                    Shifts shift = new Shifts();
                    shift.OrganizationId = helper.OrganizationId;
                    shift.EmployeeId = null;
                    shift.SortOrder = i + 1;
                    shift.positionId = Convert.ToInt32(helper.positionId[i]);
                    shift.ShiftDate = helper.ShiftDate;
                    shift.CreatedAt = DateTime.Now;
                    _context.Shifts.Add(shift);
                }
                await _context.SaveChangesAsync();
                return Ok(new { message = "Shift created" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Int32 id, [FromBody]Shifts helpers)
        {
            try
            {
                var existingHelper = await _context.Shifts.Where(x => x.Id == helpers.Id).SingleOrDefaultAsync();
                if(helpers == null)
                    return BadRequest();
                
                //existingHelper.HelperName = helpers.HelperName;
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