using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XF_FetchJson.Models
{
    class User {
        private int id { get; set; }

        private string name { get; set; }

        private string username { get; set; }
        public List<string> ulist { get; set; }

        public User() {
        }



    }
}
