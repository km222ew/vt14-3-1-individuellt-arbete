<%@ Page Title="Movie details" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="MovieCollection.Pages.MoviePages.Details" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1>Details</h1>
    <p>for</p>
    <%-- Panel med label för rätt-meddelande --%>
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

    <%-- ValidationSummary --%>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            DisplayMode="BulletList" 
            HeaderText="Something went wrong. Correct the error and try again."
            ValidationGroup="Delete"
            CssClass="validationsummary" />
    </div>

    <%-- FormView för visning av en film --%>
    <asp:FormView ID="MovieFormView" runat="server"
        ItemType="MovieCollection.Model.Movie"
        DataKeyNames="MovieID"
        DefaultMode="ReadOnly"
        RenderOuterTable="false"
        SelectMethod="MovieFormView_GetItem"
        DeleteMethod="MovieFormView_DeleteItem">
        <%-- FormView Item--%>
        <ItemTemplate>
            <div>
                <h2><%#: Item.Title %></h2>
            </div>
            <div>
                <h3>Story</h3>
                <p><%#: Item.Story %></p>
            </div>
            <%-- ListView för roller --%>
                <fieldset>
                <legend>Cast</legend>
                    <asp:ListView ID="RoleListView" runat="server"
                        ItemType="MovieCollection.Model.Role"
                        DataKeyNames="MovieID, RoleID, PersID"
                        SelectMethod="RoleListView_GetData"
                        OnItemDataBound="RoleListView_ItemDataBound">
                        <%-- ListView Layout --%>
                        <LayoutTemplate>                        
                                <table id="table">
                                    <tr>
                                        <th>Actor</th>
                                        <th></th>
                                        <th>Role</th>
                                        <th></th>
                                    </tr>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                </table>                                                                
                            <%--<ul>
                            
                            </ul>--%>
                        </LayoutTemplate>
                        <%-- ListView Empty --%>
                        <EmptyDataTemplate>
                            <li>
                                Role is missing
                            </li>
                        </EmptyDataTemplate>
                        <%-- ListView Item --%>
                        <ItemTemplate>
                            <tr>
                                <td class="td_left">
                                    <asp:Label ID="PersonLabel" runat="server" Text="{0} " />
                                </td>
                                <td class="td_middle">
                                    ...
                                </td>
                                <td class="td_left">
                                    <%#: Item.MovieRole %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </fieldset>

            <%-- Knappar --%>
            <div class="detailsButtons">
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    Text="Go back to list" 
                    NavigateUrl='<%# GetRouteUrl("Movies", null)%>'
                    CssClass="button1" />
                <asp:HyperLink ID="HyperLink2" runat="server" 
                    Text="Edit movie details" 
                    NavigateUrl='<%# GetRouteUrl("MovieEdit", null)%>' 
                    CssClass="button1" />
                <asp:LinkButton runat="server" 
                    CommandName="Delete" 
                    Text="Delete movie"
                    ValidationGroup="Delete" 
                    OnClientClick='<%# String.Format("return confirm(\"Do you really want to delete this ({0}) movie?\")", Item.Title) %>' 
                    CssClass="button1" />
            </div>
        </ItemTemplate>
    </asp:FormView>      
</asp:Content>
