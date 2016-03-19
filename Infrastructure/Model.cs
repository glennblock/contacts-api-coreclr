using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Entity;
using Microsoft.Extensions.PlatformAbstractions;

namespace Contacts.Infrastructure
{
    public class ContactsContext : DbContext
    {        
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }

    public class Address
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
    
    public class ContactsState {
        public IEnumerable<ContactState> Contacts {get;set;}
    }
    
    public class ContactState {
        public Link Href {get;set;}
        public string Name {get;set;}
        public AddressState Address {get;set;}
    }
    
    public class AddressState {
       public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }  
}

