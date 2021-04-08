using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_API.DbContexts;
using WebApplication_API.SakilaModels;

//本例演示Base Repository pattern
namespace WebApplication_API.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SakilaContextMSSQL dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategorysAsync()
        {
            return (await FindAll()).OrderBy(ow => ow.Name);
        }

        public async Task<Category> GetCategoryByIdAsync(int CategoryId)
        {
            return (await FindByCondition(Category => Category.CategoryId.Equals(CategoryId))).FirstOrDefault();
        }

        public async Task<Category> GetCategoryWithDetailsAsync(int CategoryId)
        {
            return await FindByCondition(Category => Category.CategoryId.Equals(CategoryId))
                .Result
                .Include(ac => ac.FilmCategory)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateCategory(Category Category)
        {
            return await Add(Category);
        }

        public async Task UpdateCategory(Category Category)
        {
            await Update(Category);
        }

        public async Task DeleteCategory(Category Category)
        {
            await Remove(Category);
        }
    }
}
