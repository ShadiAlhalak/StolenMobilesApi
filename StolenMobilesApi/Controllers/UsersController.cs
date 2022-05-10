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
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Users>> GetUsers()
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    return db.Users.ToList();
                }
            }
            catch
            {
                return NotFound();
            }
      
        }

        [HttpGet("{id}")]
        public ActionResult<Users> GetUser(int id)
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    var user = db.Users.ToList().Find(User => User.Id == id);
                    if (user is null) return NotFound();
                    return user;
                }
            }
            catch
            { 
            return NotFound();
            }
     
        }

        [HttpPost("SignUp")]
        public ActionResult SignUp([FromBody] Users User)
        {
            try
            {
                //note : when pass User parametar pass it without id because id auto increment
                if (User is null)
                    return BadRequest();
                using (StolenContext db = new StolenContext())
                {
                    if (db.Users.ToList().FirstOrDefault(ur => ur.Email == User.Email) is not null)
                        return BadRequest();
                    db.Users.Add(User);
                    db.SaveChanges();
                    return Ok();
                    //return CreatedAtAction(nameof(SignUp), db);
                }
            }
            catch
            {
                return BadRequest();
            }
        
        }

        [HttpPost("SignIn")]
        public ActionResult SignIn( string username ,  string password)
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    var user = db.Users.ToList().Find(User => User.UserName == username && User.UserPassword == password);
                    if (user is null) return NotFound();
                    return Ok();
                }
            }
            catch
            {
                return NotFound();
            }
        
        }

        [HttpDelete]
        public ActionResult ClearUsers()
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    db.Users.RemoveRange(db.Users.ToList());
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
        public ActionResult RemoveUser(int id)
        {
            try
            {
                using (StolenContext db = new StolenContext())
                {
                    var User = db.Users.ToList().Find(user => user.Id == id);
                    if (User is null) return NotFound();
                    db.Users.Remove(User);
                    db.SaveChanges();
                    return NoContent();
                }
            }
            catch
            {
                return NotFound();
            }
     
        }

        [HttpPut]
        public ActionResult Update( Users Updated_user)
        {
            try
            {
                //note : when pass Updated_user parametar pass it without id because id auto increment
                using (StolenContext db = new StolenContext())
                {
                    var old_user = db.Users.ToList().Find(User => User.Id == Updated_user.Id);
                    if (old_user is null) return BadRequest();
                    db.Entry(old_user).CurrentValues.SetValues(Updated_user);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
      
        }

    }
}