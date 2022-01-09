using Second.Model;

namespace Second.Services
{
    public interface IHorseService
    {
        IEnumerable<HorseModel> GetAll();
        HorseModel? Get(int id);
    }
}