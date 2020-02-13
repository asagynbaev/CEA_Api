using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Positions
    {
        [Key]
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public DateTime? DefaultTime { get; set; }
        public DateTime? DefaultTime2 { get; set; }
        public int SortOrder { get; set; }
    }
}