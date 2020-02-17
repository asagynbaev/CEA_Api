using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class AmountModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
    }
    public class ShiftModel
    {
        //public string[] positionId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int OrganizationId { get; set; }
        public int positionId { get; set; }
        public int? Amount { get; set; }
        public int? EmployeeId { get; set; }
        public int? SortOrder { get; set; }
        public List<AmountModel> Amounts { get; set; }
    }
}