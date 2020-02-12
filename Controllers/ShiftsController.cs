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

                var result = from e in _context.Shifts where e.OrganizationId == id
                    join d in _context.Positions on e.positionId equals d.Id into table1  
                    from d in table1.ToList()  
                    join i in _context.Employees on e.EmployeeId equals i.Id into table2  
                    from i in table2.ToList()  
                    select new
                    {  
                        Shift=e,  
                        Positions=d,  
                        Employees=i  
                    };

                // var result = _context.Shifts.Join(_context.Positions, 
                //     shift => shift.positionId,  
                //     positions => positions.Id,  
                //     (shift, positions) => new  
                //     {  
                //         Shift = shift,  
                //         Positions = positions,
                //     }).Where(x => x.Shift.OrganizationId == id);

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
        public async Task<IActionResult> Save([FromBody]ShiftModel helper)
        {
            try
            {
                TimeSpan ts = new TimeSpan(00, 00, 01);
                foreach (var item in helper.Amounts)
                {
                    if(item.Amount > 1)
                    {
                        for(int i = 1; i <= item.Amount; i++)
                        {
                            Shifts shift = new Shifts();
                            shift.OrganizationId = helper.OrganizationId;
                            shift.EmployeeId = null;
                            shift.positionId = item.Id;
                            shift.ShiftDate = helper.ShiftDate + ts;
                            shift.CreatedAt = DateTime.Now;
                            _context.Shifts.Add(shift);
                        }
                    }
                    else
                    {
                        Shifts shift = new Shifts();
                        shift.OrganizationId = helper.OrganizationId;
                        shift.EmployeeId = null;
                        shift.positionId = item.Id;
                        shift.ShiftDate = helper.ShiftDate + ts;
                        shift.CreatedAt = DateTime.Now;
                        _context.Shifts.Add(shift);
                    }
                }

                // for(int i = 0; i < helper.positionId.Length; i++)
                // {
                //     Shifts shift = new Shifts();
                //     shift.OrganizationId = helper.OrganizationId;
                //     shift.EmployeeId = null;
                //     shift.SortOrder = i + 1;
                //     shift.positionId = Convert.ToInt32(helper.positionId[i]);
                //     shift.ShiftDate = helper.ShiftDate;
                //     shift.CreatedAt = DateTime.Now;
                //     _context.Shifts.Add(shift);
                // }
                await _context.SaveChangesAsync();
                return Ok(new { message = "Shifts created" });
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
                
                existingHelper.EmployeeId = helpers.EmployeeId;
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
    }
}