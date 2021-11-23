using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CardsController : ApiController
    {
        private ApplicationEntities db = new ApplicationEntities();

        // GET: api/Cards
        public IQueryable<Cards> GetCards()
        {
            return db.Cards;
        }

        // GET: api/Cards/5
        [ResponseType(typeof(Cards))]
        public IHttpActionResult GetCards(int id)
        {
            Cards cards = db.Cards.Find(id);
            if (cards == null)
            {
                return NotFound();
            }

            return Ok(cards);
        }

        // PUT: api/Cards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCards(int id, Cards cards)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cards.id)
            {
                return BadRequest();
            }

            db.Entry(cards).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardsExists(id))
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

        // POST: api/Cards
        [ResponseType(typeof(Cards))]
        public IHttpActionResult PostCards(Cards cards)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cards.Add(cards);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cards.id }, cards);
        }

        // DELETE: api/Cards/5
        [ResponseType(typeof(Cards))]
        public IHttpActionResult DeleteCards(int id)
        {
            Cards cards = db.Cards.Find(id);
            if (cards == null)
            {
                return NotFound();
            }

            db.Cards.Remove(cards);
            db.SaveChanges();

            return Ok(cards);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardsExists(int id)
        {
            return db.Cards.Count(e => e.id == id) > 0;
        }
    }
}