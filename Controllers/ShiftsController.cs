using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                var result = _context.Shifts.Join(_context.Positions,  
                    shift => shift.positionId,  
                    positions => positions.Id,  
                    (shift, positions) => new  
                    {  
                        Shift = shift,  
                        Positions = positions  
                    }).Where(x => x.Shift.OrganizationId == id);

                List<ShiftMergedModel> list = new List<ShiftMergedModel>();
                
                foreach (var item in result)
                {
                    ShiftMergedModel shift = new ShiftMergedModel();
                    shift.Id = item.Shift.Id;
                    shift.EmployeeId = item.Shift.EmployeeId;
                    shift.OrganizationId = item.Shift.OrganizationId;
                    shift.ShiftDate = item.Shift.ShiftDate;
                    shift.SortOrder = item.Shift.SortOrder;
                    shift.PositionId = item.Shift.positionId;
                    shift.PositionName = item.Positions.Name;
                    shift.DefaultTime = item.Positions.DefaultTime;
                    shift.CreatedAt = item.Shift.CreatedAt;
                    shift.CanceledAt = item.Shift.CanceledAt;
                    shift.IsCanceled = item.Shift.IsCanceled;

                    list.Add(shift);
                }

                return Ok(list);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is:" + ex.Message });
            }
            
        }

        [HttpGet("scheduler/{id}")]
        public IActionResult GetByIdForScheduler(Int32 id)
        {
            try
            {

                var result = _context.Shifts.Join(_context.Employees, 
                    shift => shift.EmployeeId,  
                    employees => employees.Id,  
                    (shift, employees) => new  
                    {  
                        Shift = shift,  
                        Employees = employees,
                    }).Where(x => x.Shift.OrganizationId == id);

                List<ForScheduler> list = new List<ForScheduler>();
                
                foreach (var item in result)
                {
                    ForScheduler shift = new ForScheduler();
                    shift.Id = item.Shift.Id;
                    shift.Start = item.Shift.ShiftDate;
                    shift.End = item.Shift.ShiftDate;
                    shift.Title = item.Employees.FirstName + " " + item.Employees.LastName;
                    shift.ResourceId = item.Shift.positionId;

                    
                    list.Add(shift);
                }

                return Ok(list);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is:" + ex.Message });
            }
            
        }

        [HttpPost]
        public IList<Shifts> Save([FromBody]ShiftModel helper)
        {
            List<Shifts> shiftList = new List<Shifts>();
            TimeSpan ts = new TimeSpan(00, 00, 01);

            foreach (var item in helper.Amounts)
            {
                for(int i = 1; i <= item.Amount; i++)
                {
                    Shifts shift = new Shifts();
                    shift.OrganizationId = helper.OrganizationId;
                    shift.EmployeeId = null;
                    shift.positionId = item.Id;
                    shift.ShiftDate = helper.ShiftDate + ts;
                    shift.CreatedAt = DateTime.Now;
                    
                    var insertedShift = GetShift(shift);
                    shiftList.Add(insertedShift);
                }
            }

            return shiftList.ToArray();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Int32 id, [FromBody]Shifts helpers)
        {
            try
            {
                var existingHelper = await _context.Shifts.Where(x => x.Id == helpers.Id).SingleOrDefaultAsync();
                if(helpers == null)
                    return BadRequest();
                if(helpers.EmployeeId != null)
                    existingHelper.EmployeeId = helpers.EmployeeId;
                else
                {
                    existingHelper.IsCanceled = helpers.IsCanceled;
                    existingHelper.CanceledAt = DateTime.Now;
                    existingHelper.CanceledBy = helpers.CanceledBy;
                }
                await _context.SaveChangesAsync(true);
                return new NoContentResult();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var shifts = await _context.Shifts.FindAsync(id);
                if (shifts == null)
                {
                    return NotFound();
                }

                _context.Shifts.Remove(shifts);
                await _context.SaveChangesAsync();
                return new NoContentResult();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = "Error is" + ex.Message });
            }
        }

        public Shifts GetShift(Shifts myShift)
        {
            _context.Shifts.Add(myShift);
            _context.SaveChanges();
            int id = myShift.Id;
            var res = _context.Shifts.Where(x => x.Id == id).Single();
            return res;
        }
    }
}