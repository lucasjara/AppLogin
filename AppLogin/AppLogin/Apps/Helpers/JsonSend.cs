using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogin.Apps.Helpers
{
    public class JsonSend
    {
        [JsonProperty("header")]
        public Header header { get; set; }
        [JsonProperty("data")]
        public Data data { get; set; }


        public class Header
        {
            public string invokeMethod { get; set; }
            public string channel { get; set; }
            public string operationSystem { get; set; }
            public string deviceModel { get; set; }
            public string applicationVersion { get; set; }
            public int osType { get; set; }
        }

        public class Userlogin
        {
            public string email { get; set; }
            public string password { get; set; }
        }
        public class User
        {
            public string name { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string numberDoc { get; set; }
            public string password { get; set; }
        }

        public class Data
        {
            [JsonProperty("userlogin")]
            public Userlogin userlogin { get; set; }
            [JsonProperty("user")]
            public User user { get; set; }
        }

        public class Root
        {
            public Header header { get; set; }
            public Data data { get; set; }
        }
    
    }
}
