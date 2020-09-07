using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.BusinessObjects.Collections;
using MyLayercake.NTier.Example.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyLayercake.NTier.Example.BusinessLayer {
    /// <summary>
    /// The AddressManager class is responsible for managing BO.Address objects in the system.
    /// </summary>
    [DataObjectAttribute()]
    public class AddressManager {

        #region Public Methods

        /// <summary>
        /// Gets a list with Address objects for the requested contact person.
        /// </summary>
        /// <param name="contactPersonId">The ID of the ContactPerson for whom the addresses should be returned.</param>
        /// <returns>A list with address objects when the database contains addresses for the contact person, or an empty list otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static AddressList GetList(int contactPersonId) {
            return AddressDB.GetList(contactPersonId);
        }

        /// <summary>
        /// Gets a single Address from the database by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the address in the database.</param>
        /// <returns>An Address object when the ID exists in the database, or <see langword="null"/> otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Address GetItem(int id) {
            return AddressDB.GetItem(id);
        }

        /// <summary>
        /// Saves an address in the database.
        /// </summary>
        /// <param name="myAddress">The Address instance to save.</param>
        /// <returns>The new ID if the Address is new in the database or the existing ID when an item was updated.</returns>
        [DataObjectMethod(DataObjectMethodType.Update | DataObjectMethodType.Insert, true)]
        public static int Save(Address myAddress) {
            //var vResults = new List<ValidationResult>();
            //var vc = new ValidationContext(instance: myAddress, serviceProvider: null, items: null);
            //var isValid = Validator.TryValidateObject(instance: vc.ObjectInstance,validationContext: vc,validationResults: vResults,validateAllProperties: true);

            //if (!isValid) {
            //    foreach (var validationResult in vResults) {
            //        yield return validationResult;
            //    }
            //}

            myAddress.Id = AddressDB.Save(myAddress);

            return myAddress.Id;
        }

        /// <summary>
        /// Deletes an address from the database.
        /// </summary>
        /// <param name="myAddress">The Address instance to delete.</param>
        /// <returns>Returns true when the object was deleted successfully, or false otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static bool Delete(Address myAddress) {
            return AddressDB.Delete(myAddress.Id);
        }

        #endregion

    }
}