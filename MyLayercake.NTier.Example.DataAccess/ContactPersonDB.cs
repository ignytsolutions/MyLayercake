using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MyLayercake.NTier.Example.DataAccess {
    /// <summary>
    /// The ContactPersonDB class is responsible for interacting with the database to retrieve and store information
    /// about ContactPerson objects.
    /// </summary>
    public class ContactPersonDB {

        #region Public Methods

        /// <summary>
        /// Gets an instance of ContactPerson from the underlying datasource.
        /// </summary>
        /// <param name="id">The unique ID of the ContactPerson in the database.</param>
        /// <returns>An ContactPerson when the ID was found in the database, or null otherwise.</returns>
        public static ContactPerson GetItem(int id) {
            ContactPerson myContactPerson = null;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocContactPersonSelectSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);

                myConnection.Open();

                using (SqlDataReader myReader = myCommand.ExecuteReader()) {
                    if (myReader.Read()) {
                        myContactPerson = FillDataRecord(myReader);
                    }

                    myReader.Close();
                }

                myConnection.Close();
            }

            return myContactPerson;
        }

        /// <summary>
        /// Returns a list with ContactPerson objects.
        /// </summary>
        /// <returns>A generics List with the ContactPerson objects.</returns>
        public static ContactPersonList GetList() {
            ContactPersonList tempList = null;

            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocContactPersonSelectList", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                myConnection.Open();

                using (SqlDataReader myReader = myCommand.ExecuteReader()) {
                    if (myReader.HasRows) {
                        tempList = new ContactPersonList();

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
        /// Saves a contact person in the database.
        /// </summary>
        /// <param name="myContactPerson">The ContactPerson instance to save.</param>
        /// <returns>The new ID if the ContactPerson is new in the database or the existing ID when an item was updated.</returns>
        public static int Save(ContactPerson myContactPerson) {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocContactPersonInsertUpdateSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                if (myContactPerson.Id == -1) {
                    myCommand.Parameters.AddWithValue("@id", DBNull.Value);
                } else {
                    myCommand.Parameters.AddWithValue("@id", myContactPerson.Id);
                }

                myCommand.Parameters.AddWithValue("@firstName", myContactPerson.FirstName);
                myCommand.Parameters.AddWithValue("@lastName", myContactPerson.LastName);

                if (String.IsNullOrEmpty(myContactPerson.MiddleName)) {
                    myCommand.Parameters.AddWithValue("@middleName", DBNull.Value);
                } else {
                    myCommand.Parameters.AddWithValue("@middleName", myContactPerson.MiddleName);
                }

                myCommand.Parameters.AddWithValue("@dateOfBirth", myContactPerson.DateOfBirth);
                myCommand.Parameters.AddWithValue("@contactpersonType", myContactPerson.Type);

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
        /// Deletes a contact person from the database.
        /// </summary>
        /// <param name="id">The ID of the contact person to delete.</param>
        /// <returns>Returns true when the object was deleted successfully, or false otherwise.</returns>
        public static bool Delete(int id) {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString)) {
                SqlCommand myCommand = new SqlCommand("sprocContactPersonDeleteSingleItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result > 0;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the ContactPerson class and fills it with the data fom the IDataRecord.
        /// </summary>
        private static ContactPerson FillDataRecord(IDataRecord myDataRecord) {
            ContactPerson myContactPerson = new ContactPerson();

            myContactPerson.Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Id"));
            myContactPerson.FirstName = myDataRecord.GetString(myDataRecord.GetOrdinal("FirstName"));

            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("MiddleName"))) {
                myContactPerson.MiddleName = myDataRecord.GetString(myDataRecord.GetOrdinal("MiddleName"));
            }

            myContactPerson.LastName = myDataRecord.GetString(myDataRecord.GetOrdinal("LastName"));

            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DateOfBirth"))) {
                myContactPerson.DateOfBirth = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("DateOfBirth"));
            }

            myContactPerson.Type = (PersonType)myDataRecord.GetInt32(myDataRecord.GetOrdinal("ContactpersonType"));

            return myContactPerson;
        }
    }
}