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
    public class ImagesController : ControllerBase
    {
        [HttpGet("GetImage")]
        public ActionResult GetImage(int MobileID)
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    byte[] img = db.Mobiles.Include(mob => mob.Image).ToList().Find(x => x.Id == MobileID).Image.ImageData;
                    return File(img, "Image/jpg");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetTitle")]
        public ActionResult<string> GetTitle(int MobileID)
        {
        try
        {
                using (StolenContext db = new StolenContext())
                {
                    var mob = db.Mobiles.Include(mob => mob.Image).ToList().FirstOrDefault(x => x.Id == MobileID);
                    if (mob is null | mob.Image is null) return NotFound();
                    string title =mob.Image.ImageTitle;
                    return title;
                }
            }
        catch
        {
             return StatusCode(StatusCodes.Status500InternalServerError);
        }
        }

        [HttpPut("ADDImage")]
        public ActionResult ADDImage(int Mobileid, string Title, IFormFile image)
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    var old_Mobile = db.Mobiles.Include(mob=>mob.Image).ToList().Find(Mobile => Mobile.Id == Mobileid);
                    if (old_Mobile is null) return BadRequest();
                    if (old_Mobile.Image is not null)
                    {
                        var img = db.Images.ToList().Find(image => image.Id == old_Mobile.Image.Id);
                        if (img is null) return NotFound();
                        db.Images.Remove(img);
                        if (old_Mobile is null) return NotFound();
                        Mobiles newMobDeletedImg= old_Mobile;
                        newMobDeletedImg.Image = null;
                        db.Entry(old_Mobile).CurrentValues.SetValues(newMobDeletedImg);
                        db.SaveChanges();
                    }
                    Mobiles newMobWithImage = old_Mobile;
                    Images mobImage = new Images();

                    using (var ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        mobImage.ImageData = fileBytes;
                    }

                    mobImage.ImageTitle = Title;
                    newMobWithImage.Image = mobImage;
                    db.Entry(old_Mobile).CurrentValues.SetValues(newMobWithImage);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete("DeleteImage")]
        public ActionResult DeleteImage(int MobileID)
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    var old_Mobile = db.Mobiles.Include(mob => mob.Image).ToList().Find(Mobile => Mobile.Id == MobileID);
                    var img = db.Images.ToList().Find(image => image.Id == old_Mobile.Image.Id);
                    if (img is null) return NotFound();
                    db.Images.Remove(img);
                    if (old_Mobile is null) return NotFound();
                    Mobiles newMobWithImage = old_Mobile;
                    newMobWithImage.Image = null;
                    db.Entry(old_Mobile).CurrentValues.SetValues(newMobWithImage);
                    db.SaveChanges();
                    return NoContent();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}
