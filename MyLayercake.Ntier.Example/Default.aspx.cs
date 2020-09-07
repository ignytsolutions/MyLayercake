using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyLayercake.Ntier.Example {
    public partial class Default : System.Web.UI.Page {
        protected void gvContactPersons_RowCommand(object sender, GridViewCommandEventArgs e) {
            switch (e.CommandName.ToLower()) {
                case "addresses":
                    gvContactPersons.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                    MultiView1.ActiveViewIndex = 0;
                    break;
                case "emailaddresses":
                    gvContactPersons.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                    MultiView1.ActiveViewIndex = 1;
                    break;
                case "phonenumbers":
                    gvContactPersons.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                    MultiView1.ActiveViewIndex = 2;
                    break;
                case "edit":
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    int contactPersonId = Convert.ToInt32(gvContactPersons.DataKeys[rowIndex].Value);
                    Response.Redirect(String.Format("AddEditContactPerson.aspx?Id={0}", contactPersonId.ToString()));
                    break;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e) {
            Response.Redirect("AddEditContactPerson.aspx");
        }

        protected void fv_ItemInserting(object sender, FormViewInsertEventArgs e) {
            // Assign the selected contact person's ID to the ContactPersonId key in the dictionary
            e.Values["ContactPersonId"] = Convert.ToInt32(gvContactPersons.SelectedDataKey.Value);
        }

        #region Addresses

        protected void btnListAddresses_Click(object sender, EventArgs e) {
            ShowAddressList();
        }
        protected void btnNewAddress_Click(object sender, EventArgs e) {
            ShowAddressInsert();
        }

        private void ShowAddressList() {
            gvAddresses.Visible = true;
            fvAddress.Visible = false;
        }

        private void ShowAddressInsert() {
            gvAddresses.Visible = false;
            fvAddress.Visible = true;
        }

        protected void fvAddress_ItemCommand(object sender, FormViewCommandEventArgs e) {
            switch (e.CommandName.ToLower()) {
                case "cancel":
                    ShowAddressList();
                    break;
            }
        }

        protected void fvAddress_ItemInserted(object sender, FormViewInsertedEventArgs e) {
            ShowAddressList();
        }

        #endregion

        #region E-mail Addresses

        protected void btnListEmailAddresses_Click(object sender, EventArgs e) {
            ShowEmailAddressList();
        }
        protected void btnNewEmailAddress_Click(object sender, EventArgs e) {
            ShowEmailAddressInsert();
        }

        private void ShowEmailAddressList() {
            gvEmailAddresses.Visible = true;
            fvEmailAddress.Visible = false;
        }

        private void ShowEmailAddressInsert() {
            gvEmailAddresses.Visible = false;
            fvEmailAddress.Visible = true;
        }

        protected void fvEmailAddress_ItemCommand(object sender, FormViewCommandEventArgs e) {
            switch (e.CommandName.ToLower()) {
                case "cancel":
                    ShowEmailAddressList();
                    break;
            }
        }

        protected void fvEmailAddress_ItemInserted(object sender, FormViewInsertedEventArgs e) {
            ShowEmailAddressList();
        }


        #endregion

        #region Phone Numbers

        protected void btnListPhoneNumbers_Click(object sender, EventArgs e) {
            ShowPhoneNumberList();
        }
        protected void btnNewPhoneNumber_Click(object sender, EventArgs e) {
            ShowPhoneNumberInsert();
        }

        private void ShowPhoneNumberList() {
            gvPhoneNumbers.Visible = true;
            fvPhoneNumber.Visible = false;
        }

        private void ShowPhoneNumberInsert() {
            gvPhoneNumbers.Visible = false;
            fvPhoneNumber.Visible = true;
        }

        protected void fvPhoneNumber_ItemCommand(object sender, FormViewCommandEventArgs e) {
            switch (e.CommandName.ToLower()) {
                case "cancel":
                    ShowPhoneNumberList();
                    break;
            }
        }

        protected void fvPhoneNumber_ItemInserted(object sender, FormViewInsertedEventArgs e) {
            ShowPhoneNumberList();
        }


        #endregion

        public string[] GetContactTypes() {
            // Code based on http://www.codeproject.com/cs/miscctrl/enumedit.asp
            List<string> myList = new List<string>();
            FieldInfo[] myEnumFields = typeof(ContactType).GetFields();

            foreach (FieldInfo myField in myEnumFields) {
                if (!myField.IsSpecialName && myField.Name.ToLower() != "notset") {
                    myList.Add(myField.Name);
                }
            }
            return myList.ToArray();
        }

        private string ToCSVString(DataTable Datatable) {
            if (Datatable == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            string[] columnNames = Datatable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in Datatable.Rows) {
                string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                ToArray();
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }

        protected void btnExportToCsv_Click(object sender, EventArgs e) {
            Page.Response.ClearContent();
            Page.Response.Clear();
            Page.Response.ContentType = "text/plain";
            Page.Response.AddHeader("Content-Disposition", "attachment; filename=DownloadedData.csv;");
            Page.Response.Write(ToCSVString(null));
            Page.Response.Flush();
            Page.Response.End();
        }
    }
}