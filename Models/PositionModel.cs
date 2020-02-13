namespace WebApi.Models
{
    public class PositionModel
    {
        public int? Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string TFrom { get; set; }
        public string TTo { get; set; }
    }
    
}