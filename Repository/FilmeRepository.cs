using FilmesAPI.Models;
using Newtonsoft.Json;

namespace FilmesAPI.Repository
{
    public class FilmeRepository
    {
        static readonly string nomeArquivo = "Filmes.json";
        static readonly string diretorio = @"C:\Users\ryanb\Área de Trabalho";
        static readonly string caminhoArquivo = Path.Combine(diretorio, nomeArquivo);

        public List<Filme> ListFilmes()
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

        public Filme GravarFilme(Filme filme)
        {
            FilmeRepository filmeRepository = new FilmeRepository();

            var filmes = filmeRepository.ListFilmes();
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

            var filmes = filmeRepository.ListFilmes();

            if (filmes.Count > 0)
            {
                return filmes.FirstOrDefault(x => x.Id == id);
            }

            return null;
        }

    }
}
