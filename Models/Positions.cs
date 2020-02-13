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
        public DateTime? TFrom { get; set; }
        public DateTime? TTo { get; set; }
        public int SortOrder { get; set; }
    }
}