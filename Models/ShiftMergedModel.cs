using System;

namespace WebApi.Models
{
    public class ShiftMergedModel
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int? OrganizationId { get; set; }
        public int? SortOrder { get; set; }
        public string PositionName { get; set; }
        public int PositionId { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? CanceledAt { get; set; }
        public int CanceledBy { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public int ResourceId { get; set; }
    }
}