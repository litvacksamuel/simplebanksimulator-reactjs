using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CustomerId
    {
        public int Id { get; set; }
    }

    public class AccountsController : ApiController
    {
        private ApplicationEntities db = new ApplicationEntities();

        // GET: api/Accounts
        public IQueryable<Account> GetAccount()
        {
            return db.Account;
        }

        // GET: api/Accounts/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult GetAccount(int id)
        {
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccount(int id, Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.id)
            {
                return BadRequest();
            }

            db.Entry(account).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        [ResponseType(typeof(Account))]
        public IHttpActionResult PostAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Account.Add(account);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = account.id }, account);
        }

        // DELETE: api/Accounts/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult DeleteAccount(int id)
        {
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return NotFound();
            }

            db.Account.Remove(account);
            db.SaveChanges();

            return Ok(account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountExists(int id)
        {
            return db.Account.Count(e => e.id == id) > 0;
        }

        [HttpPost]
        [Route("api/Accounts")]
        public IHttpActionResult PostUserAccounts(CustomerId customerId)
        {
            var accounts = db.Account.Where(account => account.id_customer == customerId.Id).Select(a => new { a.account_number, a.status, a.amount, a.creation_date }).ToList();
            return Ok(accounts);
        }
    }
}