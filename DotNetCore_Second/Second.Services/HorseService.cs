using Second.DataAccess.Repositories;
using Second.DataAccess.ApplicationDb;
using Second.Model;

namespace Second.Services
{
    public class HorseService : IHorseService
    {
        private readonly IGenericRepository<Horse> _horseRepository;

        public HorseService(IGenericRepository<Horse> horseRepository)
        {
            _horseRepository = horseRepository;
        }

        public IEnumerable<HorseModel> GetAll()
        {
            var horses = _horseRepository.GetAll();

            return horses.Select(Map);
        }

        public HorseModel? Get(int id)
        {
            var horse = _horseRepository.GetById(id);

            return horse == null ? null : Map(horse);
        }

        private static HorseModel Map(Horse horse)
        {
            return new HorseModel
            {
                Id = horse.Id,
                Name = horse.Name,
                Starts = horse.RaceStarts,
                Win = horse.RaceWins,
                Place = horse.RacePlace,
                Show = horse.RaceShow,
                Earnings = horse.Earnings
            };
        }
    }
}