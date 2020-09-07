using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.BusinessObjects.Collections;
using MyLayercake.NTier.Example.DataAccess;
using System;
using System.ComponentModel;

namespace MyLayercake.NTier.Example.BusinessLayer {
    /// <summary>
    /// The PhoneNumberManager class is responsible for managing BO.PhoneNumber objects in the system.
    /// </summary>
    [DataObjectAttribute()]
    public class PhoneNumberManager {

        #region Public Methods

        /// <summary>
        /// Gets a list with PhoneNumber objects for the requested contact person.
        /// </summary>
        /// <param name="contactPersonId">The ID of the ContactPerson for whom the phonenumbers should be returned.</param>
        /// <returns>A list with PhoneNumber objects when the database contains phonenumbers for the contact person, or an empty list otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static PhoneNumberList GetList(int contactPersonId) {
            return PhoneNumberDB.GetList(contactPersonId);
        }

        /// <summary>
        /// Gets a single PhoneNumber from the database.
        /// </summary>
        /// <param name="id">The unique ID of the phone number in the database.</param>
        /// <returns>An PhoneNumber object when the ID exists in the database, or <see langword="null"/> otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static PhoneNumber GetItem(int id) {
            return PhoneNumberDB.GetItem(id);
        }

        /// <summary>
        /// Saves a phone number in the database.
        /// </summary>
        /// <param name="myPhoneNumber">The PhoneNumber instance to save.</param>
        /// <returns>The new ID of the PhoneNumber is new in the database or the existing ID when an item was updated.</returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static int Save(PhoneNumber myPhoneNumber) {
            myPhoneNumber.Id = PhoneNumberDB.Save(myPhoneNumber);
            return myPhoneNumber.Id;
        }

        /// <summary>
        /// Deletes a phone number from the database.
        /// </summary>
        /// <param name="myPhoneNumber">The PhoneNumber instance to delete.</param>
        /// <returns>Returns true when the object was deleted successfully, or false otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static bool Delete(PhoneNumber myPhoneNumber) {
            return PhoneNumberDB.Delete(myPhoneNumber.Id);
        }

        #endregion
    }
}