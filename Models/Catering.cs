using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Catering
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime CatDate { get; set; }
        public string OrganizationName { get; set; }
        public int AmountOfPeople { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DressCode { get; set; }
        public int Amount { get; set; }
    }
}