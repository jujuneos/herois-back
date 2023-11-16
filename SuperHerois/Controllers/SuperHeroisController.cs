using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHerois.Context;
using SuperHerois.Models;

namespace SuperHerois.Controllers;

[Route("[controller]")]
[ApiController]
public class SuperHeroisController : ControllerBase
{
    public readonly SuperHeroisDbContext dbContext;

    public SuperHeroisController(SuperHeroisDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost("CadastrarHeroi")]
    public ActionResult CadastrarHeroi(Heroi heroi)
    {
        var heroisQuery = dbContext
            .Herois!
            .Any(h => h.NomeHeroi == heroi.NomeHeroi);

        if (heroisQuery)
            return BadRequest("Já existe um Super-Herói com esse nome.");

        dbContext.Herois!.Add(heroi);
        dbContext.SaveChanges();

        return Ok("Super-Herói cadastrado com sucesso!");
    }

    [HttpPut("{id:int}")]
    public ActionResult EditarHeroi(int id, Heroi heroi)
    {
        if (id != heroi.Id)
            return NotFound("Super-Herói não encontrado.");

        dbContext.Entry(heroi).State = EntityState.Modified;

        var poderes = dbContext
            .HeroisSuperPoderes!
            .ToList();

        // Checa se houve algum novo poder adicionado e caso sim, inclui no banco
        foreach (var poder in heroi.Superpoderes!)
            if (!poderes.Exists(p => p.SuperPoderId == poder.SuperPoderId))
            {
                dbContext.HeroisSuperPoderes!.Add(poder);
            }

        dbContext.SaveChanges();

        return Ok("Super-Herói atualizado com sucesso!");
    }

    [HttpGet("{id:int}")]
    public ActionResult ConsultarHeroi(int id)
    {
        var heroi = dbContext
            .Herois!
            .Where(h => h.Id == id)
            .Select(h => new
            {
                id = h.Id,
                nome = h.Nome,
                nomeHeroi = h.NomeHeroi,
                dataNascimento = h.DataNascimento.ToShortDateString(),
                altura = h.Altura,
                peso = h.Peso,
                superpoderes = h.Superpoderes!.Select(s => s.SuperPoder!.Superpoder)
            })
            .SingleOrDefault();

        if (heroi is null)
            return NotFound("Super-Herói não encontrado.");

        return Ok(heroi);
    }

    [HttpGet("listar")]
    public ActionResult<IEnumerable<dynamic>> ListarHerois()
    {
        var herois = dbContext
            .Herois!
            .AsNoTracking()
            .Select(h => new
            {
                id = h.Id,
                nome = h.Nome,
                nomeHeroi = h.NomeHeroi,
                dataNascimento = h.DataNascimento.ToShortDateString(),
                altura = h.Altura,
                peso = h.Peso,
                superpoderes = h.Superpoderes!.Select(s => s.SuperPoder!.Superpoder)
            })
            .ToList();

        if (herois is null)
            return NotFound("Nenhum Super-Herói cadastrado.");

        return Ok(herois);
    }

    [HttpDelete("{id:int}")]
    public ActionResult ExcluirHeroi(int id)
    {
        var heroi = dbContext
            .Herois!
            .SingleOrDefault(h => h.Id == id);

        if (heroi is null)
            return NotFound("Super-Herói não encontrado.");

        dbContext.Herois!.Remove(heroi);
        dbContext.SaveChanges();

        return Ok("Super-Herói removido com sucesso!");
    }
}