using System;
using System.ComponentModel;
using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.BusinessObjects.Collections;
using MyLayercake.NTier.Example.DataAccess;

namespace MyLayercake.NTier.Example.BusinessLayer {
    /// <summary>
    /// The EmailAddressManager class is responsible for managing BO.EmailAddress objects in the system.
    /// </summary>
    [DataObjectAttribute()]
    public class EmailAddressManager {

        #region Public Methods

        /// <summary>
        /// Gets a list with EmailAddress objects for the requested contact person.
        /// </summary>
        /// <param name="contactPersonId">The ID of the ContactPerson for whom the e-mail addresses should be returned.</param>
        /// <returns>A list with e-mail address objects when the database contains e-mail addresses for the contact person, or an empty list otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static EmailAddressList GetList(int contactPersonId) {
            return EmailAddressDB.GetList(contactPersonId);
        }

        /// <summary>
        /// Gets a single EmailAddress from the database.
        /// </summary>
        /// <param name="id">The unique ID of the e-mail address in the database.</param>
        /// <returns>An EmailAddress object when the Id exists in the database, or <see langword="null"/> otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmailAddress GetItem(int id) {
            return EmailAddressDB.GetItem(id);
        }

        /// <summary>
        /// Saves an e-mail address in the database.
        /// </summary>
        /// <param name="myEmailAddress">The EmailAddress instance to save.</param>
        /// <returns>The new ID of the PhoneNumber is new in the database or the existing ID when an item was updated.</returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static int Save(EmailAddress myEmailAddress) {
            myEmailAddress.Id = EmailAddressDB.Save(myEmailAddress);
            return myEmailAddress.Id;
        }

        /// <summary>
        /// Deletes an e-mail address from the database.
        /// </summary>
        /// <param name="myEmailAddress">The EmailAddress instance to delete.</param>
        /// <returns>Returns true when the object was deleted successfully, or false otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static bool Delete(EmailAddress myEmailAddress) {
            return EmailAddressDB.Delete(myEmailAddress.Id);
        }

        #endregion

    }
}