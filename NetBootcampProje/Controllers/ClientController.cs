using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBootcampProje.Models;

namespace NetBootcampProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ProjeDbContext _context;

        public ClientController()
        {
            _context = new ProjeDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clients = _context.Clients.Include(x => x.Company);
            return Ok(clients);
        }

        [HttpPost]
        public IActionResult Post(Client client)
        {
            Company company = _context.Companies.FirstOrDefault(x => x.Id == client.CompanyId);
            if (company == null)
            {
                return NotFound("Şirket bulunamadı");
            }
            client.Company = company;
            client.CompanyId = company.Id;
            _context.Clients.Add(client);
            _context.SaveChanges();
            return Ok(client);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Client client)
        {
            var update = _context.Clients.FirstOrDefault(x => x.Id == id);
            if (update == null)
            {
                return NotFound("Müşteri bulunamadı");
            }
            update.Name = client.Name;
            update.Surname = client.Surname;
            update.BirthDate = client.BirthDate;
            update.Adress = client.Adress;
            update.EMail = client.EMail;
            update.CompanyId = client.CompanyId;
            _context.SaveChanges();
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _context.Clients.FirstOrDefault(x => x.Id == id);
            if (delete == null)
            {
                return NotFound("Müşteri bulunamadı");
            }
            _context.Clients.Remove(delete);
            _context.SaveChanges();
            return Ok(delete);
        }
    }
}
