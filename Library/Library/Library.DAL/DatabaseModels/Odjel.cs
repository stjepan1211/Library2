using Library.DAL.Common.IDatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.DatabaseModels
{
    public class Odjel : IOdjel
    {
        public Odjel()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public Guid KnjiznicaID { get; set; }
        public string Naziv { get; set; }
        public virtual ICollection<IKnjiga> Knjige { get; set; }
    }
}
