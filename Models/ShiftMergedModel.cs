using System;

namespace WebApi.Models
{
    public class ShiftMergedModel
    {
        public int Id { get; set; }
        public DateTime ShiftDate { get; set; }
        public int? OrganizationId { get; set; }
        public int? SortOrder { get; set; }
        public string PositionName { get; set; }
        public DateTime? DefaultTime { get; set; }
    }
}