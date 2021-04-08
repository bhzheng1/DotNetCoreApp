using WebApplication_API.ContosoEntities;
using WebApplication_API.DbContexts;

//本例演示使用GenericRepository
namespace WebApplication_API.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICoursepository
    {
        public CourseRepository(ContosoContext context) : base(context)
        {
            
        }


    }
}
