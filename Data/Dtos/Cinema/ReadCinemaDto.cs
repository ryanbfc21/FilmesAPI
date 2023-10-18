using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Data.Dtos.Sessao;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.Cinema
{
    public class ReadCinemaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ReadEnderecoDto Endereco { get; set; }
        public ICollection<ReadSessaoDto> Sessoes { get; set; }

    }
}
