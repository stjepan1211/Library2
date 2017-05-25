using Library.DAL.Common.IDatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.DatabaseModels
{
    public class PosudenaKnjiga : IPosudenaKnjiga
    {
        public PosudenaKnjiga()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public Guid KnjigaID { get; set; }
        public string NazivKnjige { get; set; }
        public string Autor { get; set; }
        public Guid KorisnikID { get; set; }
        public Nullable<bool> Vracena { get; set; }
        public DateTime PosudenaDatum { get; set; }
        public DateTime IstekRokaDatum { get; set; }
    }
}
