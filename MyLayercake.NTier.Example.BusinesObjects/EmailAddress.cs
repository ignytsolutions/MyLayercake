using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MyLayercake.NTier.Example.BusinessObjects {
    /// <summary>
    /// The EmailAddress class represents an e-mail address that belongs to a <see cref="ContactPerson"> contact person</see>.
    /// </summary>
    [
      DebuggerDisplay("EmailAddress: {Email, nq} ({Type, nq})")
    ]
    public class EmailAddress {

        #region Private Variables

        private int id = -1;
        private string email = String.Empty;
        private ContactType type = ContactType.NotSet;
        private int contactPersonId = -1;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the unique ID of the e-mail address.
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
        /// Gets or sets the actual e-mail address text of the e-mail address.
        /// </summary>
        public string Email {
            get {
                return email;
            }
            set {
                email = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the e-mail address, like business or personal.
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
        /// Gets or sets the ID of the owner (a <see cref="ContactPerson"/>) of the e-mail address.
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