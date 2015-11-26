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
    public class JoukkueController : ApiController
    {
        private H4102_3Entities db = new H4102_3Entities();

        // GET: api/Joukkue
        public IQueryable<JoukkueDTO> GetJoukkues()
        {
            var joukkue = from b in db.Joukkue
                          select new JoukkueDTO()
                          {
                              JoukkueID = b.idJoukkue,
                              nimi = b.Nimi,
                              voitot = b.Voitot,
                              häviöt = b.Häviöt,
                              jatkoaikahaviot = b.Jatkoaikahäviöt

                          };
            return joukkue;
        }

        // GET: api/Joukkue/5
        [ResponseType(typeof(JoukkueDTO))]
        public async Task<IHttpActionResult> GetJoukkue(int id)
        {
            //Joukkue joukkue = await db.Joukkue.FindAsync(id);
            var joukkue = await db.Joukkue.Select(b =>

           new JoukkueDTO()
           {
               JoukkueID = b.idJoukkue,
               nimi = b.Nimi,
               voitot =b.Voitot,
               häviöt = b.Häviöt,
               jatkoaikahaviot = b.Jatkoaikahäviöt,
               pisteet = b.Voitot * 2 + b.Jatkoaikahäviöt
           }
           ).SingleOrDefaultAsync(b => b.JoukkueID == id);

            if (joukkue == null)
            {
                return NotFound();
            }

            return Ok(joukkue);
        }
        //hio vielä. en varma toimiiko
        [ResponseType(typeof(Joukkue))]
        [Route("api/Joukkue/{id}/Pelaajat")]
        [HttpGet]
        public async Task<IHttpActionResult> GetJoukkuePelaajat(int id)
        {
            var joukkue = from b in db.Joukkue where (b.idJoukkue == id) select b.Pelaajas;
                          

            if (joukkue == null)
            {
                return NotFound();
            }
            return Ok(joukkue);
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

            db.Joukkue.Add(joukkue);
            await db.SaveChangesAsync();




            return CreatedAtRoute("DefaultApi", new { id = joukkue.idJoukkue }, joukkue);
        }

        // DELETE: api/Joukkue/5
        [ResponseType(typeof(Joukkue))]
        public async Task<IHttpActionResult> DeleteJoukkue(int id)
        {
            Joukkue joukkue = await db.Joukkue.FindAsync(id);
            if (joukkue == null)
            {
                return NotFound();
            }

            db.Joukkue.Remove(joukkue);
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
            return db.Joukkue.Count(e => e.idJoukkue == id) > 0;
        }
    }
}

/*  [ResponseType(typeof(Joukkue))]
  [Route("api/kakkaa")]
  [HttpGet]
  public async Task<IHttpActionResult> Getyksijoukkuenimi(int id)
  {
      //User user = await db.Users.FindAsync(id);            
      var Nimi = from c in db.Joukkue where (c.idJoukkue == id) select c.Nimi;


      if (Nimi == null)
      {
          return NotFound();
      } 
      return Ok(Nimi);
  }

*/
