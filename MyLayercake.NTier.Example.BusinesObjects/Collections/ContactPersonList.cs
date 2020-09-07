using System;
using System.Collections.Generic;

namespace MyLayercake.NTier.Example.BusinessObjects {
    /// <summary>
    /// The ContactPersonList class is designed to work with lists of instances of ContactPerson.
    /// </summary>
    /// 
    [Serializable]
    public class ContactPersonList : List<ContactPerson> {
        public ContactPersonList() { }
    }
}
