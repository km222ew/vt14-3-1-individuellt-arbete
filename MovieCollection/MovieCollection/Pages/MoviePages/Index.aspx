<%@ Page Title="Home" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MovieCollection.Pages.MoviePages.Index" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="index">
        <h1>Home</h1>
        <p>My movie-collection </p>
        <p>
            Welcome to my movie-collection. By navigating the links above you can choose to see all the movies in the collection,
            add a new movie to the collection or go back here if you feel like it. There are additional features in the collection
            viewer where you can read the details of a movie, edit the details or delete the movie from the collection entirely.
        </p>
    </div>
</asp:Content>
