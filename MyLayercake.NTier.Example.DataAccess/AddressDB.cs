using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.BusinessObjects.Collections;
using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MyLayercake.NTier.Example.DataAccess {
    /// <summary>
    /// The AddressDB class is responsible for interacting with the database to retrieve and store information
    /// about Address objects.
    /// </summary>
    public class AddressDB {

        #region Public Methods

        /// <summary>
        /// Gets an instance of Address from the underlying datasource.
        /// </summary>
        /// <param name="id">The unique ID of the Address in the database.</param>
        /// <returns>An Address when the ID was found in the database, or null otherwise.</returns>
        public static Address GetItem(int id) {
            Address myAddress = null;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocAddressSelectSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);

                myConnection.Open();

                using (SqlDataReader myReader = myCommand.ExecuteReader()) {
                    if (myReader.Read()) {
                        myAddress = FillDataRecord(myReader);
                    }
                    myReader.Close();
                }

                myConnection.Close();
            }

            return myAddress;
        }

        /// <summary>
        /// Returns a list with Address objects.
        /// </summary>
        /// <param name="contactPersonId">The ID of the ContactPerson for whom the addresses should be returned.</param>
        /// <returns>
        /// A generics List with the Address objects.
        /// </returns>
        public static AddressList GetList(int contactPersonId) {
            AddressList tempList = null;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocAddressSelectList", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@contactPersonId", contactPersonId);
                myConnection.Open();
                using (SqlDataReader myReader = myCommand.ExecuteReader()) {
                    if (myReader.HasRows) {
                        tempList = new AddressList();
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
        /// Saves an address in the database.
        /// </summary>
        /// <param name="myAddress">The Address instance to save.</param>
        /// <returns>The new ID if the Address is new in the database or the existing ID when an item was updated.</returns>
        public static int Save(Address myAddress) {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocAddressInsertUpdateSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                if (myAddress.Id == -1) {
                    myCommand.Parameters.AddWithValue("@id", DBNull.Value);
                } else {
                    myCommand.Parameters.AddWithValue("@id", myAddress.Id);
                }

                myCommand.Parameters.AddWithValue("@street", myAddress.Street);
                myCommand.Parameters.AddWithValue("@houseNumber", myAddress.HouseNumber);
                if (String.IsNullOrEmpty(myAddress.ZipCode)) {
                    myCommand.Parameters.AddWithValue("@zipCode", DBNull.Value);
                } else {
                    myCommand.Parameters.AddWithValue("@zipCode", myAddress.ZipCode);
                }
                myCommand.Parameters.AddWithValue("@city", myAddress.City);
                myCommand.Parameters.AddWithValue("@country", myAddress.Country);
                myCommand.Parameters.AddWithValue("@addressType", myAddress.Type);
                myCommand.Parameters.AddWithValue("@contactPersonId", myAddress.ContactPersonId);

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
        /// Deletes an address from the database.
        /// </summary>
        /// <param name="id">The ID of the Address to delete.</param>
        /// <returns>Returns true when the object was deleted successfully, or false otherwise.</returns>
        public static bool Delete(int id) {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocAddressDeleteSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result > 0;
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Initializes a new instance of the Address class and fills it with the data fom the IDataRecord.
        /// </summary>
        private static Address FillDataRecord(IDataRecord myDataRecord) {
            Address myAddress = new Address();

            myAddress.Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Id"));
            myAddress.Street = myDataRecord.GetString(myDataRecord.GetOrdinal("Street"));
            myAddress.HouseNumber = myDataRecord.GetString(myDataRecord.GetOrdinal("HouseNumber"));

            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ZipCode"))) {
                myAddress.ZipCode = myDataRecord.GetString(myDataRecord.GetOrdinal("ZipCode"));
            }

            myAddress.City = myDataRecord.GetString(myDataRecord.GetOrdinal("City"));
            myAddress.Country = myDataRecord.GetString(myDataRecord.GetOrdinal("Country"));
            myAddress.Type = (ContactType)myDataRecord.GetInt32(myDataRecord.GetOrdinal("AddressType"));
            myAddress.ContactPersonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ContactPersonId"));

            return myAddress;
        }

        #endregion
    }
}