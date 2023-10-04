using AutoMapper;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema> ();
            CreateMap<Cinema, CreateCinemaDto>();
            CreateMap<ReadCinemaDto, Cinema>();
            CreateMap<Cinema, ReadCinemaDto>().ForMember(dto => dto.EnderecoDto,
                opt => opt.MapFrom(cinema => cinema.Endereco)); ;


        }
    }
}
