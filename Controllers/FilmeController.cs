using FilmesAPI.Models;
using FilmesAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            FilmeRepository filmeRepository = new FilmeRepository();

            filme = filmeRepository.GravarFilme(filme);
            return CreatedAtAction(nameof(RecuperaFilmePorId), new {id = filme.Id}, filme);

        }

        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 1000)
        {
            FilmeRepository filmeRepository = new FilmeRepository();
            return filmeRepository.ListFilmes().Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            FilmeRepository filmeRepository = new FilmeRepository();
            var filme = filmeRepository.FindFilmePorId(id);

            if (filme != null)
            {
                return Ok(filme);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
