using Library.DAL.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Common
{
    public interface IKorisnikService
    {
        Task<int> Add(Korisnik entity);
        Task<Korisnik> Get(Guid id);
        Task<int> Update(Korisnik entity);
        Task<int> Delete(Guid id);
        Task<IEnumerable<Korisnik>> GetAll();
        Task<Korisnik> GetByUsername(string username);
    }
}
