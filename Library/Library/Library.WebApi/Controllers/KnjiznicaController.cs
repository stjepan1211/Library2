using Library.DAL.DatabaseModels;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.WebApi.Controllers
{
    [RoutePrefix("api/Knjiznica")]
    public class KnjiznicaController : ApiController
    {
        private IKnjiznicaService _knjiznicaService;
        private IOdjelService _odjelService;

        public KnjiznicaController(IKnjiznicaService knjiznicaService, IOdjelService odjelService)
        {
            this._knjiznicaService = knjiznicaService;
            this._odjelService = odjelService;
        }

        //GetAll
        [HttpGet]
        [Route("GetAll")]
        public async Task<HttpResponseMessage> GetAllKnjiznica()
        {
            try
            {

                var knjiznice = await _knjiznicaService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, knjiznice);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //GetAll
        [HttpGet]
        [Route("Get")]
        public async Task<HttpResponseMessage> GetKnjiznica(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var knjiznica = await _knjiznicaService.Get(id);

                return Request.CreateResponse(HttpStatusCode.OK, knjiznica);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<HttpResponseMessage> AddKnjiznica(Knjiznica knjiznica)
        {
            try
            {
                if (knjiznica.Naziv == null || knjiznica.Adresa == null || knjiznica.BrojOdjela == null
                    || knjiznica.BrojUclanjenih == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                knjiznica.ID = Guid.NewGuid();

                var response = await _knjiznicaService.Add(knjiznica);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> UpdateKorisnik(Knjiznica knjiznica)
        {
            try
            {

                if (knjiznica.ID == null || knjiznica.BrojUclanjenih == null || knjiznica.Naziv == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                Knjiznica toBeUpdated = await _knjiznicaService.Get(knjiznica.ID);

                toBeUpdated.BrojUclanjenih = knjiznica.BrojUclanjenih;
                toBeUpdated.Naziv = knjiznica.Naziv;

                var response = await _knjiznicaService.Update(toBeUpdated);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<HttpResponseMessage> DeleteKnjiznica(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var Odjeli = await _odjelService.GetAll();

                foreach(var item in Odjeli)
                {
                    if(item.KnjiznicaID == id)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Za ovu knjiznicu dodani su odjeli. Potrebno ih "
                            +"je obrisati prije brisanja knjižnice.");
                    }
                }

                var response = await _knjiznicaService.Delete(id);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
