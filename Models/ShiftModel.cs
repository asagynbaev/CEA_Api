using System;

namespace WebApi.Models
{
    public class ShiftModel
    {
        public string[] positionId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int? OrganizationId { get; set; }
        public int? SortOrder { get; set; }
    }
}