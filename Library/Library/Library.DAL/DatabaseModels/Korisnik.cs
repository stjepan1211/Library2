using Library.DAL.Common.IDatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.DatabaseModels
{
    public class Korisnik : IKorisnik
    {
        public Korisnik()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Zakasnina { get; set; }
        public string Uloga { get; set; }
    }
}
