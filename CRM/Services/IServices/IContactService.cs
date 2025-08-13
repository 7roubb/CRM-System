using global::CRM.Dto.Requests;
using global::CRM.Dto.Responses;
namespace CRM.Services.IServices

{
    public interface IContactService
    {
        Task<IEnumerable<ContactResponse>> GetAllContactsAsync();
        Task<ContactResponse> GetContactByIdAsync(int id);
        Task<ContactResponse> CreateContactAsync(ContactRequest contactRequest);
        Task UpdateContactAsync(int id, ContactRequest contactRequest);
        Task DeleteContactAsync(int id);
        Task<IEnumerable<ContactResponse>> GetContactsByStatusIdAsync(int statusId);
    }
}