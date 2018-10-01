using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLExample
{
    class Customers
    {
        public Customers(string f, string s)
        {
            this.Forename = f;
            this.Surname = s;
        }

        public string Forename { get; set; }
        public string Surname { get; set; }
    }
}
