using Library.DAL.Common.IDatabaseModels;
using Library.DAL.DatabaseModels;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class KnjigaService : IKnjigaService
    {
        private IGeneric _generic;

        public KnjigaService(IGeneric generic)
        {
            _generic = generic;
        }
        //Add
        public async Task<int> Add(Knjiga entity)
        {
            try
            {
                return await _generic.Add<Knjiga>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get
        public async Task<Knjiga> Get(Guid id)
        {
            try
            {
                return await _generic.Get<Knjiga>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //GetAll by odjel
        public async Task<IEnumerable<IKnjiga>> GetByOdjel(Guid odjelId)
        {
            try
            {
                var response = await _generic.GetQueryable<Knjiga>().Where(k => k.OdjelID == odjelId).ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Update
        public async Task<int> Update(Knjiga entity)
        {
            try
            {
                return await _generic.Update<Knjiga>(entity);
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
                return await _generic.Delete<Knjiga>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //GetAll
        public async Task<IEnumerable<Knjiga>> GetAll()
        {
            try
            {
                var response = await _generic.GetAll<Knjiga>();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
