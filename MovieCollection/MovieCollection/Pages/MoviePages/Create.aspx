<%@ Page Title="Add movie" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="MovieCollection.Pages.MoviePages.Create" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1>Add movie</h1>
    <%-- ValidationSummary --%>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            DisplayMode="BulletList"
            HeaderText="Something went wrong. Correct the error and try again."
            CssClass="validationsummary" />
    </div>

    <%-- FormView för att lägga till en ny film --%>
    <asp:FormView ID="MovieFormView" runat="server"
        ItemType="MovieCollection.Model.Movie"
        DataKeyNames="MovieID"
        RenderOuterTable="false"
        DefaultMode="Insert"
        InsertMethod="MovieFormView_InsertItem">
        <%-- InsertTemplate --%>
        <InsertItemTemplate>
            <%-- Insert Title --%>
            <div class="textbox">   
                <div>
                    <label for="TitleTextBox">Title</label>
                </div>
                <div>
                    <asp:TextBox ID="TitleTextBox" runat="server" 
                        MaxLength="60" 
                        Text='<%# BindItem.Title %>'
                        CssClass="input" />
                    <asp:RequiredFieldValidator ID="TitleRequiredFieldValidator" runat="server" 
                        ErrorMessage="The movies title must be assigned"
                        Display="None"
                        ControlToValidate="TitleTextBox">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <%-- Insert Story --%>
            <div class="textbox"> 
                <div>
                    <label for="StoryTextBox">Story</label>
                </div>
                <div>
                    <asp:TextBox ID="StoryTextBox" runat="server" 
                        TextMode="MultiLine" 
                        Text='<%# BindItem.Story %>'
                        CssClass="input" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="The movies story must be assigned"
                        Display="None"
                        ControlToValidate="StoryTextBox">
                    </asp:RequiredFieldValidator>
                </div>
             </div>
            <%-- Insert Role --%>
            <div class="textbox">
                <div>
                    <label for="InsertRoleTextBox">Role</label>
                </div>
                <%-- Dropdown för val av person --%>
                <asp:DropDownList ID="PersonDropDownList" runat="server"
                    ItemType="MovieCollection.Model.Person"
                    SelectMethod="PersonDropDownList_GetData"
                    DataTextField="FullName"
                    DataValueField="PersID"
                    SelectedValue='<%# Bind("PersID") %>'
                    CssClass="inputCreateRoleDropDown" />
                <%-- TextBox för roll --%>
                <asp:TextBox ID="InsertRoleTextBox" runat="server" 
                    Text='<%# Bind("MovieRole") %>' 
                    MaxLength="60" 
                    ValidationGroup="Insert"
                    CssClass="inputCreateRoleTextBox" />
                <asp:RequiredFieldValidator ID="InsertRoleRequiredFieldValidator" runat="server" 
                    ErrorMessage="A role must be assigned" 
                    Display="None" 
                    ControlToValidate="InsertRoleTextBox">
                </asp:RequiredFieldValidator>
            </div>

            <%-- Knappar --%>
            <div class="detailsButtons">
            <asp:LinkButton ID="LinkButton1" runat="server" 
                    CommandName="Insert" 
                    Text="Save"
                    CssClass="button1" />            
            <asp:LinkButton ID="LinkButton2" runat="server" 
                    CommandName="Cancel" 
                    Text="Reset" 
                    CausesValidation="false"
                    CssClass="button1" />
            <asp:HyperLink ID="HyperLink1"  runat="server" 
                    Text="Cancel" 
                    NavigateUrl='<%# GetRouteUrl("Movies") %>'
                    CssClass="button1" />
            </div>
        </InsertItemTemplate>
    </asp:FormView>
</asp:Content>