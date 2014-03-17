<%@ Page Title="Movie details" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="MovieCollection.Pages.MoviePages.Details" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <asp:ValidationSummary ID="ValidationSummary" runat="server"
        HeaderText="Something went wrong. Correct the error and try again." />
    <asp:ValidationSummary ID="EditValidationSummary" runat="server"
        HeaderText="Something went wrong. Correct the error and try again."
        ValidationGroup="Insert" />

    <asp:FormView ID="MovieFormView" runat="server"
        ItemType="MovieCollection.Model.Movie"
        DataKeyNames="MovieID"
        DefaultMode="ReadOnly"
        RenderOuterTable="false"
        SelectMethod="MovieFormView_GetItem"
        UpdateMethod="MovieFormView_UpdateItem"
        DeleteMethod="MovieFormView_DeleteItem">
        <ItemTemplate>
            <div>
                <h2><%#: Item.Title %></h2>
            </div>
            <div>
                <h3>Story</h3>
            </div>
            <div>
                <p><%#: Item.Story %></p>
            </div>
            <asp:ListView ID="RoleListView" runat="server"
                ItemType="MovieCollection.Model.Role"
                DataKeyNames="MovieID, RoleID, PersID"
                SelectMethod="RoleListView_GetData"
                UpdateMethod="RoleListView_UpdateItem"
                DeleteMethod="RoleListView_DeleteItem"
                InsertMethod="RoleListView_InsertItem"
                InsertItemPosition="LastItem"
                OnItemDataBound="RoleListView_ItemDataBound">

                <LayoutTemplate>
                    
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <li>
                        Role is missing
                    </li>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <li>
                        <asp:Label ID="PersonLabel" runat="server" Text="{0}   ...   " /><span><%#: Item.MovieRole %></span>
                        <asp:LinkButton runat="server" 
                            CommandName="Edit" 
                            Text="Edit" 
                            CausesValidation="false"/>
                        <asp:LinkButton runat="server" 
                            CommandName="Delete" 
                            Text="Delete"
                            CausesValidation="false" 
                            OnClientClick='<%# String.Format("return confirm(\"Do you really want to delete this ({0}) role?\")", Item.MovieRole) %>' />
                    </li>
                </ItemTemplate>
                <InsertItemTemplate>
                    <li>
                        <asp:DropDownList ID="PersonDropDownList" runat="server"
                            ItemType="MovieCollection.Model.Person"
                            SelectMethod="PersonDropDownList_GetData"
                            DataTextField="FullName"
                            DataValueField="PersID"
                            SelectedValue='<%# BindItem.PersID %>' />
                        <asp:TextBox ID="InsertRoleTextBox" runat="server" 
                            Text='<%# BindItem.MovieRole %>' 
                            MaxLength="60" 
                            ValidationGroup="RoleInsert" />
                        <asp:RequiredFieldValidator ID="InsertRoleRequiredFieldValidator" runat="server" 
                            ErrorMessage="A role must be assigned" 
                            Display="Dynamic" 
                            ValidationGroup="Insert" 
                            ControlToValidate="InsertRoleTextBox">
                        </asp:RequiredFieldValidator>
                        <asp:LinkButton runat="server" 
                            CommandName="Insert" 
                            Text="Save" 
                            ValidationGroup="Insert" />
                        <asp:LinkButton runat="server" 
                            CommandName="Cancel" 
                            Text="Cancel" 
                            ValidationGroup="Insert" />
                               
                    </li>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="PersonDropDownList2" runat="server"
                            ItemType="MovieCollection.Model.Person"
                            SelectMethod="PersonDropDownList_GetData"
                            DataTextField="FullName"
                            DataValueField="PersID"
                            SelectedValue='<%# BindItem.PersID %>' />
                    <asp:TextBox ID="UpdateRoleTextBox" runat="server" 
                            Text='<%# BindItem.MovieRole %>' 
                            MaxLength="60"/>
                    <asp:RequiredFieldValidator ID="UpdateRoleRequiredFieldValidator" runat="server" 
                            ErrorMessage="A role must be assigned" 
                            Display="Dynamic" 
                            ControlToValidate="UpdateRoleTextBox">
                        </asp:RequiredFieldValidator>
                    <asp:LinkButton runat="server" 
                            CommandName="Update" 
                            Text="Save" />
                        <asp:LinkButton runat="server" 
                            CommandName="Cancel" 
                            Text="Cancel" />

                </EditItemTemplate>
            </asp:ListView>
        </ItemTemplate>
        <EditItemTemplate>
            <div>
                <label for="updateTitle">Title</label>
            </div>
            <div>
                <asp:TextBox ID="updateTitle" runat="server" MaxLength="60" Text='<%# BindItem.Title %>' />
                <asp:RequiredFieldValidator ID="UpdateMovieRequiredFieldValidator" runat="server" 
                    ErrorMessage="The movies title must be assigned"
                    Display="Dynamic"
                    ControlToValidate="updateTitle">
                </asp:RequiredFieldValidator>
            </div>
            <div>
                <label for="updateStory">Story</label>
            </div>
            <div>
                <asp:TextBox ID="updateStory" runat="server" TextMode="MultiLine" Text='<%# BindItem.Story %>'/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="The movies story must be assigned"
                    Display="Dynamic"
                    ControlToValidate="updateStory">
                </asp:RequiredFieldValidator>
            </div>
        </EditItemTemplate>
    </asp:FormView>
      
</asp:Content>
