using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication_API.SakilaModels;

namespace WebApplication_API.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> CreateCategory(Category Category);
        Task DeleteCategory(Category Category);
        Task<IEnumerable<Category>> GetAllCategorysAsync();
        Task<Category> GetCategoryByIdAsync(int CategoryId);
        Task<Category> GetCategoryWithDetailsAsync(int CategoryId);
        Task UpdateCategory(Category Category);
    }
}