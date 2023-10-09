using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscpetcherZadach_Client
{
    public class MyProcess
    {
        public int IdProc { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return $"{IdProc} {name}";
        }
    }
}
