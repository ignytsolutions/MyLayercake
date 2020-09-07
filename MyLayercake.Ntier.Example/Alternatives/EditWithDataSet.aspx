<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWithDataSet.aspx.cs" Inherits="MyLayercake.Ntier.Example.Alternatives.EditWithDataSet" %>

<!DOCTYPE html>

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

