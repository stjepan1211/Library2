using Library.DAL.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Common
{
    public interface IPosudenaKnjigaService
    {
        Task<int> Add(PosudenaKnjiga entity);
        Task<PosudenaKnjiga> Get(Guid id);
        Task<int> Update(PosudenaKnjiga entity);
        Task<int> Delete(Guid id);
        Task<IEnumerable<PosudenaKnjiga>> GetAll();
    }
}
