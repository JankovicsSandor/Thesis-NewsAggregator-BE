using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceConfiguration.API.Models.Input
{
    public class NewConfigurationModel
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public string TitleProperty { get; set; }

        [Required]
        public string DescriptionProperty { get; set; }

        [Required]
        public string PublishProperty { get; set; }
    }
}
