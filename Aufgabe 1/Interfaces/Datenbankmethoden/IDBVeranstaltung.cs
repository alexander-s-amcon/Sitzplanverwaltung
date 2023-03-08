using Aufgabe_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_1.Interfaces.Datenbankmethoden
{
    public interface IDBVeranstaltung
    {
        List<Veranstaltungen> LadeVeranstaltung();
        void AddVeranstaltung(Veranstaltungen veranstaltung);
        void EditVeranstaltung(Veranstaltungen veranstaltung);
        void DeleteVeranstaltung(Veranstaltungen veranstaltung);
        void UpdateVeranstaltung(Veranstaltungen veranstaltung);
        void CreateVeranstaltungen();
    }
}
