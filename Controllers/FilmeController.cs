using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Repository;
using Microsoft.AspNetCore.JsonPatch;
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

        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">
        /// Objeto com os campos necessários para criação de um filme
        /// </param>
        /// <returns>IActionResult</returns>
        /// <response code = "201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            FilmeRepository filmeRepository = new FilmeRepository(_context);

            Filme filme = _mapper.Map<Filme>(filmeDto);

            filmeRepository.GravarFilme(filme);
            return CreatedAtAction(nameof(RecuperaFilmePorId), new {id = filme.Id}, filme);

        }

        [HttpGet]
        public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 1000)
        {
            FilmeRepository filmeRepository = new FilmeRepository(_context);
            var filmes = filmeRepository.ListFilmes().Skip(skip).Take(take);
            return _mapper.Map<List<ReadFilmeDto>>(filmes);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            FilmeRepository filmeRepository = new FilmeRepository(_context);
            var filme = filmeRepository.FindFilmePorId(id);

            if (filme != null)
            {
                var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(filmeDto);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            FilmeRepository filmeRepository = new FilmeRepository(_context);
            var filme = filmeRepository.FindFilmePorId(id);

            if (filme == null)
            {
                return NotFound();
            }

            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            FilmeRepository filmeRepository = new FilmeRepository(_context);
            var filme = filmeRepository.FindFilmePorId(id);

            if (filme == null)
            {
                return NotFound();
            }

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            FilmeRepository filmeRepository = new FilmeRepository(_context);
            var filme = filmeRepository.FindFilmePorId(id);

            if (filme == null)
                return NotFound();

            _context.Remove(filme);
            _context.SaveChanges(true);

            return NoContent();


        }

    }
}
