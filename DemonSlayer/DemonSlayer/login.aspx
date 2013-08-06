<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="lesson9.login" %>
<asp:content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:content>
<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div>
     <asp:label id="lblError" runat="server" text="Invalid Login" visible="false" 
         cssclass="error"></asp:label>
</div>
<div>
    <label for="txtUsername">Username:</label>
    <asp:textbox id="txtUsername" runat="server"></asp:textbox>
</div>
<div>
    <label for="txtPassword">Password:</label>
    <asp:textbox id="txtPassword" runat="server" textmode="Password"></asp:textbox>
</div>

<asp:button id="btnLogin" runat="server" text="Login" onclick="btnLogin_Click" />
</asp:content>
