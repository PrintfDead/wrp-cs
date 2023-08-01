using System;
using System.Collections.Generic;
using System.Text;

namespace WashingtonRP.Structures.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
    }
}
