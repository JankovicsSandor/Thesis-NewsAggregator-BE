using System.ComponentModel.DataAnnotations;

namespace ResourceConfiguration.API.Models.Input
{
    public class RequestResourceProperties
    {
        [Required]
        public string Url { get; set; }
    }
}
