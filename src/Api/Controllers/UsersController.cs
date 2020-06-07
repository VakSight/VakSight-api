using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaceses;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        // POST api/users
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserModel model)
        {
            var user = new UserDTO { Email = model.Email, Password = model.Password };
            await service.CreateUserAsync(user);

            return Ok();
        }

        // PUT api/users/<id>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/users/<id>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
