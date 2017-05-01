using Library.DAL.Common.IDatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Common.IDatabaseModels
{
    public interface IKnjiznica
    {
        Guid ID { get; set; }
        string Naziv { get; set; }
        string Adresa { get; set; }
        string BrojOdjela { get; set; }
        string BrojUclanjenih { get; set; }
        ICollection<IOdjel> Odjel { get; set; }
    }
}
