using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StolenMobilesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace StolenMobilesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet("{imei}")]
        public ActionResult<Mobiles> Search(string imei)
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    var Mobile = db.Mobiles.ToList().Find(Mobile => Mobile.IMEI == imei);
                    if (Mobile is null) return NotFound();
                    Mobiles Mobile_With_New_Location = Mobile;
                    Mobile_With_New_Location.StolenIn = "University";
                    //Mobile_With_New_Location.StolenIn=""   //add new location here (in this line)
                    db.Entry(Mobile).CurrentValues.SetValues(Mobile_With_New_Location);
                    db.SaveChanges();
                    return Mobile;
                }

            }
            catch  (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
