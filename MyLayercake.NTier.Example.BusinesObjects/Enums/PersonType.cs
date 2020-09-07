namespace MyLayercake.NTier.Example.BusinessObjects.Enums {
    /// <summary>
    /// Determines the type of a contact person.
    /// </summary>
    public enum PersonType {
        /// <summary>
        /// Indicates a friend.
        /// </summary>
        Friend = 0,
        /// <summary>
        /// Indicates a family member.
        /// </summary>
        Family = 1,
        /// <summary>
        /// Indicates a co worker.
        /// </summary>
        Colleague = 2,
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        NotSet = -1
    }
}