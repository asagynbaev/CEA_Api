using System;

namespace WebApi.Models
{
    public class ForScheduler
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public int ResourceId { get; set; }
    }
}