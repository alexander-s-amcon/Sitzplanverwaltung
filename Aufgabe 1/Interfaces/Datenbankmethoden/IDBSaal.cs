using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_1.Interfaces.Datenbankmethoden
{
    public interface IDBSaal
    {
        List<Saele> LadeSaal();
        void AddSaal(Saele saal);
        void EditSaal(Saele saal);
        void DeleteSaal(Saele saal);
        void CreateSaal();
    }


}
