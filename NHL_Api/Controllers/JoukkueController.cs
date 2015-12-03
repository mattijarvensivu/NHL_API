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
using System.Web.WebPages;
using NHL_Api.Models;

namespace NHL_Api.Controllers
{
    public class JoukkueController : ApiController
    {
        private H4102_3Entities db = new H4102_3Entities();

        // GET: api/Joukkue
        public IQueryable<JoukkueDto> GetJoukkues()
        {
            var joukkue = from b in db.Joukkues
                          select new JoukkueDto
                          {
                              Joukkueid = b.idJoukkue,
                              Lyhenne = b.Lyhenne,
                              Nimi = b.Nimi,
                              Voitot = b.Voitot,
                              Häviöt = b.Häviöt,
                              Jatkoaikahaviot = b.Jatkoaikahäviöt,
                              Pisteet = b.Voitot * 2 + b.Jatkoaikahäviöt
                          };
            return joukkue;
        }

        // GET: api/Joukkue/5
        [ResponseType(typeof(JoukkueDto))]
        public async Task<IHttpActionResult> GetJoukkue(int id)
        {
            var joukkue = await db.Joukkues.Select(b =>
            
           new JoukkueDto
           {
               Joukkueid = b.idJoukkue,
               Lyhenne = b.Lyhenne,
               Nimi = b.Nimi,
               Voitot = b.Voitot,
               Häviöt = b.Häviöt,
               Jatkoaikahaviot = b.Jatkoaikahäviöt,
             Pisteet = b.Voitot * 2 + b.Jatkoaikahäviöt
           }
           ).SingleOrDefaultAsync(b => b.Joukkueid == id);

            if (joukkue == null)
            {
                return NotFound();
            }

            return Ok(joukkue);
        }

        
        [ResponseType(typeof(Pelaaja))]
        [Route("api/Joukkue/{id}/Pelaajat")]
        [HttpGet]
        public async Task<IHttpActionResult> GetJoukkuePelaajat(int id)
        {
            Joukkue joukkue = await db.Joukkues.FindAsync(id);
           var pelaajat = from b in db.Pelaajas where (b.idJoukkue==joukkue.idJoukkue)
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



                if (joukkue.idJoukkue==0)
            {
                return NotFound();
            }
            return Ok(pelaajat);
        }

    

        // PUT: api/Joukkue/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJoukkue(int id, Joukkue joukkue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != joukkue.idJoukkue)
            {
                return BadRequest();
            }

            db.Entry(joukkue).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JoukkueExists(id))
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

        // POST: api/Joukkue
        [ResponseType(typeof(Joukkue))]
        public async Task<IHttpActionResult> PostJoukkue(Joukkue joukkue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Joukkues.Add(joukkue);
            await db.SaveChangesAsync();




            return CreatedAtRoute("DefaultApi", new { id = joukkue.idJoukkue }, joukkue);
        }

        // DELETE: api/Joukkue/5
        [ResponseType(typeof(Joukkue))]
        public async Task<IHttpActionResult> DeleteJoukkue(int id)
        {
            Joukkue joukkue = await db.Joukkues.FindAsync(id);
            if (joukkue == null)
            {
                return NotFound();
            }

            db.Joukkues.Remove(joukkue);
            await db.SaveChangesAsync();

            return Ok(joukkue);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JoukkueExists(int id)
        {
            return db.Joukkues.Count(e => e.idJoukkue == id) > 0;
        }
    }
}

