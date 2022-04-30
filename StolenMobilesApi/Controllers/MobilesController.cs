using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StolenMobilesApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace StolenMobilesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobilesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Mobiles>> GetUsers()
        {
            using (StolenContext db = new StolenContext())
            {
                return db.Mobiles.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Mobiles> GetUser(int id)
        {
            using (StolenContext db = new StolenContext())
            {
                var Mobile = db.Mobiles.ToList().Find(Mobile => Mobile.Id == id);
                if (Mobile is null) return NotFound();

                return Mobile;

            }
        }

        [HttpGet("SeeDevicesStatus")]
        public ActionResult<List<Mobiles>> SeeDevicesStatus(int UserId)
        {
            using (StolenContext db = new StolenContext())
            {
                //var Mobs = db.Mobiles.ToList().Find(Mobile => Mobile.UserId == UserId);
                List<Mobiles> Mobs = db.Mobiles.ToList();//(Mobile => Mobile.UserId == UserId);
                List<Mobiles> result = Mobs.FindAll(Mobiles => Mobiles.UserId == UserId);
                if (result is null) return NotFound();
                return result;

            }
        }

        [HttpPost("ADDMobile")]
        public ActionResult ADDMobile([FromBody] Mobiles Mobile)
        {
            if (Mobile is null)
                return BadRequest();
            using (StolenContext db = new StolenContext())
            {
                db.Mobiles.Add(Mobile);
                db.SaveChanges();
                return CreatedAtAction(nameof(ADDMobile), db);
            }
        }

        [HttpDelete]
        public ActionResult ClearMobiles()
        {
            using (StolenContext db = new StolenContext())
            {
                db.Mobiles.RemoveRange(db.Mobiles.ToList());
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveMobile(int id)
        {
            using (StolenContext db = new StolenContext())
            {
                var Mobile = db.Mobiles.ToList().Find(mobile => mobile.Id == id);
                if (Mobile is null) return NotFound();
                db.Mobiles.Remove(Mobile);
                db.SaveChanges();
                return NoContent();
            }
        }

        [HttpPut]
        public ActionResult Update(Mobiles Updated_Mobile)
        {
            using (StolenContext db = new StolenContext())
            {
                var old_Mobile = db.Mobiles.ToList().Find(User => User.Id == Updated_Mobile.Id);
                if (old_Mobile is null) return BadRequest();
                db.Entry(old_Mobile).CurrentValues.SetValues(Updated_Mobile);
                db.SaveChanges();
                return NoContent();
            }
        }

    }
}
