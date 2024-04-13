using Module_WorldDemo.Entities;

namespace Module_WorldDemo.DataAccess;

public interface ICompanyRepository
{
    Task<Company?> GetCompanyById(int id);
    Task<List<Company>> GetCompanies();
    Task<int> PutCompany(Company company);
    Task<int> PatchCompany(Company company);
}