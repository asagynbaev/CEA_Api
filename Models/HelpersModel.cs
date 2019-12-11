using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class HelpersModel
    {
        [Key]
        public int Id { get; set; }
        public int HelperId { get; set; }
        public string HelperName { get; set; }
    }
}