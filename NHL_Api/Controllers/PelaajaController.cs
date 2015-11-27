using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NHL_Api.Models;

namespace NHL_Api.Controllers
{
    public class PelaajaController : ApiController
    {
        private H4102_3Entities db = new H4102_3Entities();

        


        // GET: api/Pelaaja
        public IQueryable<PelaajaDto> GetPelaajat()
        {
            var pelaajat = from b in db.Pelaajas
                          select new PelaajaDto
                          {
                              Id = b.idPelaaja,
                              Nimi = b.Nimi,
                              Pelinumero = b.Pelinumero,
                              Maalit = b.Maalit,
                              Syötöt = b.Syötöt,
                              Plusmiinus = b.Plusminus,
                              Pisteet = b.Maalit + b.Syötöt
                          };
            return pelaajat;
        }



        // GET: api/Pelaaja/5
        [ResponseType(typeof(PelaajaDto))]
        public async Task<IHttpActionResult> GetPelaaja(int id)
        {
           
            var pelaajat = await db.Pelaajas.Select(b =>

           new PelaajaDto
           {
               Id = b.idPelaaja,
               Nimi = b.Nimi,
               Pelinumero = b.Pelinumero,
               Maalit = b.Maalit,
               Syötöt = b.Syötöt,
               Plusmiinus = b.Plusminus,
               Pisteet = b.Maalit + b.Syötöt
           }
           ).SingleOrDefaultAsync(b => b.Id == id);

            if (pelaajat == null)
            {
                return NotFound();
            }

            return Ok(pelaajat);
        }

        // PUT: api/Pelaaja/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPelaaja(int id, Pelaaja pelaaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pelaaja.idPelaaja)
            {
                return BadRequest();
            }

            db.Entry(pelaaja).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PelaajaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Pelaaja
        [ResponseType(typeof(Pelaaja))]
        public async Task<IHttpActionResult> PostPelaaja(Pelaaja pelaaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pelaajas.Add(pelaaja);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pelaaja.idPelaaja }, pelaaja);
        }

        // DELETE: api/Pelaaja/5
        [ResponseType(typeof(Pelaaja))]
        public async Task<IHttpActionResult> DeletePelaaja(int id)
        {
            Pelaaja pelaaja = await db.Pelaajas.FindAsync(id);
            if (pelaaja == null)
            {
                return NotFound();
            }

            db.Pelaajas.Remove(pelaaja);
            await db.SaveChangesAsync();

            return Ok(pelaaja);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PelaajaExists(int id)
        {
            return db.Pelaajas.Count(e => e.idPelaaja == id) > 0;
        }
    }
}