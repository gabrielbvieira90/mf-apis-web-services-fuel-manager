using mf_apis_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_apis_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context; //configuração do banco de dados, variével _context

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() //tarefa assincrona
        {
            var model = await _context.Usuarios.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsuarioDto model)
        {
            Usuario novo = new Usuario() //recebendo os dados do model
            {
                Nome = model.Nome,
                Password = BCrypt.Net.Bcrypt.HashPassword(model.Password),
                Perfil = model.Perfil
            };

            _context.Usuarios.Add(novo); //passando os dados do model novo
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = novo.Id }, novo); //retorno da criação mostrando o que criado
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Usuarios
                .FirstOrDefaultAsync(c => c.Id == id);

            if (model == null) return NotFound();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UsuarioDto model) //recebe o id mais o modelo pra atualizar todos os dados
        {
            if (id != model.Id) return BadRequest(); //validação para ver se o ID que estou modificando na rota é igual ao ID que está no modelo

            var modeloDb = await _context.Usuarios.AsNoTracking() //para nao rastrear e deixar os dados presos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (modeloDb == null) return NotFound();

            modeloDb.Nome = model.Nome;
            modeloDb.Password = BCrypt.Net.Bcrypt.HashPassword(model.Password);
            modeloDb.Perfil = model.Perfil;

            _context.Usuarios.Update(modeloDb);
            await _context.SaveChangesAsync();

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var model = await _context.Usuarios.FindAsync(id);

            if (model == null) return NotFound();

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}

