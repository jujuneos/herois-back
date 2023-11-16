using Microsoft.AspNetCore.Mvc;
using SuperHerois.Context;

namespace SuperHerois.Controllers;

[Route("[controller]")]
[ApiController]
public class SuperPoderesController : ControllerBase
{
    private readonly SuperHeroisDbContext dbContext;

    public SuperPoderesController(SuperHeroisDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("listar")]
    public ActionResult<IEnumerable<dynamic>> Todos()
    {
        var superpoderes = dbContext.Superpoderes!.ToList();

        if (!superpoderes.Any())
            return NotFound("Nenhum super-poder cadastrado.");

        return Ok(superpoderes);
    }
}