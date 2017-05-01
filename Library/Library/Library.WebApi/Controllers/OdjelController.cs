using Library.DAL.Common.IDatabaseModels;
using Library.DAL.DatabaseModels;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.WebApi.Controllers
{
    [RoutePrefix("api/Odjel")]
    public class OdjelController : ApiController
    {
        private IOdjelService _odjelService;
        private IKnjiznicaService _knjiznicaService;
        private IKnjigaService _knjigaService;

        public OdjelController(IOdjelService odjelService, IKnjiznicaService knjiznicaService, IKnjigaService knjigaService)
        {
            this._odjelService = odjelService;
            this._knjiznicaService = knjiznicaService;
            this._knjigaService = knjigaService;
        }

        //GetAll
        [HttpGet]
        [Route("GetAll")]
        public async Task<HttpResponseMessage> GetAllOdjel()
        {
            try
            {

                IEnumerable<Odjel> odjeli = await _odjelService.GetAll();

                foreach(var odjel in odjeli)
                {
                    
                    var knjige = await _knjigaService.GetByOdjel(odjel.ID);
                    odjel.Knjige = new Collection<IKnjiga>(knjige.ToList());
                }
                return Request.CreateResponse(HttpStatusCode.OK, odjeli );
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //GetAll
        [HttpGet]
        [Route("Get")]
        public async Task<HttpResponseMessage> GetOdjel(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var odjel = await _odjelService.Get(id);

                return Request.CreateResponse(HttpStatusCode.OK, odjel);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<HttpResponseMessage> AddKnjiga(Odjel odjel)
        {
            try
            {
                if (odjel.Naziv == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                IEnumerable<Knjiznica> knjiznica = await _knjiznicaService.GetAll();

                if (knjiznica.Count() == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Potrebno dodati knjiznicu zatim odjele.");

                odjel.KnjiznicaID = knjiznica.First().ID;
                odjel.ID = Guid.NewGuid();

                var response = await _odjelService.Add(odjel);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> UpdateOdjel(Odjel odjel)
        {
            try
            {

                if (odjel.ID == null || odjel.Naziv == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                Odjel toBeUpdated = await _odjelService.Get(odjel.ID);

                toBeUpdated.Naziv = odjel.Naziv;

                var response = await _odjelService.Update(toBeUpdated);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<HttpResponseMessage> DeleteOdjel(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var knjige = await _knjigaService.GetByOdjel(id);

                if(knjige.Count() > 0)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "U odjelu ima dodanih knjiga. Potrebno ih je "
                        + "obrisati prije brisanja odjela.");

                var response = await _odjelService.Delete(id);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
