using Library.DAL.Common.IDatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.DatabaseModels
{
    public class Knjiga : IKnjiga
    {
        public Knjiga()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public Guid OdjelID { get; set; }
        public string Naslov { get; set; }
        public string Autor { get; set; }
        public int UkupanBroj { get; set; }
    }
}
