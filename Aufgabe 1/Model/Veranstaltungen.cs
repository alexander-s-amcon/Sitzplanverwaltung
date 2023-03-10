using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_1.Model
{
    public class Veranstaltungen
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Saal { get; set; }
        public DateTime von { get; set; }
        public DateTime bis { get; set; }

    }
}
