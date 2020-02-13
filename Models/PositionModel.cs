namespace WebApi.Models
{
    public class PositionModel
    {
        public int? Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
    }
    
}