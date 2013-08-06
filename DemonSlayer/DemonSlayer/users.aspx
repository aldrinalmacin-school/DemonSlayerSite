<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="lab3_solution.users" %>
<asp:content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:content>
<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:gridview id="gvUsers" runat="server" datakeynames="UserID" 
        onrowdeleting="gvUsers_RowDeleting" autogeneratecolumns="False" 
        onrowdatabound="gvUsers_RowDataBound">
    <columns>
        <asp:boundfield datafield="UserID" visible="false" />
        <asp:boundfield datafield="Username" headertext="Username" />
        <asp:boundfield datafield="Role" headertext="Role" />
        <asp:commandfield deletetext="Delete" headertext="Delete" showdeletebutton="true" />
    </columns>
</asp:gridview>

</asp:content>
