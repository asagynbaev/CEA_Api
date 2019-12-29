using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Positions
    {
        [Key]
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string PositionName { get; set; }
        public DateTime? DefaultTime { get; set; }
        public int SortOrder { get; set; }
    }
}