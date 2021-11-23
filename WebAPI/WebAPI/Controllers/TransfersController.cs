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
    public class TransfersController : ApiController
    {
        private ApplicationEntities db = new ApplicationEntities();

        // GET: api/Transfers
        public IQueryable<Transfers> GetTransfers()
        {
            return db.Transfers;
        }

        // GET: api/Transfers/5
        [ResponseType(typeof(Transfers))]
        public IHttpActionResult GetTransfers(int id)
        {
            Transfers transfers = db.Transfers.Find(id);
            if (transfers == null)
            {
                return NotFound();
            }

            return Ok(transfers);
        }

        // PUT: api/Transfers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransfers(int id, Transfers transfers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transfers.id)
            {
                return BadRequest();
            }

            db.Entry(transfers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransfersExists(id))
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

        // POST: api/Transfers
        [ResponseType(typeof(Transfers))]
        public IHttpActionResult PostTransfers(Transfers transfers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transfers.Add(transfers);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = transfers.id }, transfers);
        }

        // DELETE: api/Transfers/5
        [ResponseType(typeof(Transfers))]
        public IHttpActionResult DeleteTransfers(int id)
        {
            Transfers transfers = db.Transfers.Find(id);
            if (transfers == null)
            {
                return NotFound();
            }

            db.Transfers.Remove(transfers);
            db.SaveChanges();

            return Ok(transfers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransfersExists(int id)
        {
            return db.Transfers.Count(e => e.id == id) > 0;
        }
    }
}