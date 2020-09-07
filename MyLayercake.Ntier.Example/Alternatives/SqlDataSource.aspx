<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SqlDataSource.aspx.cs" Inherits="MyLayercake.Ntier.Example.Alternatives.SqlDataSource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Using an SqlDataSource Control</title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" DataKeyNames="Id">
        <Columns>
          <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
      </asp:GridView>
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NLayer %>" DeleteCommand="DELETE FROM [ContactPerson] WHERE [Id] = @original_Id" InsertCommand="INSERT INTO [ContactPerson] ([FirstName], [MiddleName], [LastName], [DateOfBirth], [ContactPersonType]) VALUES (@FirstName, @MiddleName, @LastName, @DateOfBirth, @ContactPersonType)" SelectCommand="SELECT * FROM [ContactPerson]" UpdateCommand="UPDATE [ContactPerson] SET [FirstName] = @FirstName, [MiddleName] = @MiddleName, [LastName] = @LastName, [DateOfBirth] = @DateOfBirth, [ContactPersonType] = @ContactPersonType WHERE [Id] = @original_Id" OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
          <asp:Parameter Name="original_Id" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
          <asp:Parameter Name="FirstName" Type="String" />
          <asp:Parameter Name="MiddleName" Type="String" />
          <asp:Parameter Name="LastName" Type="String" />
          <asp:Parameter Name="DateOfBirth" Type="DateTime" />
          <asp:Parameter Name="ContactPersonType" Type="Int32" />
          <asp:Parameter Name="original_Id" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
          <asp:Parameter Name="FirstName" Type="String" />
          <asp:Parameter Name="MiddleName" Type="String" />
          <asp:Parameter Name="LastName" Type="String" />
          <asp:Parameter Name="DateOfBirth" Type="DateTime" />
          <asp:Parameter Name="ContactPersonType" Type="Int32" />
        </InsertParameters>
      </asp:SqlDataSource>
      <asp:FormView ID="FormView1" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSource1" DefaultMode="Insert">
        <EditItemTemplate>
          Id:
          <asp:Label ID="IdLabel1" runat="server" Text='<%# Eval("Id") %>'></asp:Label><br />
          FirstName:
          <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>'>
          </asp:TextBox><br />
          MiddleName:
          <asp:TextBox ID="MiddleNameTextBox" runat="server" Text='<%# Bind("MiddleName") %>'>
          </asp:TextBox><br />
          LastName:
          <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>'>
          </asp:TextBox><br />
          DateOfBirth:
          <asp:TextBox ID="DateOfBirthTextBox" runat="server" Text='<%# Bind("DateOfBirth") %>'>
          </asp:TextBox><br />
          ContactPersonType:
          <asp:TextBox ID="ContactPersonTypeTextBox" runat="server" Text='<%# Bind("ContactPersonType") %>'>
          </asp:TextBox><br />
          <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update">
          </asp:LinkButton>
          <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel">
          </asp:LinkButton>
        </EditItemTemplate>
        <InsertItemTemplate>
          FirstName:
          <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>'>
          </asp:TextBox><br />
          MiddleName:
          <asp:TextBox ID="MiddleNameTextBox" runat="server" Text='<%# Bind("MiddleName") %>'>
          </asp:TextBox><br />
          LastName:
          <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>'>
          </asp:TextBox><br />
          DateOfBirth:
          <asp:TextBox ID="DateOfBirthTextBox" runat="server" Text='<%# Bind("DateOfBirth") %>'>
          </asp:TextBox><br />
          ContactPersonType:
          <asp:TextBox ID="ContactPersonTypeTextBox" runat="server" Text='<%# Bind("ContactPersonType") %>'>
          </asp:TextBox><br />
          <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert">
          </asp:LinkButton>
          <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel">
          </asp:LinkButton>
        </InsertItemTemplate>
        <ItemTemplate>
          Id:
          <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>'></asp:Label><br />
          FirstName:
          <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label><br />
          MiddleName:
          <asp:Label ID="MiddleNameLabel" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label><br />
          LastName:
          <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("LastName") %>'></asp:Label><br />
          DateOfBirth:
          <asp:Label ID="DateOfBirthLabel" runat="server" Text='<%# Bind("DateOfBirth") %>'></asp:Label><br />
          ContactPersonType:
          <asp:Label ID="ContactPersonTypeLabel" runat="server" Text='<%# Bind("ContactPersonType") %>'></asp:Label><br />
          <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit">
          </asp:LinkButton>
          <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete">
          </asp:LinkButton>
          <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New">
          </asp:LinkButton>
        </ItemTemplate>
      </asp:FormView>
    </div>
  </form>
</body>
</html>
