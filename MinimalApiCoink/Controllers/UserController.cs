using Microsoft.AspNetCore.Mvc;
using MinimalApiCoink.Interfaces;
using MinimalApiCoink.Models;
using MinimalApiCoink.Repositories;

namespace MinimalApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var users = _userRepository.GetUsers();
                if (users == null || !users.Any())
                {
                    return NotFound();
                }
                return Ok(users);
            }
            catch (Exception)
            {
                //aqui algun servicio para el log del error
                return StatusCode(500, "Ha ocurrido un error al intentar obtener los usuarios");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return user;
            }
            catch (Exception)
            {
                //aqui algun servicio para el log del error
                return StatusCode(500, "Ha ocurrido un error al intentar obtener el usuario");
            }
        }

        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.Direccion) || string.IsNullOrEmpty(user.Telefono) || 0 == user.IdMunicipio)
                    return BadRequest("Nombre, Apellido y Email son campos obligatorios.");

                _userRepository.AddUser(user);
                return CreatedAtAction(nameof(Get), new { id = user.IdUsuario }, user);
            }
            catch (Exception)
            {
                //aqui algun servicio para el log del error
                return StatusCode(500, "Ha ocurrido un error al intentar guardar el usuario");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.Direccion) || string.IsNullOrEmpty(user.Telefono) || 0 == user.IdMunicipio)
                    return BadRequest("Nombre, Apellido y Email son campos obligatorios.");
                var user_update = _userRepository.GetUserById(user.IdUsuario);
                if (user == null)
                {
                    return NotFound();
                }
                _userRepository.UpdateUser(user);
                return NoContent();
            }
            catch (Exception)
            {
                //aqui algun servicio para el log del error
                return StatusCode(500, "Ha ocurrido un error al intentar actualizar el usuario");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(id);
            return NoContent();
        }
    }
}