namespace WebApi.Models
{
    public class PositionModel
    {
        public int? Id { get; set; }
        public int OrganizationId { get; set; }
        public string PositionName { get; set; }
        public string DefaultTime { get; set; }
    }
    
}