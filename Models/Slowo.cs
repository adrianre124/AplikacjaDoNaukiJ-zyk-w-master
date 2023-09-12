using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaDoNaukiJęzyków.Models
{
    public class Slowo
    {
        public int ID { get; set; }
        public string PoziomSlowa { get; set; }
        public string SlowoPol { get; set; }
        public string SlowoAng { get; set; }
        public string SlowoAra { get; set; }
        public string SlowoUkr { get; set; }
    }
}
