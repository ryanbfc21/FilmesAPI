using AutoMapper;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Data.Dtos.Filme;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme, UpdateFilmeDto>();
            CreateMap<Filme, ReadFilmeDto>()
                .ForMember(dto => dto.Sessoes,
                opt => opt.MapFrom(cinema => cinema.Sessoes));

        }
    }
}
