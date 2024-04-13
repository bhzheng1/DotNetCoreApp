using ClassLibrary_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary_DataAccess.DataAccess;

public class CompanyRepository : ICompanyRepository
{
    private readonly ClDataBaseContext _context;
    public CompanyRepository(ClDataBaseContext context)
    {
        _context = context;
    }
    public async Task<Company?> GetCompanyById(int id) => await _context.Companies.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Company>> GetCompanies()
    {
        Console.WriteLine(_context.Companies.ToQueryString());
        return await _context.Companies.ToListAsync();
    }

    public async Task<int> PutCompany(Company company)
    {
        if (await _context.Companies.FirstOrDefaultAsync(p => p.Id == company.Id) is Company found)
        {
            // ref: https://stackoverflow.com/questions/46657813/how-to-update-record-using-entity-framework-core
            _context.Companies.Entry(found).State = EntityState.Detached;
        }
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
        return company.Id;
    }

    public async Task<int> PatchCompany(Company company)
    {
        if (await _context.Companies.FirstOrDefaultAsync(p => p.Id == company.Id) is Company found)
        {
            // ref: https://stackoverflow.com/questions/46657813/how-to-update-record-using-entity-framework-core
            if (company.Name != null) found.Name = company.Name;
            if (company.Address != null) found.Address = company.Address;
            await _context.SaveChangesAsync();
            return company.Id;
        }
        return 0;
    }
}