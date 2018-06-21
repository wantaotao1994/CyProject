using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyProject.Model.JsonModel
{
    public class BaseJsonRes
    {

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
