<%@ Page Title="Error" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MovieCollection.Pages.Shared.Error" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        

    <h2>Server-error</h2>

    <p>We regret that an error occurred why we could not deal with your inquiry.</p>

    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%$ RouteUrl:routename=Index %>'>Back to the front-page</asp:HyperLink>


</asp:Content>