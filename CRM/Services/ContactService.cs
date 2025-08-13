// ContactService.cs
using CRM.Data;
using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Exceptions;
using CRM.Model;
using CRM.Models;
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
            if (contact == null)
                throw new ContactNotFoundException(id);

            return contact.ToContactResponse();
        }

        public async Task<ContactResponse> CreateContactAsync(ContactRequest contactRequest)
        {
            // Check for duplicate email
            if (await _context.Contact.AnyAsync(c => c.Email == contactRequest.Email))
                throw new ContactAlreadyExistsException(contactRequest.Email);

            var contact = contactRequest.ToContact();
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
            return contact.ToContactResponse();
        }

        public async Task UpdateContactAsync(int id, ContactRequest contactRequest)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
                throw new ContactNotFoundException(id);

            // Check for duplicate email if changing
            if (contact.Email != contactRequest.Email &&
                await _context.Contact.AnyAsync(c => c.Email == contactRequest.Email))
            {
                throw new ContactAlreadyExistsException(contactRequest.Email);
            }

            contact.UpdateFromContactRequest(contactRequest);
            _context.Contact.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
                throw new ContactNotFoundException(id);

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