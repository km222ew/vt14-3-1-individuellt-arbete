<%@ Page Title="Edit Movie" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="MovieCollection.Pages.MoviePages.Edit" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1>Edit</h1>
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
        <asp:ValidationSummary runat="server" 
            DisplayMode="BulletList" 
            HeaderText="Something went wrong. Correct the error and try again."
            CssClass="validationsummary" />
        <asp:ValidationSummary runat="server" 
            DisplayMode="BulletList" 
            HeaderText="Something went wrong. Correct the error and try again."
            ValidationGroup="Insert"
            ShowModelStateErrors="False"
            CssClass="validationsummary"  />
    </div>

    <%-- FormView för ändring av film --%>
    <asp:FormView ID="MovieFormView" runat="server"
        ItemType="MovieCollection.Model.Movie"
        DataKeyNames="MovieID"
        DefaultMode="Edit"
        RenderOuterTable="false"
        SelectMethod="MovieFormView_GetItem"
        UpdateMethod="MovieFormView_UpdateItem">

        <%-- Edit --%>
        <EditItemTemplate>
            <%-- Edit Title --%>
            <div class="textbox">
                <div>
                    <label for="updateTitle">Title</label>
                </div>
                <div >
                    <asp:TextBox ID="updateTitle" runat="server" 
                        MaxLength="60" 
                        Text='<%# BindItem.Title %>'
                        CssClass="input" />
                    <asp:RequiredFieldValidator ID="UpdateMovieRequiredFieldValidator" runat="server" 
                        ErrorMessage="The movies title must be assigned"
                        Display="None"
                        ControlToValidate="updateTitle">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <%-- Edit Story --%>
            <div class="textbox">
                <div>
                    <label for="updateStory">Story</label>
                </div>
                <div>
                    <asp:TextBox ID="updateStory" runat="server" 
                        TextMode="MultiLine"
                        Text='<%# BindItem.Story %>'
                        CssClass="input"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="The movies story must be assigned"
                        Display="None"
                        ControlToValidate="updateStory">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <%-- ListView med roller --%>
            <fieldset>
                <legend>Cast</legend>
                <asp:ListView ID="RoleListView" runat="server"
                    ItemType="MovieCollection.Model.Role"
                    DataKeyNames="MovieID, RoleID, PersID"
                    SelectMethod="RoleListView_GetData"
                    UpdateMethod="RoleListView_UpdateItem"
                    DeleteMethod="RoleListView_DeleteItem"
                    InsertMethod="RoleListView_InsertItem"
                    InsertItemPosition="LastItem">
                    <%-- Layout --%>
                    <LayoutTemplate>
                        <ul>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </ul>
                    </LayoutTemplate>
                    <%-- Empty --%>
                    <EmptyDataTemplate>
                        <li>
                            Role is missing
                        </li>
                    </EmptyDataTemplate>
                    <%-- Item --%>
                    <ItemTemplate>
                        <li>
                            <%-- Dropdown som är disabled och visar aktuell person för den rollen --%>
                            <asp:DropDownList ID="PersonViewDropDownList" runat="server"
                                ItemType="MovieCollection.Model.Person"
                                SelectMethod="PersonDropDownList_GetData"
                                DataTextField="FullName"
                                DataValueField="PersID"
                                SelectedValue='<%# Item.PersID %>'
                                Enabled="false"
                                CssClass="inputSmall" />
                            <%-- Textbox som är disabled och visar aktuell roll --%>
                            <asp:TextBox ID="RoleViewTextBox" runat="server" 
                                Text='<%# Item.MovieRole %>' 
                                MaxLength="60"
                                Enabled="false"
                                CssClass="inputSmall" />
                            <%-- Knappar för ändring och borttagning av roll --%>
                            <asp:LinkButton runat="server" 
                                CommandName="Edit" 
                                Text="Edit" 
                                CausesValidation="false"
                                CssClass="button1"/>
                            <asp:LinkButton runat="server" 
                                CommandName="Delete" 
                                Text="Delete"
                                CausesValidation="false" 
                                OnClientClick='<%# String.Format("return confirm(\"Do you really want to delete this ({0}) role?\")", Item.MovieRole) %>'
                                CssClass="button1" />
                        </li>
                    </ItemTemplate>
                    <%-- Insert ny roll --%>
                    <InsertItemTemplate>
                        <li>
                            <%-- Dropdown med personer --%>
                            <asp:DropDownList ID="PersonDropDownList" runat="server"
                                ItemType="MovieCollection.Model.Person"
                                SelectMethod="PersonDropDownList_GetData"
                                DataTextField="FullName"
                                DataValueField="PersID"
                                SelectedValue='<%# BindItem.PersID %>'
                                CssClass="inputSmall" />
                            <%-- Textbox för ifyllning av ny roll --%>
                            <asp:TextBox ID="InsertRoleTextBox" runat="server" 
                                Text='<%# BindItem.MovieRole %>' 
                                MaxLength="60" 
                                ValidationGroup="Insert"
                                CssClass="inputSmall" />
                            <asp:RequiredFieldValidator ID="InsertRoleRequiredFieldValidator" runat="server" 
                                ErrorMessage="A role must be assigned" 
                                Display="None" 
                                ValidationGroup="Insert" 
                                ControlToValidate="InsertRoleTextBox">
                            </asp:RequiredFieldValidator>
                            <asp:LinkButton runat="server" 
                                CommandName="Insert" 
                                Text="Save" 
                                ValidationGroup="Insert"
                                CssClass="button1" />                               
                        </li>
                    </InsertItemTemplate>
                    <%-- Edit roll --%>
                    <EditItemTemplate>
                        <asp:DropDownList ID="PersonDropDownList2" runat="server"
                                ItemType="MovieCollection.Model.Person"
                                SelectMethod="PersonDropDownList_GetData"
                                DataTextField="FullName"
                                DataValueField="PersID"
                                SelectedValue='<%# BindItem.PersID %>'
                                CssClass="inputSmall" />
                        <asp:TextBox ID="UpdateRoleTextBox" runat="server" 
                                Text='<%# Bind("MovieRole") %>' 
                                MaxLength="60"
                                CssClass="inputSmall" />
                        <asp:RequiredFieldValidator ID="UpdateRoleRequiredFieldValidator" runat="server" 
                                ErrorMessage="A role must be assigned" 
                                Display="None" 
                                ControlToValidate="UpdateRoleTextBox">
                            </asp:RequiredFieldValidator>
                        <asp:LinkButton ID="LinkButton5" runat="server" 
                                CommandName="Update" 
                                Text="Save"
                                CssClass="button1" />
                        <asp:LinkButton ID="LinkButton6" runat="server" 
                                CommandName="Cancel" 
                                Text="Cancel" 
                                CausesValidation="false"
                                CssClass="button1" />

                    </EditItemTemplate>
                </asp:ListView>
            </fieldset>

            <%-- Knappar --%>
            <div class="detailsButtons">
                <asp:LinkButton runat="server" 
                        CommandName="Update" 
                        Text="Save"
                        CssClass="button1" />            
                <asp:LinkButton runat="server" 
                        CommandName="Cancel" 
                        Text="Reset"
                        CausesValidation="false"
                        CssClass="button1" />
                <asp:HyperLink ID="HyperLink1"  runat="server" 
                        Text="Cancel" 
                        NavigateUrl='<%# GetRouteUrl("MovieDetails", new { id = Item.MovieID }) %>'
                        CssClass="button1" />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
