using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Common.IDatabaseModels
{
    public interface IKnjiga
    {
        Guid ID { get; set; }
        Guid OdjelID { get; set; }
        string Naslov { get; set; }
        string Autor { get; set; }
        int UkupanBroj { get; set; }
    }
}
