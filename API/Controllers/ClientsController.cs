using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Client> GetClients()
        {
            return _context.Clients.OrderByDescending(x => x.Id).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient(ClientDTO clientDTO)
        {
            var checkClientEmail = _context.Clients.FirstOrDefault(x => x.Email == clientDTO.Email);
            if (checkClientEmail is not null)
            {
                ModelState.AddModelError("Email", "O Email já está em uso");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var client = new Client
            {
                FirstName = clientDTO.FirstName,
                LastName = clientDTO.LastName,
                Email = clientDTO.Email,
                PhoneNumber = clientDTO.Phone ?? "",
                Address = clientDTO.Address ?? "",
                Status = clientDTO.Status,
                CreatedAt = DateTime.Now,
            };

            _context.Clients.Add(client);
            _context.SaveChanges();

            return Ok(client);
        }

        [HttpPut("{id}")]
        public IActionResult EditClient(int id, ClientDTO clientDTO)
        {
            var checkClientEmail = _context.Clients.FirstOrDefault(x => x.Email == clientDTO.Email);
            if (checkClientEmail is not null)
            {
                ModelState.AddModelError("Email", "O Email já está em uso");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var client = _context.Clients.Find(id);
            if (client == null)
                return NotFound();

            client.FirstName = clientDTO.FirstName;
            client.LastName = clientDTO.LastName;
            client.Email = clientDTO.Email;
            client.PhoneNumber = clientDTO.Phone ?? "";
            client.Address = clientDTO.Address ?? "";
            client.Status = clientDTO.Status;

            _context.SaveChanges();

            return Ok(client);
        }
    }
}
