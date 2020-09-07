using MyLayercake.NTier.Example.BusinessLayer;
using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyLayercake.Ntier.Example.Alternatives {
    public partial class UseApi : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            ContactPerson myContactPerson = new ContactPerson();
            myContactPerson.FirstName = "Imar";
            myContactPerson.LastName = "Spaanjaars";
            myContactPerson.DateOfBirth = new DateTime(1971, 8, 9);
            myContactPerson.Type = PersonType.Family;

            Address myAdress = new Address();
            myAdress.Street = "Some Street";
            myAdress.HouseNumber = "Some Number";
            myAdress.ZipCode = "Some Zip";
            myAdress.City = "Some City";
            myAdress.Country = "Some Country";
            myContactPerson.Addresses.Add(myAdress);

            EmailAddress myEmailAdress = new EmailAddress();
            myEmailAdress.Email = "Imar@DoNotSpam.com";
            myEmailAdress.Type = ContactType.Personal;
            myContactPerson.EmailAddresses.Add(myEmailAdress);

            PhoneNumber myPhoneNumber = new PhoneNumber();
            myPhoneNumber.Number = "555 - 2368";
            myPhoneNumber.Type = ContactType.Personal;
            myContactPerson.PhoneNumbers.Add(myPhoneNumber);

            ContactPersonManager.Save(myContactPerson);

            // Get an existing item from the database.
            myContactPerson = ContactPersonManager.GetItem(26, true);

        }
    }
}