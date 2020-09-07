<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditWithDataSet.aspx.cs" Inherits="Alternatives_EditWithDataSet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Editing With a DataSet</title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <table>
        <tr>
          <td>
            <asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Label ID="Label3" runat="server" Text="Middle Name"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtMiddleName" runat="server"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>
          </td>
          <td>
            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td>
          </td>
          <td>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
          </td>
        </tr>
      </table>
    </div>
  </form>
</body>
</html>
