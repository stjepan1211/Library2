using Library.DAL.Common.IDatabaseModels;
using Library.DAL.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Common
{
    public interface IKnjigaService
    {
        Task<int> Add(Knjiga entity);
        Task<Knjiga> Get(Guid id);
        Task<int> Update(Knjiga entity);
        Task<int> Delete(Guid id);
        Task<IEnumerable<Knjiga>> GetAll();
        Task<IEnumerable<IKnjiga>> GetByOdjel(Guid odjelId);
    }
}
