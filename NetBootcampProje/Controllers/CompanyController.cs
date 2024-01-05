using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetBootcampProje.Models;

namespace NetBootcampProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ProjeDbContext _context;

        public CompanyController()
        {
            _context = new ProjeDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var companies = _context.Companies;
            return Ok(companies);
        }

        [HttpPost]
        public IActionResult Post(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return Ok(company);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Company company)
        {
            var update = _context.Companies.FirstOrDefault(x => x.Id == id);
            if (update == null)
            {
                return NotFound("Şirket bulunamadı");
            }
            update.Name = company.Name;
            update.Adress = company.Adress;
            _context.SaveChanges();
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _context.Companies.FirstOrDefault(x =>x.Id == id);
            if (delete == null)
            {
                return NotFound("Şirket bulunamadı");
            }
            _context.Companies.Remove(delete);
            _context.SaveChanges();
            return Ok(delete);
        }

    }
}
