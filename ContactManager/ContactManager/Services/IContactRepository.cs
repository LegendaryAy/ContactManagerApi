using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();

        void AddContact(Contact contact);

        Contact GetContactById(int contactId);

        Contact GetContactByEmail(string email);

        int GetContactCount();

        IEnumerable<Contact> GetContactByName(string name, bool includename);

        void UpdateContact(Contact contact);

        void RemoveContact(Contact contact);
    }
}
