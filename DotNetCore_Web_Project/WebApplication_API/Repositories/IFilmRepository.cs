using ModelClassLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication_API.Repositories
{
    public interface IFilmRepository
    {
        Task<string> CreateFilm(FilmModel film);
        Task<string> DeleteFilmById(int id);
        Task<IList<FilmModel>> SelectAllFilms();
        Task<FilmModel> SelectById(int id);
        Task<string> UpdateFilm(FilmModel film);
    }
}