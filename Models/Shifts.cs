using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Shifts
    {
        [Key]
        public int Id { get; set; }
        public int positionId { get; set; }
    }
}