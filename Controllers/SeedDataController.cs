using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Contacts.Infrastructure;

namespace Contacts.Controllers
{     
    [Route("api/[controller]")]
    public class SeedDataController : Controller
    {
        private ContactsContext _context;

        public SeedDataController(ContactsContext context) {
            _context = context;
        }
        
        [HttpPost]
        public async Task Initialize() {
            await _context.Contacts.ToAsyncEnumerable().ForEachAsync(e=>_context.Remove(e)); 
            await _context.Addresses.ToAsyncEnumerable().ForEachAsync(e=>_context.Remove(e)); 
            await AddContacts();
        }
       
        private async Task AddContacts() {            
            Address address;
            
            address=new Address {Address1="Some Street", City="Gurgaon", Country="India"};
            _context.Add(address);
            _context.Contacts.Add(new Contact {Name="Dhananjay Kumar", Address=address});
            
            address=new Address {Address1="Another Street", City="Seattle", State="WA", PostalCode="98112", Country="USA"};
            _context.Add(address);
            _context.Contacts.Add(new Contact {Name="Glenn Block", Address=address});
                        
            await _context.SaveChangesAsync();
        }
    }
}