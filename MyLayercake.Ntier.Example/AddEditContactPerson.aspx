<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditContactPerson.aspx.cs" Inherits="MyLayercake.Ntier.Example.AddEditContactPerson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Manage Contact Persons</title>
  <link href="Css/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <table>
        <tr>
          <td class="LabelCell">
            Name</td>
          <td>
            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valRequiredFirstName" ControlToValidate="txtFirstName" ErrorMessage="Please enter a first name." runat="server" />
          </td>
        </tr>
        <tr>
          <td class="LabelCell">
            Middle Name</td>
          <td>
            <asp:TextBox ID="txtMiddleName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
          <td class="LabelCell">
            Last Name</td>
          <td>
            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valRequiredLastName" ControlToValidate="txtLastName" ErrorMessage="Please enter a last name." runat="server" />
          </td>
        </tr>
        <tr>
          <td class="LabelCell">
            Date of Birth</td>
          <td>
            <asp:Calendar ID="calDateOfBirth" runat="server"></asp:Calendar>
            <asp:CustomValidator ID="valRequiredDateOfBirth" runat="server" ErrorMessage="Please select a date of birth." />
          </td>
        </tr>
        <tr>
          <td class="LabelCell">
            Type</td>
          <td>
            <asp:DropDownList ID="lstType" runat="server">
            </asp:DropDownList></td>
        </tr>
        <tr>
          <td class="LabelCell">
          </td>
          <td>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="btnCancel_Click" /></td>
        </tr>
      </table>
    </div>
  </form>
</body>
</html>

