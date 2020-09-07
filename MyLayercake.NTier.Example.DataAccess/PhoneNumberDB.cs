using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.BusinessObjects.Collections;
using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MyLayercake.NTier.Example.DataAccess {
    /// <summary>
    /// The PhoneNumberDB class is responsible for interacting with the database to retrieve and store information
    /// about PhoneNumber objects.
    /// </summary>
    public class PhoneNumberDB {

        #region Public Methods

        /// <summary>
        /// Gets an instance of PhoneNumber from the underlying datasource.
        /// </summary>
        /// <param name="id">The unique ID of the PhoneNumber in the database.</param>
        /// <returns>A PhoneNumber when the ID was found in the database, or null otherwise.</returns>
        public static PhoneNumber GetItem(int id) {
            PhoneNumber myPhoneNumber = null;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocPhoneNumberSelectSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);

                myConnection.Open();
                using (SqlDataReader myReader = myCommand.ExecuteReader()) {
                    if (myReader.Read()) {
                        myPhoneNumber = FillDataRecord(myReader);
                    }
                    myReader.Close();
                }
                myConnection.Close();
            }
            return myPhoneNumber;
        }

        /// <summary>
        /// Returns a list with PhoneNumber objects.
        /// </summary>
        /// <param name="contactPersonId">The ID of the ContactPerson for whom the phone numbers should be returned.</param>
        /// <returns>
        /// A generics List with the PhoneNumber objects.
        /// </returns>
        public static PhoneNumberList GetList(int contactPersonId) {
            PhoneNumberList tempList = null;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocPhoneNumberSelectList", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@contactPersonId", contactPersonId);
                myConnection.Open();

                using (SqlDataReader myReader = myCommand.ExecuteReader()) {
                    if (myReader.HasRows) {
                        tempList = new PhoneNumberList();
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
        /// Saves an instance of the <see cref="PhoneNumber" /> in the database.
        /// </summary>
        /// <param name="myPhoneNumber">The PhoneNumber instance to save.</param>
        /// <returns>Returns true when the object was saved successfully, or false otherwise.</returns>
        public static int Save(PhoneNumber myPhoneNumber) {
            int result = 0;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocPhoneNumberInsertUpdateSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                if (myPhoneNumber.Id == -1) {
                    myCommand.Parameters.AddWithValue("@id", DBNull.Value);
                } else {
                    myCommand.Parameters.AddWithValue("@id", myPhoneNumber.Id);
                }

                myCommand.Parameters.AddWithValue("@phoneNumber", myPhoneNumber.Number);
                myCommand.Parameters.AddWithValue("@phoneNumberType", myPhoneNumber.Type);
                myCommand.Parameters.AddWithValue("@contactPersonId", myPhoneNumber.ContactPersonId);
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
        /// Deletes a phone number from the database.
        /// </summary>
        /// <param name="id">The ID of the PhoneNumber to delete.</param>
        /// <returns>Returns true when the object was deleted successfully, or false otherwise.</returns>
        public static bool Delete(int id) {
            int result = 0;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocPhoneNumberDeleteSingleItem", myConnection);
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
        /// Initializes a new instance of the PhoneNumber class and fills it with the data fom the IDataRecord.
        /// </summary>
        private static PhoneNumber FillDataRecord(IDataRecord myDataRecord) {
            PhoneNumber myPhoneNumber = new PhoneNumber();

            myPhoneNumber.Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Id"));
            myPhoneNumber.Number = myDataRecord.GetString(myDataRecord.GetOrdinal("PhoneNumber"));
            myPhoneNumber.Type = (ContactType)myDataRecord.GetInt32(myDataRecord.GetOrdinal("PhoneNumberType"));
            myPhoneNumber.ContactPersonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ContactPersonId"));

            return myPhoneNumber;
        }

        #endregion
    }
}