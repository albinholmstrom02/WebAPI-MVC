using Backend.Contexts;
using Backend.Filters;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly DataContext _context;
        public ContactsController(DataContext context)
        {
            _context = context;
        }

        [UseApiKey]
        [HttpGet]
        public IActionResult Get()
        {
            var contacts = _context.Contacts.ToList();
            if (contacts.Count == 0)
            {
                return NotFound("Contacts not available");
            }
            return Ok(contacts);
        }

        [UseApiKey]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound($"Contact details not found with id {id}");
            }
            return Ok(contact);
        }

        [UseApiKey]
        [HttpPost]
        public IActionResult Post(ContactEntity contact)
        {
            _context.Add(contact);
            _context.SaveChanges();
            return Ok("Contact created");
        }

        [UseApiKey]
        [HttpPut]
        public IActionResult Put(ContactEntity contact)
        {
            if (contact == null || contact.Id == 0)
            {
                if (contact == null)
                {
                    return BadRequest("Contact data is invalid.");
                }
                else if (contact.Id == 0)
                {
                    return BadRequest($"Contact Id {contact.Id} is invalid");
                }
            }
            var model = _context.Contacts.Find(contact.Id);
            if (model == null)
            {
                return BadRequest($"Contact Id {contact.Id} is invalid");
            }
            model.FullName = contact.FullName;
            model.Title = contact.Title;
            model.Description = contact.Description;
            model.Email = contact.Email;
            model.Created = contact.Created;
            _context.SaveChanges();
            return Ok("Contact details updated");
        }

        [UseApiKey]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound($"Contact not found with id {id}");
            }

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return Ok("Contact details deleted.");
        }
    }
}
