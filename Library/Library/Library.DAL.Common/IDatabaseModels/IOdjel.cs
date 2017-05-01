using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Common.IDatabaseModels
{
    public interface IOdjel
    {
        Guid ID { get; set; }
        Guid KnjiznicaID { get; set; }
        string Naziv { get; set; }
        ICollection<IKnjiga> Knjige { get; set; }
    }
}
