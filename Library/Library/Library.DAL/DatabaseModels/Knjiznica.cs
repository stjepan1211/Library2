using Library.DAL.Common.IDatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.DatabaseModels
{
    public class Knjiznica : IKnjiznica
    {
        public Knjiznica()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string BrojOdjela { get; set; }
        public string BrojUclanjenih { get; set; }
        public virtual ICollection<IOdjel> Odjel { get; set; }
    }
}
