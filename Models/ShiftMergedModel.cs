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
        public DateTime? DefaultTime { get; set; }
        public DateTime? DefaultTime2 { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? CanceledAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}