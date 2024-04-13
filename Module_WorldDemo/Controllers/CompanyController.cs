using ClassLibrary_DataAccess.DataAccess;
using ClassLibrary_DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClassLibrary_DataAccess.Controllers;

[ApiController]
[Route("api/dataAccess/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;
    public CompanyController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompanies()
    {
        return Ok(await _companyRepository.GetCompanies());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompany(int id)
    {
        return Ok(await _companyRepository.GetCompanyById(id));
    }

    [HttpPut]
    public async Task<IActionResult> EditCompany(Company company)
    {
        return Ok(await _companyRepository.PutCompany(company));
    }

    [HttpPatch]
    public async Task<IActionResult> PatchCompany(Company company)
    {
        return Ok(await _companyRepository.PatchCompany(company));
    }
}