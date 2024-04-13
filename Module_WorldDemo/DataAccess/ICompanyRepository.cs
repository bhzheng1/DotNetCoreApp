using ClassLibrary_DataAccess.Entities;

namespace ClassLibrary_DataAccess.DataAccess;

public interface ICompanyRepository
{
    Task<Company?> GetCompanyById(int id);
    Task<List<Company>> GetCompanies();
    Task<int> PutCompany(Company company);
    Task<int> PatchCompany(Company company);
}