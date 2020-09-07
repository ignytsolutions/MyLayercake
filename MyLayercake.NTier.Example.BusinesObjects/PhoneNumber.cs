using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MyLayercake.NTier.Example.BusinessObjects {
  /// <summary>
  /// The PhoneNumber class represents a phone number that belongs to a <see cref="ContactPerson"> contact person</see>.
  /// </summary>
  [
    DebuggerDisplay("PhoneNumber: {Number, nq} ({Type, nq})")
  ]
  public class PhoneNumber
  {

    #region Private Variables

    private int id = -1;
    private string number = String.Empty;
    private ContactType type = ContactType.NotSet;
    private int contactPersonId = -1;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the unique ID of the phone number.
    /// </summary>
    [DataObjectFieldAttribute(true, true, false)]
    public int Id
    {
      get
      {
        return id;
      }
      set
      {
        id = value;
      }
    }

    /// <summary>
    /// Gets or sets the actual number of the phone number.
    /// </summary>
    public string Number
    {
      get
      {
        return number;
      }
      set
      {
        number = value;
      }
    }

    /// <summary>
    /// Gets or sets the type of the phone number, like business or personal.
    /// </summary>
    public ContactType Type
    {
      get
      {
        return type;
      }
      set
      {
        type = value;
      }
    }

    /// <summary>
    /// Gets or sets the ID of the owner (a <see cref="ContactPerson"/>) of the phone number.
    /// </summary>
    public int ContactPersonId
    {
      get
      {
        return contactPersonId;
      }
      set
      {
        contactPersonId = value;
      }
    }

    #endregion

  }
}