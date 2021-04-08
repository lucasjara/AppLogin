using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogin.Apps.Helpers
{
    class JsonResponse
    {
        [JsonProperty("msg")]
        public Msg msg { get; set; }
        public class Msg
        {
            public string code { get; set; }
            public string msg { get; set; }
        }
    }
}
