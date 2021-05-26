using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _context;
        
        //ContactRepo Constructor
        public ContactRepository(ContactDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds Contact to Database
        /// </summary>
        public void AddContact(Contact contact)
        {
            _context.Add(contact);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get Contact By Email from Database
        /// </summary>
        public Contact GetContactByEmail(string email)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Email == email);
            return contact;
        }

        /// <summary>
        /// Get Contact By Id from Database
        /// </summary>
        public Contact GetContactById(int contactId)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == contactId);
            return contact;
        }

        /// <summary>
        /// Get Contact By Name from Database
        /// </summary>
        public IEnumerable<Contact> GetContactByName(string name, bool includename)
        {
            var contact = _context.Contacts.Select(c => c).Where(c => c.FirstName == name || c.LastName == name).AsEnumerable();
            return contact;
        }

        /// <summary>
        /// Get Contact Count
        /// </summary>
        public int GetContactCount()
        {
            var count = _context.Contacts.Select(c => c).ToList().Count;
            return count;
        }

        /// <summary>
        /// Get All Contacts from Database
        /// </summary>
        public IEnumerable<Contact> GetContacts()
        {
            var contacts = _context.Contacts.Select(c => c).ToList();
            return contacts;
        }

        /// <summary>
        /// Remove Contact from Database
        /// </summary>
        public void RemoveContact(Contact contact)
        {
            _context.Remove(contact);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update Contact in Database
        /// </summary>
        public void UpdateContact(Contact contact)
        {
            var cont = _context.Contacts.FirstOrDefault(a => a.Id == contact.Id);
            _context.Entry(cont).CurrentValues.SetValues(contact);
            _context.SaveChanges();
        }
    }
}
