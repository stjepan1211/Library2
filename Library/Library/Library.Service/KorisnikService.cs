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
    public class KorisnikService : IKorisnikService
    {
        private IGeneric _generic;

        public KorisnikService(IGeneric generic)
        {
            _generic = generic;
        }
        //Add
        public async Task<int> Add(Korisnik entity)
        {
            try
            {
                return await _generic.Add<Korisnik>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get
        public async Task<Korisnik> Get(Guid id)
        {
            try
            {
                return await _generic.Get<Korisnik>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Korisnik> GetByUsername(string username)
        {
            try
            {
                return await _generic.GetQueryable<Korisnik>().Where(k => k.Username == username).FirstAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Update
        public async Task<int> Update(Korisnik entity)
        {
            try
            {
                return await _generic.Update<Korisnik>(entity);
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
                return await _generic.Delete<Korisnik>(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //GetAll
        public async Task<IEnumerable<Korisnik>> GetAll()
        {
            try
            {
                var response = await _generic.GetAll<Korisnik>();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
