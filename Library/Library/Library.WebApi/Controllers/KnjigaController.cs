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
    [RoutePrefix("api/Knjiga")]
    public class KnjigaController : ApiController
    {
        private IKnjigaService _knjigaService;

        public KnjigaController(IKnjigaService knjigaService)
        {
            this._knjigaService = knjigaService;
        }

        //GetAll
        [HttpGet]
        [Route("GetAll")]
        public async Task<HttpResponseMessage> GetAllKnjiga()
        {
            try
            {

                var knjige = await _knjigaService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, knjige);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //Get
        [HttpGet]
        [Route("Get")]
        public async Task<HttpResponseMessage> GetKnjiga(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var knjiga = await _knjigaService.Get(id);

                return Request.CreateResponse(HttpStatusCode.OK, knjiga);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //Get
        [HttpGet]
        [Route("GetByOdjel")]
        public async Task<HttpResponseMessage> GetByOdjel(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id odjela.");

                var knjige = await _knjigaService.GetByOdjel(id);

                return Request.CreateResponse(HttpStatusCode.OK, knjige);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [Route("Add")]
        public async Task<HttpResponseMessage> AddKnjiga(Knjiga knjiga)
        {
            try
            {
                if (knjiga.Naslov == null || knjiga.Autor == null || knjiga.UkupanBroj < 0 || knjiga.OdjelID == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos knjige.");

                knjiga.ID = Guid.NewGuid();
               

                var response = await _knjigaService.Add(knjiga);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> UpdateKnjiga(Knjiga knjiga)
        {
            try
            {

                if (knjiga.ID == null || knjiga.Autor == null || knjiga.OdjelID == null || knjiga.Naslov == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                Knjiga toBeUpdated = await _knjigaService.Get(knjiga.ID);

                toBeUpdated.Autor = knjiga.Autor;
                toBeUpdated.Naslov = knjiga.Naslov;
                toBeUpdated.UkupanBroj = knjiga.UkupanBroj;
                toBeUpdated.OdjelID = knjiga.OdjelID;

                var response = await _knjigaService.Update(toBeUpdated);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<HttpResponseMessage> DeleteKnjiga(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var response = await _knjigaService.Delete(id);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }



        [HttpPut]
        [Route("UpdatePlusOne")]
        public async Task<HttpResponseMessage> UpdatePlusOne(Guid id)
        {
            try
            {

                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                Knjiga toBeUpdated = await _knjigaService.Get(id);

                toBeUpdated.UkupanBroj++;

                var response = await _knjigaService.Update(toBeUpdated);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }





    }
}
