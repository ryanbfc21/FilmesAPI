using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Repository
{
    public class CinemaRepository 
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaRepository(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void GravarCinema(CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
        }

        public Cinema FindCinemaPorId(int id)
        {
            return _context.Cinemas.FirstOrDefault(x => x.Id == id);
        }
    }
}
