using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StolenMobilesApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StolenMobilesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobilesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Mobiles>> GetAllMoblies()
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    return db.Mobiles.ToList();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

        [HttpGet("{id}")]
        public ActionResult<Mobiles> GetMoble(int id)
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    var Mobile = db.Mobiles.Include(e => e.Image).ToList().Find(Mobile => Mobile.Id == id);
                    if (Mobile is null) return NotFound();
                    return Mobile;
                }
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SeeDevicesStatus")]
        public ActionResult<List<Mobiles>> SeeDevicesStatus(int UserId)
        {
            try
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
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetSpecificMobile")]
        public ActionResult<Mobiles> GetSpecificMobile(int UserId ,int MobileId)
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    var Mobile = db.Mobiles.ToList().Find(Mobile => Mobile.Id == MobileId & Mobile.UserId == UserId);
                    if (Mobile is null) return NotFound();
                    return Mobile;
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("ADDMobile")]
        public ActionResult ADDMobile([FromBody] Mobiles Mobile)
        {
            try
            {
                //note : when pass Mobile parametar pass it without id because id auto increment

                if (Mobile is null)
                    return BadRequest();
                using (StolenContext db = new StolenContext())
                {
                    if (db.Mobiles.ToList().FirstOrDefault(mob => mob.IMEI == Mobile.IMEI) is not null)
                        return BadRequest();
                    db.Mobiles.Add(Mobile);
                    db.SaveChanges();
                    //return CreatedAtAction(nameof(ADDMobile), db);
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("ClearMobiles")]
        public ActionResult ClearMobiles()
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    db.Mobiles.RemoveRange(db.Mobiles.ToList());
                    db.SaveChanges();
                    return NoContent();
                }
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveMobile(int id)
        {
            try
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
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public ActionResult Update(Mobiles Updated_Mobile)
        {
            try
            {           
                //note : when pass Updated_Mobile parametar pass it without id because id auto increment
                using (StolenContext db = new StolenContext())
                {

                    var old_Mobile = db.Mobiles.ToList().Find(User => User.Id == Updated_Mobile.Id);
                    if (old_Mobile is null) return BadRequest();
                    db.Entry(old_Mobile).CurrentValues.SetValues(Updated_Mobile);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
