using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBootcampProje.Models;

namespace NetBootcampProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ProjeDbContext _context;

        public ReservationController()
        {
            _context = new ProjeDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var reservations = _context.Reservations.Include(x => x.Room).Include(x => x.Client).ToList();
            return Ok(reservations);
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var reservation = _context.Reservations.Include(x => x.Room).Include(x => x.Client).FirstOrDefault(x => x.Id == id);
            if (reservation == null)
            {
                return NotFound("Rezervasyon bulunamadı");
            }
            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            if (reservation.StartDate <= reservation.AddDate || reservation.EndDate <= reservation.StartDate)
            {
                return NotFound("Rezervasyon başlangıç tarihi bugünden veya  rezervasyon bitiş tarihi başlangıç gününden önce olamaz");
            }
            Client client = _context.Clients.FirstOrDefault(x => x.Id == reservation.ClientId);
            Room room = _context.Rooms.FirstOrDefault(x => x.Id == reservation.RoomId);
            if (client == null)
            {
                return NotFound("Müşteri bulunamadı");
            }
            reservation.Client = client;
            reservation.ClientId = client.Id;
            if (room == null)
            {
                return NotFound("Oda bulunamadı");
            }
            reservation.Room = room;
            reservation.RoomId = room.Id;
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return Ok(reservation);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Reservation reservation)
        {
            var update = _context.Reservations.FirstOrDefault(x => x.Id == id);
            if (update == null)
            {
                return NotFound("Rezervasyon bulunamadı");
            }
            else if (update.StartDate < update.AddDate || update.EndDate < update.StartDate)
            {
                return NotFound("Rezervasyon başlangıç tarihi bugünden veya bitiş gününden önce olamaz");
            }
            update.ClientId = reservation.ClientId;
            update.RoomId = reservation.RoomId;
            update.StartDate = reservation.StartDate;
            update.EndDate = reservation.EndDate;
            _context.SaveChanges();
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _context.Reservations.FirstOrDefault(x => x.Id == id);
            if (delete == null)
            {
                return NotFound("Rezervasyon bulunamadı");
            }
            _context.Reservations.Remove(delete);
            _context.SaveChanges();
            return Ok(delete);
        }
    }
}
