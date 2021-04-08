using HelperClassLibrary;
using Microsoft.EntityFrameworkCore;
using ModelClassLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_API.DbContexts;
using WebApplication_API.SakilaModels;

//本例演示简单的Asynchronous Task
namespace WebApplication_API.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private SakilaContextMSSQL _dbContext;
        public FilmRepository(SakilaContextMSSQL context)
        {
            _dbContext = context;
        }

        public async Task<IList<FilmModel>> SelectAllFilms()
        {
            return await _dbContext.Film
                .Where(f => f.Title.Contains("g"))
                .OrderByDescending(f => f.Title)
                .Select(f => f.Copy<Film, FilmModel>())
                .ToListAsync();
        }

        public async Task<IList<FilmModel>> SelectAllFilmsLinq()
        {

            return await _dbContext.Film.Select(f => f.Copy<Film, FilmModel>()).ToListAsync();
        }

        public async Task<FilmModel> SelectById(int id)
        {
            var film = await _dbContext.Film.SingleOrDefaultAsync(f => f.FilmId == id);

            return film?.Copy<Film, FilmModel>();
        }

        public async Task<string> CreateFilm(FilmModel film)
        {

            try
            {
                var existFilm = await _dbContext.Film.Where(_ => _.FilmId == film.FilmId).FirstOrDefaultAsync();
                if (existFilm != null)
                    return await Task.FromResult<string>("film already exists");
                await _dbContext.Film.AddAsync(film.Copy<FilmModel, Film>());
                await _dbContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
            return await Task.FromResult<string>(null);
        }

        public async Task<string> UpdateFilm(FilmModel film)
        {
            try
            {
                var existFilm = await _dbContext.Film.Where(_ => _.FilmId == film.FilmId).FirstOrDefaultAsync();
                if (existFilm == null)
                    return await Task.FromResult<string>("film does not exists");
                existFilm.Title = film.Title;
                await _dbContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
            return await Task.FromResult<string>(null);
        }

        public async Task<string> DeleteFilmById(int id)
        {
            try
            {
                var existFilm = await _dbContext.Film.Where(_ => _.FilmId == id).FirstOrDefaultAsync();
                if (existFilm == null)
                    return await Task.FromResult<string>("film does not exists");
                _dbContext.Film.Remove(existFilm);
                await _dbContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
            return await Task.FromResult<string>(null);
        }
    }
}
