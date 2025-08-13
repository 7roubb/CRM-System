using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Model;
using System;

namespace CRM.Models
{
    public static class ContactMapper
    {
        public static Contact ToContact(this ContactRequest dto)
        {
            return new Contact
            {
                Contact_Title = dto.Contact_Title,
                Contact_First = dto.Contact_First,
                Contact_Middle = dto.Contact_Middle,
                Contact_Last = dto.Contact_Last,
                Contact_Type = dto.Contact_Type,
                Date_of_Initial_Contact = dto.Date_of_Initial_Contact,
                Company = dto.Company,
                Address_Street_1 = dto.Address_Street_1,
                Address_Street_2 = dto.Address_Street_2,
                Address_City = dto.Address_City,
                Address_State = dto.Address_State,
                Address_Zip = dto.Address_Zip,
                Address_Country = dto.Address_Country,
                Phone = dto.Phone,
                Email = dto.Email,
                Website = dto.Website,
                Linkedin_Profile = dto.Linkedin_Profile,
                Sales_Rep_ID = dto.Sales_Rep_ID,
                Contact_Status_ID = dto.Contact_Status_ID,
                Project_Type = dto.Project_Type,
                Proposal_Description = dto.Proposal_Description,
                Proposal_Date = dto.Proposal_Date,
                Deliverables = dto.Deliverables
            };
        }

        public static ContactResponse ToContactResponse(this Contact contact)
        {
            return new ContactResponse
            {
                Id = contact.Id,
                Contact_Title = contact.Contact_Title,
                Contact_First = contact.Contact_First,
                Contact_Middle = contact.Contact_Middle,
                Contact_Last = contact.Contact_Last,
                Contact_Type = contact.Contact_Type,
                Date_of_Initial_Contact = contact.Date_of_Initial_Contact,
                Company = contact.Company,
                Address_Street_1 = contact.Address_Street_1,
                Address_Street_2 = contact.Address_Street_2,
                Address_City = contact.Address_City,
                Address_State = contact.Address_State,
                Address_Zip = contact.Address_Zip,
                Address_Country = contact.Address_Country,
                Phone = contact.Phone,
                Email = contact.Email,
                Website = contact.Website,
                Linkedin_Profile = contact.Linkedin_Profile,
                Sales_Rep_ID = contact.Sales_Rep_ID,
                Contact_Status_ID = contact.Contact_Status_ID,
                Project_Type = contact.Project_Type,
                Proposal_Description = contact.Proposal_Description,
                Proposal_Date = contact.Proposal_Date,
                Deliverables = contact.Deliverables
            };
        }

        public static void UpdateFromContactRequest(this Contact contact, ContactRequest dto)
        {
            contact.Contact_Title = dto.Contact_Title;
            contact.Contact_First = dto.Contact_First;
            contact.Contact_Middle = dto.Contact_Middle;
            contact.Contact_Last = dto.Contact_Last;
            contact.Contact_Type = dto.Contact_Type;
            contact.Date_of_Initial_Contact = dto.Date_of_Initial_Contact;
            contact.Company = dto.Company;
            contact.Address_Street_1 = dto.Address_Street_1;
            contact.Address_Street_2 = dto.Address_Street_2;
            contact.Address_City = dto.Address_City;
            contact.Address_State = dto.Address_State;
            contact.Address_Zip = dto.Address_Zip;
            contact.Address_Country = dto.Address_Country;
            contact.Phone = dto.Phone;
            contact.Email = dto.Email;
            contact.Website = dto.Website;
            contact.Linkedin_Profile = dto.Linkedin_Profile;
            contact.Sales_Rep_ID = dto.Sales_Rep_ID;
            contact.Contact_Status_ID = dto.Contact_Status_ID;
            contact.Project_Type = dto.Project_Type;
            contact.Proposal_Description = dto.Proposal_Description;
            contact.Proposal_Date = dto.Proposal_Date;
            contact.Deliverables = dto.Deliverables;
        }

    }
}
