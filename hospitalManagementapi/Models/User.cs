using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospitalManagementapi.Models
{
    public class User
    {

        public int Id {get;set;}
        public byte[] PasswordHash { get; set; }
        public byte[] passwordsalt { get; set; }
        public string username { get; set; }

        public string Role { get; set; }



    }
}
