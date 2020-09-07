<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyWorkbench.NTier.WebApi.Controllers.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                        <asp:GridView ID="gvContactPersons" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True" CellPadding="4" GridLines="None">
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
        </div>
    </form>
</body>
</html>
