using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Contacts.Infrastructure;

namespace Contacts.Migrations
{
    [DbContext(typeof(ContactsContext))]
    partial class ContactsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Contacts.Infrastructure.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("PostalCode");

                    b.Property<string>("State");

                    b.HasKey("AddressId");
                });

            modelBuilder.Entity("Contacts.Infrastructure.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressAddressId");

                    b.Property<string>("Name");

                    b.HasKey("ContactId");
                });

            modelBuilder.Entity("Contacts.Infrastructure.Contact", b =>
                {
                    b.HasOne("Contacts.Infrastructure.Address")
                        .WithMany()
                        .HasForeignKey("AddressAddressId");
                });
        }
    }
}
