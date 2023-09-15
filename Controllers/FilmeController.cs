using AutoMapper;
using FilmesAPI.Data;
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

        private FilmeContext _context;
        private IMapper _mapper;


        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            FilmeRepository filmeRepository = new FilmeRepository(_context);

            Filme filme = _mapper.Map<Filme>(filmeDto);

            filmeRepository.GravarFilme(filme);
            return CreatedAtAction(nameof(RecuperaFilmePorId), new {id = filme.Id}, filme);

        }

        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 1000)
        {
            FilmeRepository filmeRepository = new FilmeRepository(_context);
            return filmeRepository.ListFilmes().Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            FilmeRepository filmeRepository = new FilmeRepository(_context);
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
