using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResourceConfigurator.Shared.Models.Events
{
    public class AddNewResourceEventResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
