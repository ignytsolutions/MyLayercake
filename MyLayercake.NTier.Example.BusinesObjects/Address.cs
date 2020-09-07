using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MyLayercake.NTier.Example.BusinessObjects {
    /// <summary>
    /// The Address class represents an address that belongs to a <see cref="ContactPerson"> contact person</see>.
    /// </summary>
    [
      DebuggerDisplay("Address: {Street, nq} {HouseNumber, nq} {City, nq} - {Country, nq} ({Type, nq})")
    ]
    public class Address {

        #region Private Variables

        private int id = -1;
        private string street = String.Empty;
        private string houseNumber = String.Empty;
        private string zipCode = String.Empty;
        private string city = String.Empty;
        private string country = String.Empty;
        private ContactType type = ContactType.NotSet;
        private int contactPersonId = -1;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the unique ID of the address.
        /// </summary>
        [DataObjectFieldAttribute(true, true, false)]
        public int Id {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        /// <summary>
        /// Gets or sets the Street of the address.
        /// </summary>
        public string Street {
            get {
                return street;
            }
            set {
                street = value;
            }
        }

        /// <summary>
        /// Gets or sets the HouseNumber of the address.
        /// </summary>
        public string HouseNumber {
            get {
                return houseNumber;
            }
            set {
                houseNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets the ZipCode of the address.
        /// </summary>
        public string ZipCode {
            get {
                return zipCode;
            }
            set {
                zipCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the City of the address.
        /// </summary>
        public string City {
            get {
                return city;
            }
            set {
                city = value;
            }
        }

        /// <summary>
        /// Gets or sets the Country of the address.
        /// </summary>
        public string Country {
            get {
                return country;
            }
            set {
                country = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the address, like business or personal.
        /// </summary>
        public ContactType Type {
            get {
                return type;
            }
            set {
                type = value;
            }
        }

        /// <summary>
        /// Gets or sets the ID of the owner (a <see cref="ContactPerson"/>) of the address.
        /// </summary>
        public int ContactPersonId {
            get {
                return contactPersonId;
            }
            set {
                contactPersonId = value;
            }
        }

        #endregion

    }
}