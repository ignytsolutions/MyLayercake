using System;
using System.ComponentModel;
using System.Transactions;
using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.DataAccess;

namespace MyLayercake.NTier.Example.BusinessLayer {
    /// <summary>
    /// The ContactPersonManager class is responsible for managing BO.ContactPerson objects in the system.
    /// </summary>
    [DataObjectAttribute()]
    public class ContactPersonManager {

        #region Public Methods

        /// <summary>
        /// Gets a list with all ContactPerson objects in the database.
        /// </summary>
        /// <returns>A list with all contact persons from the database when the database contains any contact person, or null otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static ContactPersonList GetList() {
            return ContactPersonDB.GetList();
        }

        /// <summary>
        /// Gets a single ContactPerson from the database without its contact data.
        /// </summary>
        /// <param name="id">The ID of the contact person in the database.</param>
        /// <returns>A ContactPerson object when the ID exists in the database, or <see langword="null"/> otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ContactPerson GetItem(int id) {
            return GetItem(id, false);
        }

        /// <summary>
        /// Gets a single ContactPerson from the database.
        /// </summary>
        /// <param name="id">The ID of the contact person in the database.</param>
        /// <param name="getContactRecords">Determines whether to load all associated contact records as well.</param>
        /// <returns>
        /// A ContactPerson object when the ID exists in the database, or <see langword="null"/> otherwise.
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ContactPerson GetItem(int id, bool getContactRecords) {
            ContactPerson myContactPerson = ContactPersonDB.GetItem(id);

            if (myContactPerson != null && getContactRecords) {
                myContactPerson.Addresses = AddressDB.GetList(id);
                myContactPerson.EmailAddresses = EmailAddressDB.GetList(id);
                myContactPerson.PhoneNumbers = PhoneNumberDB.GetList(id);
            }
            return myContactPerson;
        }

        /// <summary>
        /// Saves a contact person in the database.
        /// </summary>
        /// <param name="myContactPerson">The ContactPerson instance to save.</param>
        /// <returns>The new ID if the ContactPerson is new in the database or the existing ID when an item was updated.</returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static int Save(ContactPerson myContactPerson) {
            using (TransactionScope myTransactionScope = new TransactionScope()) {
                int contactPersonId = ContactPersonDB.Save(myContactPerson);

                foreach (Address myAddress in myContactPerson.Addresses) {
                    myAddress.ContactPersonId = contactPersonId;
                    AddressDB.Save(myAddress);
                }

                foreach (EmailAddress myEmailAddress in myContactPerson.EmailAddresses) {
                    myEmailAddress.ContactPersonId = contactPersonId;
                    EmailAddressDB.Save(myEmailAddress);
                }

                foreach (PhoneNumber myPhoneNumber in myContactPerson.PhoneNumbers) {
                    myPhoneNumber.ContactPersonId = contactPersonId;
                    PhoneNumberDB.Save(myPhoneNumber);
                }

                //  Assign the ContactPerson its new (or existing ID).
                myContactPerson.Id = contactPersonId;

                myTransactionScope.Complete();

                return contactPersonId;
            }
        }

        /// <summary>
        /// Deletes a contact person from the database.
        /// </summary>
        /// <param name="myContactPerson">The ContactPerson instance to delete.</param>
        /// <returns>Returns true when the object was deleted successfully, or false otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static bool Delete(ContactPerson myContactPerson) {
            return ContactPersonDB.Delete(myContactPerson.Id);
        }
        #endregion

    }
}