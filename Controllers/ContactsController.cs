using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Contacts.Infrastructure;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Http.Extensions;

namespace Contacts.Controllers
{ 
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private ContactsContext _context;
        public ContactsController(ContactsContext context) {
            _context = context;
        }
        
        private Uri GetHref(int contactId) {
            var uri = new Uri(new Uri(Request.GetDisplayUrl()), string.Format("contacts/{0}", contactId));
            return uri;
        }
                    
        public ContactsState Get()
        {  
            return GetContactsState(_context.Contacts);       
/*
            var state = new ContactsState();
            state.Contacts = _context.Contacts.Include(c=>c.Address).Select(c=>new ContactState {
                Name = c.Name,
                Href = new Link {Rel="self", Href=GetHref(c.ContactId)},
                Address = new AddressState {
                    Address1 = c.Address.Address1,
                    Address2 = c.Address.Address2,
                    City = c.Address.City,
                    State = c.Address.State,
                    Country = c.Address.Country,
                    PostalCode = c.Address.PostalCode
                }
            });
            return state;
 */
        }
        
        [HttpGet("{id}")]
        public ContactsState Get(int id)
        {
            return GetContactsState(_context.Contacts.Where(c=>c.ContactId == id));
        }

        [HttpPost]
        public async Task<int> Post([FromBody]Contact value)
        {
            _context.Contacts.Add(value);
            return await _context.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody]Contact value)
        {
            var contact = _context.Contacts.Single(c=>c.ContactId == id);
            contact.Name = value.Name;
            if (value.Address != null) {
                var address = await _context.Addresses.ToAsyncEnumerable().Single(a=>a.AddressId == value.Address.AddressId);
                contact.Address = address;
            }
            
            return await _context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            var contact = _context.Contacts.Single(c=>c.ContactId == id);
            _context.Remove(contact);
            return await _context.SaveChangesAsync();
        }   
        
        private ContactsState GetContactsState(IQueryable<Contact> contacts) {
            var state = new ContactsState();
            state.Contacts = contacts.Include(c=>c.Address).Select(c=>new ContactState {
                Name = c.Name,
                Href = new Link {Rel="self", Href=GetHref(c.ContactId)},
                Address = new AddressState {
                    Address1 = c.Address.Address1,
                    Address2 = c.Address.Address2,
                    City = c.Address.City,
                    State = c.Address.State,
                    Country = c.Address.Country,
                    PostalCode = c.Address.PostalCode
                }
            });
            return state;
        }
     
    }
}
