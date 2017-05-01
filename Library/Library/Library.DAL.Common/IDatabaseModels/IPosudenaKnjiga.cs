using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Common.IDatabaseModels
{
    public interface IPosudenaKnjiga
    {
        Guid ID { get; set; }
        Guid KnjigaID { get; set; }
        Guid KorisnikID { get; set; }
        Nullable<bool> Vracena { get; set; }
        DateTime PosudenaDatum { get; set; }
        DateTime IstekRokaDatum { get; set; }
    }
}
