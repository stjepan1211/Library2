using Library.DAL.DatabaseModels;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class KnjiznicaService : IKnjiznicaService
    {

        private IGeneric _generic;

        public KnjiznicaService(IGeneric generic)
        {
            _generic = generic;
        }
        //Add
        public async Task<int> Add(Knjiznica entity)
        {
            try
            {
                return await _generic.Add<Knjiznica>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get
        public async Task<Knjiznica> Get(Guid id)
        {
            try
            {
                return await _generic.Get<Knjiznica>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Update
        public async Task<int> Update(Knjiznica entity)
        {
            try
            {
                return await _generic.Update<Knjiznica>(entity);
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
                return await _generic.Delete<Knjiznica>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //GetAll
        public async Task<IEnumerable<Knjiznica>> GetAll()
        {
            try
            {
                var response = await _generic.GetAll<Knjiznica>();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
