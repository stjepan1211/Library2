using Library.DAL.DatabaseModels;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class PosudenaKnjigaService : IPosudenaKnjigaService
    {
        private IGeneric _generic;

        public PosudenaKnjigaService(IGeneric generic)
        {
            _generic = generic;
        }
        //Add
        public async Task<int> Add(PosudenaKnjiga entity)
        {
            try
            {
                return await _generic.Add<PosudenaKnjiga>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get
        public async Task<PosudenaKnjiga> Get(Guid id)
        {
            try
            {
                return await _generic.Get<PosudenaKnjiga>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Update
        public async Task<int> Update(PosudenaKnjiga entity)
        {
            try
            {
                return await _generic.Update<PosudenaKnjiga>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Delete
        public async Task<int> Delete(Guid id)
        {
            try
            {
                return await _generic.Delete<PosudenaKnjiga>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //GetAll
        public async Task<IEnumerable<PosudenaKnjiga>> GetAll()
        {
            try
            {
                var response = await _generic.GetAll<PosudenaKnjiga>();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
