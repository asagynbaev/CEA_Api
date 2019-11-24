using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string Phone { get; set; }
        public int English { get; set; }
        public int Czech { get; set; }
        public bool Hotels { get; set; }
        public bool Buses { get; set; }
        public bool Shops { get; set; }
        public int Insurance { get; set; }
        public int VisaType { get; set; }
        public string WorkExperience { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
    }
}