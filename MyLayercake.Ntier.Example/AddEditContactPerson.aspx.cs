using MyLayercake.NTier.Example.BusinessLayer;
using MyLayercake.NTier.Example.BusinessObjects;
using MyLayercake.NTier.Example.BusinessObjects.Enums;
using System;
using System.Reflection;
using System.Web.UI.WebControls;

public partial class AddEditContactPerson : System.Web.UI.Page {
    private int contactPersonId = -1;

    protected void Page_Load(object sender, EventArgs e) {
        if (Request.QueryString.Get("Id") != null) {
            contactPersonId = Convert.ToInt32(Request.QueryString.Get("Id"));
        }

        if (!Page.IsPostBack) {
            BindTypeDropDown();
            if (contactPersonId > 0) // Edit an existing item
            {
                // Get person
                ContactPerson myContactPerson = ContactPersonManager.GetItem(contactPersonId);
                if (myContactPerson != null) {
                    txtFirstName.Text = myContactPerson.FirstName;
                    txtMiddleName.Text = myContactPerson.MiddleName;
                    txtLastName.Text = myContactPerson.LastName;
                    calDateOfBirth.SelectedDate = myContactPerson.DateOfBirth;
                    if (lstType.Items.FindByText(myContactPerson.Type.ToString()) != null) {
                        lstType.Items.FindByText(myContactPerson.Type.ToString()).Selected = true;
                    }
                    this.Title = "Edit " + myContactPerson.FullName;
                }
            } else {
                this.Title = "Create new Contact Person";
            }
        }
    }

    private void BindTypeDropDown() {
        FieldInfo[] myEnumFields = typeof(PersonType).GetFields();
        foreach (FieldInfo myField in myEnumFields) {
            if (!myField.IsSpecialName && myField.Name.ToLower() != "notset") {
                int myValue = (int)myField.GetValue(0);
                lstType.Items.Add(new ListItem(myField.Name, myValue.ToString()));
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e) {
        Page.Validate();
        if (calDateOfBirth.SelectedDate == DateTime.MinValue) {
            valRequiredDateOfBirth.IsValid = false;
        }
        if (Page.IsValid) {
            ContactPerson myContactPerson;
            if (contactPersonId > 0) {
                // Update existing item
                myContactPerson = ContactPersonManager.GetItem(contactPersonId);
            } else {
                // Create a new ContactPerson
                myContactPerson = new ContactPerson();
            }
            myContactPerson.FirstName = txtFirstName.Text;
            myContactPerson.MiddleName = txtMiddleName.Text;
            myContactPerson.LastName = txtLastName.Text;
            myContactPerson.DateOfBirth = calDateOfBirth.SelectedDate;
            myContactPerson.Type = (PersonType)Convert.ToInt32(lstType.SelectedValue);
            ContactPersonManager.Save(myContactPerson);
            EndEditing();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e) {
        EndEditing();
    }

    private void EndEditing() {
        Response.Redirect("~/");
    }
}
