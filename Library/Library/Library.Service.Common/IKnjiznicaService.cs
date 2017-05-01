using Library.DAL.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Common
{
    public interface IKnjiznicaService
    {
        Task<int> Add(Knjiznica entity);
        Task<Knjiznica> Get(Guid id);
        Task<int> Update(Knjiznica entity);
        Task<int> Delete(Guid id);
        Task<IEnumerable<Knjiznica>> GetAll();
    }
}
