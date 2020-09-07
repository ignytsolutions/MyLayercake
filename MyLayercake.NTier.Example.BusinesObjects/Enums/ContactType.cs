namespace MyLayercake.NTier.Example.BusinessObjects.Enums {
  /// <summary>
  /// Determines the type of a contact record.
  /// </summary>
  public enum ContactType {
        /// <summary>
        /// Indicates a business contact record.
        /// </summary>
        Business = 0,
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Personal = 1,
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        NotSet = -1
    }
}