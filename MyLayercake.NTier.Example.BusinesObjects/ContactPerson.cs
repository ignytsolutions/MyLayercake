using MyLayercake.NTier.Example.BusinessObjects.Collections;
using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MyLayercake.NTier.Example.BusinessObjects {
    /// <summary>
    /// The ContactPerson class represents contact persons in the Contact Person Manager application.
    /// </summary>
    [
      DebuggerDisplay("Person: {FullName, nq} Type: ({Type, nq})")
    ]
    public class ContactPerson {
        #region Private Variables

        private int id = -1;
        private string firstName = String.Empty;
        private string middleName = String.Empty;
        private string lastName = String.Empty;
        private DateTime dateOfBirth = DateTime.MinValue;
        private PersonType type = PersonType.NotSet;

        private AddressList addresses = new AddressList();
        private PhoneNumberList phoneNumbers = new PhoneNumberList();
        private EmailAddressList emailAddresses = new EmailAddressList();

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the unique ID of the contact person.
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
        /// Gets or sets the first name of the contact person.
        /// </summary>
        public string FirstName {
            get {
                return firstName;
            }
            set {
                firstName = value;
            }
        }

        /// <summary>
        /// Gets or sets the middle name of the contact person.
        /// </summary>
        public string MiddleName {
            get {
                return middleName;
            }
            set {
                middleName = value;
            }
        }

        /// <summary>
        /// Gets or sets the last name of the contact person.
        /// </summary>
        public string LastName {
            get {
                return lastName;
            }
            set {
                lastName = value;
            }
        }

        /// <summary>
        /// Gets or sets the date of birth of the contact person.
        /// </summary>
        public DateTime DateOfBirth {
            get {
                return dateOfBirth;
            }
            set {
                dateOfBirth = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the contact person.
        /// </summary>
        public PersonType Type {
            get {
                return type;
            }
            set {
                type = value;
            }
        }

        /// <summary>
        /// Gets the full name of the contact person which is a combination of
        /// <see cref="FirstName" />, <see cref="MiddleName" /> and <see cref="LastName" />.
        /// </summary>
        public string FullName {
            get {
                string tempValue = firstName;
                if (!String.IsNullOrEmpty(middleName)) {
                    tempValue += " " + middleName;
                }
                tempValue += " " + lastName;
                return tempValue;
            }
        }

        /// <summary>
        /// Gets or sets a collection of <see cref="Address" /> instances for the contact person.
        /// </summary>
        public AddressList Addresses {
            get {
                return addresses;
            }
            set {
                addresses = value;
            }
        }

        /// <summary>
        /// Gets or sets a collection of <see cref="PhoneNumber" /> instances for the contact person.
        /// </summary>
        public PhoneNumberList PhoneNumbers {
            get {
                return phoneNumbers;
            }
            set {
                phoneNumbers = value;
            }
        }

        /// <summary>
        /// Gets or sets a collection of <see cref="EmailAddress" /> instances for the contact person.
        /// </summary>
        public EmailAddressList EmailAddresses {
            get {
                return emailAddresses;
            }
            set {
                emailAddresses = value;
            }
        }
        #endregion

    }
}