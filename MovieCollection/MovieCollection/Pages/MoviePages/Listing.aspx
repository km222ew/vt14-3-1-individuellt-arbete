<%@ Page Title="Movies" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="MovieCollection.Pages.MoviePages.Listing" ViewStateMode="Disabled" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Panel with label for successfull message --%>
    <asp:Panel ID="successPanel" runat="server" 
        CssClass="" 
        Visible="true">
        <asp:Label ID="successLabel" runat="server">akjdkasdj</asp:Label>
        <div id="close">
            <asp:ImageButton ID="closeMessage" runat="server" 
                ImageUrl="Content/delete.jpg" 
                CausesValidation="False" />
        </div>
    </asp:Panel>

    <%-- ListView --%>
    <asp:ListView ID="MovieListView" runat="server"
        Itemtype="MovieCollection.Model.Movie"
        SelectMethod="MovieListView_GetDataPageWise"
        DeleteMethod="MovieListView_DeleteItem"
        DataKeyNames="MovieID">

        <%-- Layout --%>
        <LayoutTemplate>
            <table>
                <tr>
                    <th>Title</th>
                    <th>Story</th>
                    <th></th>
                    <th></th>
                </tr>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </table>
            <asp:DataPager ID="MovieDataPager" runat="server" PagedControlID="MovieListView" PageSize="20">
               
                 <Fields>
                    <asp:NextPreviousPagerField
                        ShowFirstPageButton="true"
                        ShowNextPageButton="false" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField
                        ShowLastPageButton="true" 
                        ShowPreviousPageButton="false" />
                </Fields>

            </asp:DataPager>
        </LayoutTemplate>

        <%-- Item --%>
        <ItemTemplate>
            <tr>
                <td>
                    <%#: Item.Title %>
                </td>
                <td>
                    <%#: Item.Story %>
                </td>
                <td>
                    <asp:HyperLink ID="DetailHyperLink" runat="server" NavigateUrl='<%# GetRouteUrl("MovieDetails", new {id = Item.MovieID}) %>' Text ="Read More"></asp:HyperLink>
                    <asp:LinkButton runat="server" 
                        CommandName="Delete" 
                        Text="Delete" 
                        CausesValidation="false"  
                        OnClientClick='<%# String.Format("return confirm(\"Do you really want to delete this ({0}) movie?\")", Item.Title) %>'>
                    </asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>

        <%-- Empty --%>
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>
                        Movie is missing
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
  
</asp:Content>
