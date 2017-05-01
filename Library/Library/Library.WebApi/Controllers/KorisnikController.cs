using Library.DAL.DatabaseModels;
using Library.Service.Common;
using Library.Token;
using Library.WebApi.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.WebApi.Controllers
{
    [RoutePrefix("api/Korisnik")]
    public class KorisnikController : ApiController
    {
        private IKorisnikService _korisnikService;

        public KorisnikController(IKorisnikService korisnikService)
        {
            this._korisnikService = korisnikService;
        }

        //GetAll
        [HttpGet]
        [Route("GetAll")]
        public async Task<HttpResponseMessage> GetAllKorisnik()
        {
            try
            {

                var korisnici = await _korisnikService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, korisnici);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //GetAll
        [HttpGet]
        [Route("Get")]
        public async Task<HttpResponseMessage> GetKorisnik(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var korisnik = await _korisnikService.Get(id);

                return Request.CreateResponse(HttpStatusCode.OK, korisnik);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<HttpResponseMessage> AddKorisnik(Korisnik korisnik)
        {
            try
            {
                if (korisnik.Email == null || korisnik.Username == null || korisnik.Password == null
                    || korisnik.Ime == null || korisnik.Prezime == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan korisnik.");
                var korisnici = await _korisnikService.GetAll();

                foreach(var item in korisnici)
                {
                    if(item.Email == korisnik.Email)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email zauzet.");
                    if(item.Username == korisnik.Username)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username zauzet.");
                }



                if (korisnik.Email == "sbaricevic@etfos.hr" || korisnik.Email == "jbaketaric@etfos.hr")
                {
                    korisnik.Uloga = "Admin";
                }
                else
                {
                    korisnik.Uloga = "Korisnik";
                }

                korisnik.ID = Guid.NewGuid();
                korisnik.Zakasnina = 0;

                var response = await _korisnikService.Add(korisnik);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> UpdateKorisnik(Korisnik korisnik)
        {
            try
            {

                if (korisnik.ID == null || korisnik.Zakasnina == 0 || korisnik.Uloga == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan unos.");

                Korisnik toBeUpdated = await _korisnikService.Get(korisnik.ID);

                toBeUpdated.Zakasnina = korisnik.Zakasnina;
                toBeUpdated.Uloga = korisnik.Uloga;

                var response = await _korisnikService.Update(toBeUpdated);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<HttpResponseMessage> DeleteKorisnik(Guid id)
        {
            try
            {
                if (id == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Neispravan id.");

                var response = await _korisnikService.Delete(id);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<HttpResponseMessage> LoginToken(UserCredentials userCredentials)
        {
            try
            {
                var userToLogin = await _korisnikService.GetByUsername(userCredentials.Username);
                if (userToLogin == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User is not registered.");
                }
                else if (userCredentials.Password != userToLogin.Password)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Password is incorrect.");
                }
                else
                {
                    var tokenDuration = DateTime.UtcNow.AddMinutes(30);
                    var token = new TokenFactory(tokenDuration).GenerateToken();
                    var tokenResponse = new TokenResponse()
                    {
                        UserName = userCredentials.Username,
                        Token = token,
                        Role = userToLogin.Uloga
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, tokenResponse);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
