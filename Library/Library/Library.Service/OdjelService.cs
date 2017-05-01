using Library.DAL.DatabaseModels;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class OdjelService : IOdjelService
    {
        private IGeneric _generic;

        public OdjelService(IGeneric generic)
        {
            _generic = generic;
        }
        //Add
        public async Task<int> Add(Odjel entity)
        {
            try
            {
                return await _generic.Add<Odjel>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get
        public async Task<Odjel> Get(Guid id)
        {
            try
            {
                var response = await _generic.Get<Odjel>(id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Update
        public async Task<int> Update(Odjel entity)
        {
            try
            {
                return await _generic.Update<Odjel>(entity);
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
                return await _generic.Delete<Odjel>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //GetAll
        public async Task<IEnumerable<Odjel>> GetAll()
        {
            try
            {
                var response = await _generic.GetAll<Odjel>();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
