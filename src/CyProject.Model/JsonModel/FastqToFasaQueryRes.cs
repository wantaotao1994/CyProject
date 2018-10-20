using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyProject.Model.JsonModel
{
    public class FastqToFasaQueryRes : BaseJsonRes
    {

        [JsonProperty("guid")]
        public string Guid { get; set; }

        
    }
}
