using Microsoft.AspNetCore.Mvc;

namespace RESTFull_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CompaniesController : ControllerBase
{
    public List<Company> _companies;
    public CompaniesController()
    {
        _companies = new List<Company> {
            new Company(){ Id = 1, Name = "ABC Company" },
            new Company() { Id=2, Name="D Company"},
            new Company() { Id=3, Name="R Company"},
            };
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        await Task.CompletedTask;
        return Ok(_companies);
    }

    [HttpGet("company/{id}")]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        var requiredCompany = _companies.Find(a => a.Id == id);
        await Task.CompletedTask;
        if (requiredCompany == null)
        {
            return NotFound(requiredCompany);
        }
        return Ok(requiredCompany);
    }

    [HttpPost("company")]
    public async Task<IActionResult> AddCompany(Company company)
    {
        _companies.Add(company);
        await Task.CompletedTask;
        return CreatedAtAction("GetCompanyById", new { company.Id }, company);
    }

    [HttpPut("company/{id}")]
    public async Task<IActionResult> UpdateCompany(int id, [FromBody] Company company)
    {
        var companytoBeUpdate = _companies.Find(a => a.Id == id);

        if (id != company.Id)
        {
            return BadRequest(company.Id);
        }

        if (companytoBeUpdate == null)
        {
            return NotFound(companytoBeUpdate);
        }

        await Task.CompletedTask;

        companytoBeUpdate.Name = company.Name;
        return NoContent();
    }

    [HttpDelete("company/{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        await Task.CompletedTask;
        var companytoBeDeleted = _companies.Find(a => a.Id == id);
        if(companytoBeDeleted==null)
        {
            return NotFound(companytoBeDeleted);
        }
        _companies.Remove(companytoBeDeleted);
        return NoContent();
    }
}

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
}