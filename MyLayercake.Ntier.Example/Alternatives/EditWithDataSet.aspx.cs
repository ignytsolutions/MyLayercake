using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class Alternatives_EditWithDataSet : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack) {
            string sql = @"SELECT FirstName, LastName, MiddleName FROM ContactPerson WHERE Id = 1";
            using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NLayer"].ConnectionString)) {
                using (SqlCommand myCommand = new SqlCommand(sql, myConnection)) {
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader()) {
                        if (myReader.Read()) {
                            txtFirstName.Text = myReader.GetString(0);
                            txtLastName.Text = myReader.GetString(1);
                            if (!myReader.IsDBNull(2)) {
                                txtMiddleName.Text = myReader.GetString(2);
                            }
                        }
                        myReader.Close();
                    }
                    myConnection.Close();
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e) {
        string sqlBase = @"UPDATE ContactPerson SET FirstName='{0}', LastName='{1}', MiddleName ='{2}' WHERE Id = 1";
        using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NLayer"].ConnectionString)) {
            string sql = String.Format(sqlBase, txtFirstName.Text, txtLastName.Text, txtMiddleName.Text);
            SqlCommand myCommand = new SqlCommand(sql, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
