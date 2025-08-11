using CRM.Data;
using CRM.Dto.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mapster;
using CRM.Dto.Responses;
using CRM.Model;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ContactsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Sales,Support,User")]
        public async Task<IActionResult> GetAll()
        {
            var Contacts = await context.Contact.ToListAsync();
            return Ok(Contacts);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Sales")]
        public async Task <IActionResult> GetOne([FromRoute]int id)
        {
            var contact = await context.Contact.FirstOrDefaultAsync(c => c.Id == id);
            if (contact == null)
            {
                return NotFound(new { message = "contact not found" });
            }
            return Ok(contact);
        
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Sales")]
        public async Task <IActionResult> Add(ContactRequest contactRequest)
        {
            var contact = contactRequest.Adapt<Contact>();
            await context.Contact.AddAsync(contact);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOne), new { id = contact.Id }, contact);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var contact= await context.Contact.FirstOrDefaultAsync(c=>c.Id == id);
            if(contact == null)
            {
                return NotFound(new { message = "contact not found" });
            }
            context.Contact.Remove(contact);  
            await context.SaveChangesAsync();
            return Ok();

        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Sales")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ContactRequest contactRequest)
        {
            var contact = await context.Contact.FirstOrDefaultAsync(c => c.Id == id);
            if (contact == null)
            {
                return NotFound(new { message = "contact not found" });
            }
            contactRequest.Adapt<Contact>();
            await context.SaveChangesAsync();

            return Ok(new { message = "Contact updated successfully" });
        }
        [HttpGet("by-status/{statusId}")]
        [Authorize(Roles = "Admin,Sales,Support,User")]
        public async Task<IActionResult> GetByStatusId([FromRoute] int statusId)
        {
            var contacts = await context.Contact
                .Include(c => c.Contact_Status)
                .Where(c => c.Contact_Status_ID == statusId)
                .ToListAsync();
            return Ok(contacts);
        }

    }
}
