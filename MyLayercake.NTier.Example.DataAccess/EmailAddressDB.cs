using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.BusinessObjects.Collections;
using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MyLayercake.NTier.Example.DataAccess {
    /// <summary>
    /// The EmailAddressDB class is responsible for interacting with the database to retrieve and store information
    /// about EmailAddress objects.
    /// </summary>
    public class EmailAddressDB {

        #region Public Methods

        /// <summary>
        /// Gets an instance of EmailAddress from the underlying datasource.
        /// </summary>
        /// <param name="id">The unique ID of the EmailAddress in the database.</param>
        /// <returns>An EmailAddress when the ID was found in the database, or null otherwise.</returns>
        public static EmailAddress GetItem(int id) {
            EmailAddress myEmailAddress = null;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocEmailAddressSelectSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);

                myConnection.Open();

                using (SqlDataReader myReader = myCommand.ExecuteReader()) {
                    if (myReader.Read()) {
                        myEmailAddress = FillDataRecord(myReader);
                    }
                    myReader.Close();
                }

                myConnection.Close();
            }

            return myEmailAddress;
        }

        /// <summary>
        /// Returns a list with EmailAddress objects.
        /// </summary>
        /// <param name="contactPersonId">The ID of the ContactPerson for whom the e-mail addresses should be returned.</param>
        /// <returns>
        /// A generics List with the EmailAddress objects.
        /// </returns>
        public static EmailAddressList GetList(int contactPersonId) {
            EmailAddressList tempList = null;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocEmailAddressSelectList", myConnection);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@contactPersonId", contactPersonId);
                myConnection.Open();

                using (SqlDataReader myReader = myCommand.ExecuteReader()) {
                    if (myReader.HasRows) {
                        tempList = new EmailAddressList();
                        while (myReader.Read()) {
                            tempList.Add(FillDataRecord(myReader));
                        }
                    }

                    myReader.Close();
                }
            }

            return tempList;
        }

        /// <summary>
        /// Saves an instance of the <see cref="EmailAddress" /> in the database.
        /// </summary>
        /// <param name="myEmailAddress">The EmailAddress instance to save.</param>
        /// <returns>Returns true when the object was saved successfully, or false otherwise.</returns>
        public static int Save(EmailAddress myEmailAddress) {
            int result = 0;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocEmailAddressInsertUpdateSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                if (myEmailAddress.Id == -1) {
                    myCommand.Parameters.AddWithValue("@id", DBNull.Value);
                } else {
                    myCommand.Parameters.AddWithValue("@id", myEmailAddress.Id);
                }

                myCommand.Parameters.AddWithValue("@email", myEmailAddress.Email);
                myCommand.Parameters.AddWithValue("@emailType", myEmailAddress.Type);
                myCommand.Parameters.AddWithValue("@contactPersonId", myEmailAddress.ContactPersonId);

                DbParameter returnValue;

                returnValue = myCommand.CreateParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                myCommand.Parameters.Add(returnValue);

                myConnection.Open();
                myCommand.ExecuteNonQuery();

                result = Convert.ToInt32(returnValue.Value);

                myConnection.Close();
            }

            return result;
        }

        /// <summary>
        /// Deletes an e-mail address from the database.
        /// </summary>
        /// <param name="id">The ID of the EmailAddress to delete.</param>
        /// <returns>Returns true when the object was deleted successfully, or false otherwise.</returns>
        public static bool Delete(int id) {
            int result = 0;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocEmailAddressDeleteSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();

                myConnection.Close();
            }

            return result > 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes a new instance of the EmailAddress class and fills it with the data fom the IDataRecord.
        /// </summary>
        private static EmailAddress FillDataRecord(IDataRecord myDataRecord) {
            EmailAddress myEmailAddress = new EmailAddress();

            myEmailAddress.Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Id"));
            myEmailAddress.Email = myDataRecord.GetString(myDataRecord.GetOrdinal("Email"));
            myEmailAddress.Type = (ContactType)myDataRecord.GetInt32(myDataRecord.GetOrdinal("EmailType"));
            myEmailAddress.ContactPersonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ContactPersonId"));

            return myEmailAddress;
        }

        #endregion

    }
}