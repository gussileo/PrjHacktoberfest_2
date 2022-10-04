using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var users = await _repository.SearchUser();
            return users.Any()
                    ? Ok(users)
                    : NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repository.SearchUser(id);
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
            var users = await _repository.SearchUser(id);
            if (users == null) return NotFound("Usuário não encontrado");
            users.Name = user.Name ?? users.Name;
            users.DataNascimento = user.DataNascimento != new DateTime()
                ? user.DataNascimento : users.DataNascimento;

            _repository.UpdateUser(users);

            return await _repository.SaveChangesAsync()
            ? Ok("Usuário atulizado com sucesso")
            : BadRequest("Não foi possível atualizar o usuário");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
            var users = await _repository.SearchUser(id);
            if (users == null) return NotFound("Usuário não encontrado");

            _repository.DeleteUser(users);

            return await _repository.SaveChangesAsync()
            ? Ok("Usuário deletado com sucesso")
            : BadRequest("Não foi possível deletar o usuário");
        }
    }
}