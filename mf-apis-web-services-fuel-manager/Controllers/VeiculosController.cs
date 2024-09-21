using mf_apis_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_apis_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context; //configuração do banco de dados, variével _context

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() //tarefa assincrona
        {
            var model = await _context.Veiculos.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Veiculo model)
        {

            if (model.AnoFrabricacao <=0 || model.AnoModelo <=0)
            {
                return BadRequest(new { message = "Ano de Fabricação e Ano de Modelo são obrigatórios!" }); //mensagem caso não retorne a condição
            }
            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new {id = model.Id}, model); //retorno da criação mostrando o que criado
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Veiculos
                .Include(t => t.Consumos)// para puxar os dados do consumo associados ao veiculo
                .Include(t => t.Usuarios).ThenInclude(t => t.Usuario) // puxar o id e os dados do usuario
                .FirstOrDefaultAsync(c => c.Id == id);
                

            if (model == null) return NotFound();

            GerarLinks(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model) //recebe o id mais o modelo pra atualizar todos os dados
        {
            if (id != model.Id) return BadRequest(); //validação para ver se o ID que estou modificando na rota é igual ao ID que está no modelo

            var modeloDb = await _context.Veiculos.AsNoTracking() //para nao rastrear e deixar os dados presos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (modeloDb == null) return NotFound();

            _context.Veiculos.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           
            var model = await _context.Veiculos.FindAsync(id);

            if (model == null) return NotFound();

             _context.Veiculos.Remove(model);
             await _context.SaveChangesAsync();

             return NoContent();
        }

        private void GerarLinks(Veiculo model)
        {
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "delete", metodo: "Delete"));
        }

        [HttpPost("{id}/usuarios")] //criar rotas para adicionar um id especifico aos usuarios
        public async Task<ActionResult> AddUsuario(int id, VeiculoUsuario model)
        {
            if (id != model.VeiculoId) return BadRequest();

            _context.VeiculosUsuarios.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new {id = model.VeiculoId}, model);
        }

        [HttpDelete("{id}/usuarios/{usuarioId}")] 
        public async Task<ActionResult> DeleteUsuario(int id, int usuarioId)
        {
            var model = await _context.VeiculosUsuarios
                  .Where(c => c.VeiculoId == id && c.UsuarioId == usuarioId)
                  .FirstOrDefaultAsync();

            if (model == null) return NotFound();

            _context.VeiculosUsuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
    }
