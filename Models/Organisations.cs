using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Organisations
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string DressCode { get; set; }
        public byte[] Logo { get; set; }

    }
}