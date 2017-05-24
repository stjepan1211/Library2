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
    [RoutePrefix("api/PosudenaKnjiga")]

    public class PosudenaKnjigaController : ApiController
    {
        private IPosudenaKnjigaService _posudenaKnjigaService;

        public PosudenaKnjigaController(IPosudenaKnjigaService posudenaKnjigaService)
        {
            this._posudenaKnjigaService = posudenaKnjigaService;
        }

        //GetAll
        [HttpGet]
        [Route("GetAll")]
        public async Task<HttpResponseMessage> GetAllPosudenaKnjiga()
        {
            try
            {
                var posudeneKnjige = await _posudenaKnjigaService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, posudeneKnjige);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //Get
        [HttpGet]
        [Route("Get")]
        public async Task<HttpResponseMessage> GetPosudenaKnjiga(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var posudenaKnjiga = await _posudenaKnjigaService.Get(id);

                return Request.CreateResponse(HttpStatusCode.OK, posudenaKnjiga);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<HttpResponseMessage> AddPosudenaKnjiga(PosudenaKnjiga posudenaKnjiga)
        {
            try
            {
                if (posudenaKnjiga.IstekRokaDatum == null || posudenaKnjiga.KnjigaID == null || posudenaKnjiga.KorisnikID == null
                    || posudenaKnjiga.PosudenaDatum == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                posudenaKnjiga.ID = Guid.NewGuid();

                var response = await _posudenaKnjigaService.Add(posudenaKnjiga);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> UpdatePosudenaKnjiga(PosudenaKnjiga posudenaKnjiga)
        {
            try
            {

                if (posudenaKnjiga.ID == null || posudenaKnjiga.PosudenaDatum == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                PosudenaKnjiga toBeUpdated = await _posudenaKnjigaService.Get(posudenaKnjiga.ID);

                toBeUpdated.PosudenaDatum = posudenaKnjiga.PosudenaDatum;

                var response = await _posudenaKnjigaService.Update(toBeUpdated);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<HttpResponseMessage> DeletePosudenaKnjiga(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var response = await _posudenaKnjigaService.Delete(id);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        //GetByUserId
        [HttpGet]
        [Route("GetByUserId")]
        public async Task<HttpResponseMessage> GetByUserId(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var posudenaKnjiga = await _posudenaKnjigaService.GetByUserId(id);

                return Request.CreateResponse(HttpStatusCode.OK, posudenaKnjiga);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        [HttpPut]
        [Route("UpdateToReturned")]
        public async Task<HttpResponseMessage> UpdateToReturned(Guid id)
        {
            try
            {

                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                PosudenaKnjiga toBeUpdated = await _posudenaKnjigaService.Get(id);

                toBeUpdated.Vracena = true;

                var response = await _posudenaKnjigaService.Update(toBeUpdated);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



    }
}
