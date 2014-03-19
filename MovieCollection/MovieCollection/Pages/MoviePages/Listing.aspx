<%@ Page Title="Movies" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="MovieCollection.Pages.MoviePages.Listing" ViewStateMode="Disabled" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1>Movies</h1>
    <%-- Panel with label for successfull message --%>
    <asp:Panel ID="successPanel" runat="server"  
        Visible="false"
        CssClass="successPanel">
        <asp:Label ID="successLabel" runat="server"></asp:Label>
        <div id="close" class="close">
            <asp:ImageButton ID="closeMessage" runat="server" 
                ImageUrl="~/Content/delete.jpg" 
                CausesValidation="False" />
        </div>
    </asp:Panel>
    <%-- ValidationSummary for delete --%>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            DisplayMode="BulletList" 
            HeaderText="Something went wrong. Correct the error and try again."
            ValidationGroup="Delete"
            CssClass="validationsummary" />
    </div>
    <%-- ListView --%>
    <asp:ListView ID="MovieListView" runat="server"
        Itemtype="MovieCollection.Model.Movie"
        SelectMethod="MovieListView_GetDataPageWise"
        DeleteMethod="MovieListView_DeleteItem"
        DataKeyNames="MovieID">

        <%-- Layout --%>
        <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>

            <%-- Paging --%>
            <div class="paging">
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
            </div>
        </LayoutTemplate>

        <%-- Item --%>
        <ItemTemplate>
            <div class="box">
                <div class="title">
                    <%#: Item.Title %>
                </div>
                <div class="buttons">
                    <asp:HyperLink ID="DetailHyperLink"  runat="server" 
                        CssClass="button2" 
                        NavigateUrl='<%# GetRouteUrl("MovieDetails", new {id = Item.MovieID}) %>' 
                        Text ="Read More"></asp:HyperLink>
                    <asp:LinkButton ID="LinkButton1" runat="server" 
                        CommandName="Delete" 
                        Text="Delete" 
                        ValidationGroup="Delete"
                        CssClass="button2"
                        OnClientClick='<%# String.Format("return confirm(\"Do you really want to delete this ({0}) movie?\")", Item.Title) %>'>
                    </asp:LinkButton>
                </div>
            </div>
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
