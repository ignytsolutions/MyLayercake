<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyLayercake.Ntier.Example.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Contact Person Manager</title>
  <link href="Css/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="Create new Contact Person" /><br />
      <br />
        <asp:Button ID="btnExportToCsv" runat="server" OnClick="btnExportToCsv_Click" Text="Export To CSV" /><br />
      <br />
      <asp:GridView ID="gvContactPersons" runat="server" AutoGenerateColumns="False" DataSourceID="odsContactPersons" DataKeyNames="Id" OnRowCommand="gvContactPersons_RowCommand" AllowPaging="True" CellPadding="4" GridLines="None">
        <Columns>
          <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
          <asp:BoundField DataField="FullName" HeaderText="Full Name" ReadOnly="True" SortExpression="FullName" />
          <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth" SortExpression="DateOfBirth" DataFormatString="{0:d}" HtmlEncode="False" />
          <asp:BoundField DataField="Type" HeaderText="Type" />
          <asp:ButtonField CommandName="Addresses" Text="Addresses" />
          <asp:ButtonField CommandName="EmailAddresses" Text="Email" />
          <asp:ButtonField CommandName="PhoneNumbers" Text="Phonenumbers" />
          <asp:ButtonField CommandName="Edit" Text="Edit" />
          <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
              <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this contact person?');"></asp:LinkButton>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
      <br />
      <br />
      <asp:ObjectDataSource ID="odsContactPersons" runat="server" DataObjectTypeName="MyLayercake.NTier.Example.BusinessObjects.ContactPerson" DeleteMethod="Delete" InsertMethod="Save" SelectMethod="GetList" TypeName="MyLayercake.NTier.Example.BusinessLayer.ContactPersonManager" UpdateMethod="Save"></asp:ObjectDataSource>
      <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
          <fieldset>
            <legend>Addresses</legend>
            <asp:Button ID="btnNewAddress" runat="server" Text="Create new Address" OnClick="btnNewAddress_Click" />
            <asp:Button ID="btnListAddresses" runat="server" Text="Show Addresses" OnClick="btnListAddresses_Click" />
            <asp:GridView ID="gvAddresses" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="odsAddresses" AllowPaging="True">
              <Columns>
                <asp:BoundField DataField="ContactPersonId" ReadOnly="True" HeaderText="Contact Person Id" />
                <asp:BoundField DataField="Street" HeaderText="Street" SortExpression="Street" />
                <asp:BoundField DataField="HouseNumber" HeaderText="House Number" />
                <asp:BoundField DataField="ZipCode" HeaderText="ZipCode" SortExpression="ZipCode" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:BoundField DataField="Country" HeaderText="Country" />
                <asp:TemplateField HeaderText="Type" SortExpression="Type">
                  <EditItemTemplate>
                    <asp:DropDownList ID="lstType" runat="server" DataSource="<%# GetContactTypes() %>" SelectedValue='<%#Bind("Type") %>' />
                  </EditItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this contact record?');"></asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
            <asp:FormView ID="fvAddress" runat="server" DataKeyNames="Id" DataSourceID="odsAddresses" DefaultMode="Insert" OnItemInserting="fv_ItemInserting" Visible="false" OnItemCommand="fvAddress_ItemCommand" OnItemInserted="fvAddress_ItemInserted" EnableViewState="False">
              <InsertItemTemplate>
                <span class="Label">Street:</span>
                <asp:TextBox ID="StreetTextBox" runat="server" Text='<%# Bind("Street") %>' />
                <br />
                <span class="Label">HouseNumber:</span>
                <asp:TextBox ID="HouseNumberTextBox" runat="server" Text='<%# Bind("HouseNumber") %>' />
                <br />
                <span class="Label">ZipCode:</span>
                <asp:TextBox ID="ZipCodeTextBox" runat="server" Text='<%# Bind("ZipCode") %>' />
                <br />
                <span class="Label">City:</span>
                <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Bind("City") %>' />
                <br />
                <span class="Label">Country:</span>
                <asp:TextBox ID="CountryTextBox" runat="server" Text='<%# Bind("Country") %>' />
                <br />
                <span class="Label">Type:</span>
                <asp:DropDownList ID="lstType" runat="server" DataSource="<%# GetContactTypes() %>" SelectedValue='<%#Bind("Type") %>' />
                <br />
                <br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert">
                </asp:LinkButton>
                <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel">
                </asp:LinkButton>
              </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsAddresses" runat="server" DataObjectTypeName="MyLayercake.NTier.Example.BusinessObjects.Address" DeleteMethod="Delete" InsertMethod="Save" SelectMethod="GetList" TypeName="MyLayercake.NTier.Example.BusinessLayer.AddressManager" UpdateMethod="Save">
              <SelectParameters>
                <asp:ControlParameter ControlID="gvContactPersons" Name="contactPersonId" PropertyName="SelectedValue" Type="Int32" />
              </SelectParameters>
            </asp:ObjectDataSource>
          </fieldset>
        </asp:View>
        <asp:View ID="View2" runat="server">
          <fieldset>
            <legend>E-mail addresses</legend>
            <asp:Button ID="btnNewEmailAddress" runat="server" Text="Create new Email Address" OnClick="btnNewEmailAddress_Click" />
            <asp:Button ID="btnListEmailAddress" runat="server" Text="Show E-mail Addresses" OnClick="btnListEmailAddresses_Click" /><br />
            <asp:GridView ID="gvEmailAddresses" runat="server" AutoGenerateColumns="False" DataSourceID="odsEmailAddresses" AllowPaging="True" DataKeyNames="Id">
              <Columns>
                <asp:BoundField DataField="ContactPersonId" HeaderText="Contact Person Id" SortExpression="ContactPersonId" ReadOnly="True" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:TemplateField HeaderText="Type" SortExpression="Type">
                  <EditItemTemplate>
                    <asp:DropDownList ID="lstType" runat="server" DataSource="<%#GetContactTypes() %>" SelectedValue='<%#Bind("Type") %>' />
                  </EditItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this contact record?');"></asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
            <asp:FormView ID="fvEmailAddress" runat="server" DataKeyNames="Id" DataSourceID="odsEmailAddresses" OnItemInserting="fv_ItemInserting" DefaultMode="Insert" Visible="false" OnItemCommand="fvEmailAddress_ItemCommand" OnItemInserted="fvEmailAddress_ItemInserted" EnableViewState="False">
              <InsertItemTemplate>
                <span class="Label">Email:</span>
                <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' /><br />
                <span class="Label">Type:</span>
                <asp:DropDownList ID="lstType" runat="server" DataSource='<%#GetContactTypes() %>' SelectedValue='<%#Bind("Type") %>' />
                <br />
                <br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert"></asp:LinkButton>
                <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
              </InsertItemTemplate>
            </asp:FormView>
          </fieldset>
          <asp:ObjectDataSource ID="odsEmailAddresses" runat="server" DataObjectTypeName="MyLayercake.NTier.Example.BusinessObjects.EmailAddress" DeleteMethod="Delete" InsertMethod="Save" SelectMethod="GetList" TypeName="MyLayercake.NTier.Example.BusinessLayer.EmailAddressManager" UpdateMethod="Save">
            <SelectParameters>
              <asp:ControlParameter ControlID="gvContactPersons" Name="contactPersonId" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
          </asp:ObjectDataSource>
        </asp:View>
        <asp:View ID="View3" runat="server">
          <fieldset>
            <legend>Phone numbers</legend>
            <asp:Button ID="btnNewPhoneNumber" runat="server" Text="Create new Phone Number" OnClick="btnNewPhoneNumber_Click" />
            <asp:Button ID="btnListPhoneNumbers" runat="server" Text="Show Phone Numbers" OnClick="btnListPhoneNumbers_Click" /><br />
            <asp:GridView ID="gvPhoneNumbers" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="odsPhoneNumbers" DataKeyNames="Id">
              <Columns>
                <asp:BoundField DataField="ContactPersonId" HeaderText="Contact Person Id" SortExpression="ContactPersonId" ReadOnly="True" />
                <asp:BoundField DataField="Number" HeaderText="Number" SortExpression="Number" />
                <asp:TemplateField HeaderText="Type" SortExpression="Type">
                  <EditItemTemplate>
                    <asp:DropDownList ID="lstType" runat="server" DataSource='<%#GetContactTypes() %>' SelectedValue='<%# Bind("Type") %>' />
                  </EditItemTemplate>
                  <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this contact record?');"></asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odsPhoneNumbers" runat="server" DataObjectTypeName="MyLayercake.NTier.Example.BusinessObjects.PhoneNumber" DeleteMethod="Delete" InsertMethod="Save" SelectMethod="GetList" TypeName="MyLayercake.NTier.Example.BusinessObjects.PhoneNumberManager" UpdateMethod="Save">
              <SelectParameters>
                <asp:ControlParameter ControlID="gvContactPersons" Name="contactPersonId" PropertyName="SelectedValue" Type="Int32" />
              </SelectParameters>
            </asp:ObjectDataSource>
            <asp:FormView ID="fvPhoneNumber" runat="server" DataKeyNames="Id" DataSourceID="odsPhoneNumbers" DefaultMode="Insert" OnItemInserting="fv_ItemInserting" Visible="false" OnItemCommand="fvPhoneNumber_ItemCommand" OnItemInserted="fvPhoneNumber_ItemInserted" EnableViewState="False">
              <InsertItemTemplate>
                <span class="Label">Number:</span>
                <asp:TextBox ID="NumberTextBox" runat="server" Text='<%# Bind("Number") %>' /><br />
                <span class="Label">Type:</span>
                <asp:DropDownList ID="lstType" runat="server" DataSource='<%#GetContactTypes() %>' SelectedValue='<%#Bind("Type") %>' />
                <br />
                <br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert"></asp:LinkButton>
                <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
              </InsertItemTemplate>
            </asp:FormView>
            <br />
          </fieldset>
        </asp:View>
      </asp:MultiView>
    </div>
  </form>
</body>
</html>
