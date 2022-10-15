using Microsoft.AspNetCore.Mvc;

using Usuario.Model;
using Usuario.Repository;

namespace Usuario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<User> users = await _repository.SearchUser();
            return users.Any()
                    ? Ok(users)
                    : NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            User? user = await _repository.SearchUser(id);
            return user != null
                    ? Ok(user)
                    : NotFound("Usuário não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            _repository.AddUser(user);
            return await _repository.SaveChangesAsync()
            ? Ok("Usuário adicionado com sucesso")
            : BadRequest("Não foi possível adicionar o usuário");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User user)
        {
            User? users = await _repository.SearchUser(id);
            if (users == null)
            {
                return NotFound("Usuário não encontrado");
            }

            string nome = user.Name ?? users.Name;
            DateTime dataNascimento = user.DataNascimento != new DateTime()
                ? user.DataNascimento : users.DataNascimento;
            users.AtualizaNomeEDataNascimento(nome, dataNascimento);

            _repository.UpdateUser(users);

            return await _repository.SaveChangesAsync()
            ? Ok("Usuário atulizado com sucesso")
            : BadRequest("Não foi possível atualizar o usuário");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            User? users = await _repository.SearchUser(id);
            if (users == null)
            {
                return NotFound("Usuário não encontrado");
            }

            _repository.DeleteUser(users);

            return await _repository.SaveChangesAsync()
            ? Ok("Usuário deletado com sucesso")
            : BadRequest("Não foi possível deletar o usuário");
        }
    }
}