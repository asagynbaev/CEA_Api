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
        public readonly CeaContext _context;
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
                var result = from Shift in _context.Shifts
                join Position in _context.Positions on Shift.positionId equals Position.Id
                join Employee in _context.Employees on Shift.EmployeeId equals Employee.Id into fff
                from asd in fff.DefaultIfEmpty()
                where Shift.OrganizationId == id
                select new {Shift, asd, Position};

                List<ShiftMergedModel> list = new List<ShiftMergedModel>();
                
                foreach (var item in result)
                {
                    var title = "";
                    if(item.Shift.EmployeeId != null)
                        title = item.asd.FirstName + " " + item.asd.LastName;

                    TimeSpan from = item.Position.DefaultTime.Value.TimeOfDay;
                    TimeSpan to = item.Position.DefaultTime2.Value.TimeOfDay;

                    ShiftMergedModel shift = new ShiftMergedModel();
                    shift.Id = item.Shift.Id;
                    shift.EmployeeId = item.Shift.EmployeeId;
                    shift.OrganizationId = item.Shift.OrganizationId;
                    shift.ShiftDate = item.Shift.ShiftDate;
                    shift.SortOrder = item.Shift.SortOrder;
                    shift.PositionId = item.Shift.positionId;
                    shift.PositionName = item.Position.DefaultTime.Value.ToString("HH:mm") + " " + item.Position.Name;
                    shift.Start = item.Shift.ShiftDate + from;
                    shift.End = item.Shift.ShiftDate + to;
                    shift.ResourceId = item.Shift.positionId;
                    shift.Title = title;
                    shift.CanceledBy = item.Shift.CanceledBy;
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

        [HttpPost]
        public IList<ShiftMergedModel> Save([FromBody]ShiftModel helper)
        {
            List<ShiftMergedModel> shiftList = new List<ShiftMergedModel>();
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
                var shiftToReturn = GetShift(existingHelper);
                if(helpers == null)
                    return BadRequest();
                if(helpers.EmployeeId != null)
                {
                    // For the database
                    existingHelper.EmployeeId = helpers.EmployeeId;

                    // For response
                    shiftToReturn.EmployeeId = helpers.EmployeeId;
                    var empName = _context.Employees.Where(x => x.Id == shiftToReturn.EmployeeId).Single();
                    shiftToReturn.Title = empName.FirstName + " " + empName.LastName;
                }
                else
                {
                    // For the database
                    existingHelper.IsCanceled = helpers.IsCanceled;
                    existingHelper.CanceledAt = DateTime.Now;
                    existingHelper.CanceledBy = helpers.CanceledBy;

                    // For response
                    shiftToReturn.IsCanceled = helpers.IsCanceled;
                    shiftToReturn.CanceledAt = DateTime.Now;
                    shiftToReturn.CanceledBy = helpers.CanceledBy;
                }
                await _context.SaveChangesAsync(true);
                return Ok(shiftToReturn);
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

        public ShiftMergedModel GetShift(Shifts myShift)
        {
            try
            {
                if(myShift.Id == 0)
                {
                    _context.Shifts.Add(myShift);
                    _context.SaveChanges();
                    int id = myShift.Id;
                    myShift = _context.Shifts.Where(x => x.Id == id).Single();
                }
                var positions = _context.Positions.Where(x => x.Id == myShift.positionId).Single();
                
                TimeSpan from = positions.DefaultTime.Value.TimeOfDay;
                TimeSpan to = positions.DefaultTime2.Value.TimeOfDay;

                ShiftMergedModel shift = new ShiftMergedModel();
                shift.Id = myShift.Id;
                shift.EmployeeId = myShift.EmployeeId;
                shift.OrganizationId = myShift.OrganizationId;
                shift.ShiftDate = myShift.ShiftDate;
                shift.PositionId = myShift.positionId;
                shift.PositionName = positions.DefaultTime.Value.ToString("HH:mm") + " " + positions.Name;
                shift.Start = myShift.ShiftDate + from;
                shift.End = myShift.ShiftDate + to;
                shift.ResourceId = myShift.positionId;
                shift.Title = "";
                shift.IsCanceled = myShift.IsCanceled;
                shift.CanceledBy = myShift.CanceledBy;
                shift.CanceledAt = myShift.CanceledAt;

                return shift;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}