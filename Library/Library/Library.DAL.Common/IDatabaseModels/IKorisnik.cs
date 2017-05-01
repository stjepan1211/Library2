using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Common.IDatabaseModels
{
    public interface IKorisnik
    {
        Guid ID { get; set; }
        string Ime { get; set; }
        string Prezime { get; set; }
        int Zakasnina { get; set; }
        string Uloga { get; set; }
    }
}
