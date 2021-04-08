using System.Threading.Tasks;

namespace NSBus
{
    public interface IDatabaseInitializer
    {
        Task Apply();
    }
}
