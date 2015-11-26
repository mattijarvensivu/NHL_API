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
        public IQueryable<Pelaaja> GetPelaajas()
        {
           
            return db.Pelaaja;
            
        }

        // GET: api/Pelaaja/5
        [ResponseType(typeof(Pelaaja))]
        public async Task<IHttpActionResult> GetPelaaja(int id)
        {
            Pelaaja pelaaja = await db.Pelaaja.FindAsync(id);

            if (pelaaja == null)
            {
                return NotFound();
            }

            return Ok(pelaaja);
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

            db.Pelaaja.Add(pelaaja);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pelaaja.idPelaaja }, pelaaja);
        }

        // DELETE: api/Pelaaja/5
        [ResponseType(typeof(Pelaaja))]
        public async Task<IHttpActionResult> DeletePelaaja(int id)
        {
            Pelaaja pelaaja = await db.Pelaaja.FindAsync(id);
            if (pelaaja == null)
            {
                return NotFound();
            }

            db.Pelaaja.Remove(pelaaja);
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
            return db.Pelaaja.Count(e => e.idPelaaja == id) > 0;
        }
    }
}