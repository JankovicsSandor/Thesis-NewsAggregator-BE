using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceConfigurator.Shared.Event
{
    public class AddNewResourceEvent
    {
        public string ResourceName { get; set; }
        public string PictureUrl { get; set; }
    }
}
