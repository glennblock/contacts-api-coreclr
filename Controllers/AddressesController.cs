using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Contacts.Infrastructure;

namespace Contacts.Controllers
{ 
    [Route("api/[controller]")]
    public class AddressesController : Controller 
    {
        private ContactsContext _context;
        
        public AddressesController(ContactsContext context) {
            _context = context;
        }
        
        public IEnumerable<Address> Get() {
            return _context.Addresses;
        }
    }
}