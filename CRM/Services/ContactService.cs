using CRM.Data;
using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Model;
using CRM.Models;
using CRM.Services;
using CRM.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;

        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactResponse>> GetAllContactsAsync()
        {
            var contacts = await _context.Contact.ToListAsync();
            return contacts.Select(c => c.ToContactResponse());
        }

        public async Task<ContactResponse> GetContactByIdAsync(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            return contact?.ToContactResponse();
        }

        public async Task<ContactResponse> CreateContactAsync(ContactRequest contactRequest)
        {
            var contact = contactRequest.ToContact();
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
            return contact.ToContactResponse();
        }

        public async Task UpdateContactAsync(int id, ContactRequest contactRequest)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null) return;

            contact.UpdateFromContactRequest(contactRequest);
            _context.Contact.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null) return;

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactResponse>> GetContactsByStatusIdAsync(int statusId)
        {
            var contacts = await _context.Contact
                .Where(c => c.Contact_Status_ID == statusId)
                .ToListAsync();

            return contacts.Select(c => c.ToContactResponse());
        }
    }
}