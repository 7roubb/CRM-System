using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Services;
using CRM.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Sales,Support,User")]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Sales")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound(new { message = "Contact not found" });
            }
            return Ok(contact);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Sales")]
        public async Task<IActionResult> Add([FromBody] ContactRequest contactRequest)
        {
            var contact = await _contactService.CreateContactAsync(contactRequest);
            return CreatedAtAction(nameof(GetOne), new { id = contact.Id }, contact);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound(new { message = "Contact not found" });
            }

            await _contactService.DeleteContactAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Sales")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ContactRequest contactRequest)
        {
            var existingContact = await _contactService.GetContactByIdAsync(id);
            if (existingContact == null)
            {
                return NotFound(new { message = "Contact not found" });
            }

            await _contactService.UpdateContactAsync(id, contactRequest);
            return NoContent();
        }

        [HttpGet("by-status/{statusId}")]
        [Authorize(Roles = "Admin,Sales,Support,User")]
        public async Task<IActionResult> GetByStatusId([FromRoute] int statusId)
        {
            var contacts = await _contactService.GetContactsByStatusIdAsync(statusId);
            return Ok(contacts);
        }
    }
}