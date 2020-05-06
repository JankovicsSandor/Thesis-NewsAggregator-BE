using System.ComponentModel.DataAnnotations;

namespace NewsReader.API.Models.Input
{
    public class RequestResourceProperties
    {
        [Required]
        public string Url { get; set; }
    }
}
