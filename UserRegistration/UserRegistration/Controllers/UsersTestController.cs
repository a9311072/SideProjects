using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using UserRegistration.Models;
using UserRegistration.Repositories;
using UserRegistration.Services;
using UserRegistration.Infrastructures;

namespace UserRegistration.Controllers
{
    public class UserController : BaseCrudService<User, UserRepository>
    {
        [HttpPost]
        [ResponseType(typeof(User))]
        [Route("api/user/register")]
        public override async Task<IHttpActionResult> Post(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user.Email == null && (user.Phone == null && user.CountryCode == null))
                return BadRequest("Email or Phone should not null");

            if (user.Email != null && user.Email != string.Empty)
                if (_repo.FirstOrDefault(x => x.Email == user.Email) != null)
                    return BadRequest("Email 已被使用");

            if (user.Phone != null && user.Phone != string.Empty)
                if (_repo.FirstOrDefault(x => x.Phone == user.Phone) != null)
                    return BadRequest("Phone 已被使用");

            if (user.Password != user.ConfirmPassword)
                return BadRequest("The password is inconsistency");

            _repo.Add(user);
            await _repo.Commit();

            //if (user.Email != null) new Notification(new Email(user.Email, user.Name)).DoNotify();                   //Done
            //if (user.Phone != null) new Notification(new SMS(user.Phone, user.CountryCode, user.Name)).DoNotify();   //does not implement

            object token = new Token(new JwtToken()).GetToken(user.Name, 120);

            return Created("api/user/register", new { Token = token });
        }

        [HttpPut]
        public override async Task<IHttpActionResult> Put(int id, User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.Id)
                return BadRequest("Id is inconsistency");

            if (user.Email == null && (user.Phone == null && user.CountryCode == null))
                return BadRequest("Email or Phone should not null");

            if (user.Email != null && user.Email != string.Empty)
                if (_repo.FirstOrDefault(x => x.Email == user.Email && x.Id != id) != null)
                    return BadRequest("Email 已被使用");

            if (user.Phone != null && user.Phone != string.Empty)
                if (_repo.FirstOrDefault(x => x.Phone == user.Phone && x.Id != id) != null)
                    return BadRequest("Phone 已被使用");

            if (user.Password != user.ConfirmPassword)
                return BadRequest("The password is inconsistency");

            _repo.Update(user);
            await _repo.Commit();

            return Ok(user);
        }
    }
}