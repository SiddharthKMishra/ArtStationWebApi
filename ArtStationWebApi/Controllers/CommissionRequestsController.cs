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
using ArtStationWebApi.Models;

namespace ArtStationWebApi.Controllers
{
    public class CommissionRequestsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/CommissionRequests
        public IQueryable<CommissionRequest> GetCommissionRequests()    //fetch all commission request data 
        {
            return db.CommissionRequests;
        }

        // GET: api/CommissionRequests/5
        [ResponseType(typeof(CommissionRequest))]
        public IHttpActionResult GetCommissionRequest(int id)       //fetch commission request data of specific id
        {
            CommissionRequest commissionRequest = db.CommissionRequests.Find(id);
            if (commissionRequest == null)
            {
                return NotFound();
            }

            return Ok(commissionRequest);
        }

        // PUT: api/CommissionRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCommissionRequest(int id, CommissionRequest commissionRequest)      //modify and update comm request data of specific id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commissionRequest.CommID)
            {
                return BadRequest();
            }

            db.Entry(commissionRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommissionRequestExists(id))
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

        // POST: api/CommissionRequests
        [ResponseType(typeof(CommissionRequest))]
        public IHttpActionResult PostCommissionRequest(CommissionRequest commissionRequest)     //add new comm request data
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CommissionRequests.Add(commissionRequest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = commissionRequest.CommID }, commissionRequest);
        }

        // DELETE: api/CommissionRequests/5
        [ResponseType(typeof(CommissionRequest))]
        public IHttpActionResult DeleteCommissionRequest(int id)            //delete comm request data of specific id
        {
            CommissionRequest commissionRequest = db.CommissionRequests.Find(id);
            if (commissionRequest == null)
            {
                return NotFound();
            }

            db.CommissionRequests.Remove(commissionRequest);
            db.SaveChanges();

            return Ok(commissionRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommissionRequestExists(int id)
        {
            return db.CommissionRequests.Count(e => e.CommID == id) > 0;
        }
    }
}