using FilmesAPI.Data;
using FilmesAPI.Models;
using Newtonsoft.Json;

namespace FilmesAPI.Repository
{
    public class FilmeRepository
    {

        private FilmeContext _context;

        public FilmeRepository(FilmeContext context)
        {
            _context = context;
        }

        public FilmeRepository()
        {

        }

        static readonly string nomeArquivo = "Filmes.json";
        static readonly string diretorio = @"C:\Users\ryanb\Área de Trabalho";
        static readonly string caminhoArquivo = Path.Combine(diretorio, nomeArquivo);

        public void GravarFilme(Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
        }

        public List<Filme> ListFilmes()
        {
            return _context.Filmes.ToList();
        }

        public List<Filme> ListFilmesJson()
        {
            List<Filme> filmes = new List<Filme>();

            if (Directory.Exists(diretorio))
            {
                string[] arquivos = Directory.GetFiles(diretorio);
                string arquivo = arquivos.FirstOrDefault(x => x == caminhoArquivo);

                if (!string.IsNullOrEmpty(arquivo))
                {
                    var reader = new StreamReader(arquivo);
                    string text = reader.ReadToEnd();
                    reader.Dispose();

                    if (!string.IsNullOrEmpty(text))
                    {
                        filmes = JsonConvert.DeserializeObject<List<Filme>>(text);
                    }
                }
            }
            return filmes;
        }

        public Filme GravarFilmeJson(Filme filme)
        {
            FilmeRepository filmeRepository = new FilmeRepository();

            var filmes = filmeRepository.ListFilmesJson();
            filme.Id = filmeRepository.GetLastFilmeId(filmes) + 1;

            filmes.Add(filme);

            using (StreamWriter writer = new StreamWriter(caminhoArquivo))
            {

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    StringEscapeHandling = StringEscapeHandling.Default
                };

                writer.WriteLine(JsonConvert.SerializeObject(filmes, settings));
            }

            return filme;
        }

        public int GetLastFilmeId(List<Filme>filmes)
        {
            var ultimoFilme = filmes.LastOrDefault();
            return ultimoFilme != null ? ultimoFilme.Id : 0;
        }

        public Filme FindFilmePorId(int id)
        {
            FilmeRepository filmeRepository = new FilmeRepository();

                return _context.Filmes.FirstOrDefault(x => x.Id == id);
                
        }

    }
}
