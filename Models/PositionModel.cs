namespace WebApi.Models
{
    public class PositionModel
    {
        public int? Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string DefaultTime { get; set; }
        public string DefaultTime2 { get; set; }
    }
    
}