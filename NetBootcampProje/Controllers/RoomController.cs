using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBootcampProje.Models;

namespace NetBootcampProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ProjeDbContext _context;

        public RoomController()
        {
            _context = new ProjeDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rooms = _context.Rooms;
            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult Post(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return Ok(room);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Room room)
        {
            var update = _context.Rooms.FirstOrDefault(x => x.Id == id);
            if (update == null)
            {
                return NotFound("Oda bulunamadı");
            }
            update.Name = room.Name;
            _context.SaveChanges();
            return Ok(update);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _context.Rooms.FirstOrDefault(x => x.Id == id);
            if (delete == null)
            {
                return NotFound("Oda bulunamadı");
            }
            _context.Rooms.Remove(delete);
            _context.SaveChanges();
            return Ok(delete);
        }
    }
}
