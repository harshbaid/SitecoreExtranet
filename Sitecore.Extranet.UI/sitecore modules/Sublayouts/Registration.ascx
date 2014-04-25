<%@ Control Language="C#" AutoEventWireup="true" 
	CodeBehind="Registration.ascx.cs" 
	Inherits="Sitecore.Extranet.UI.Registration" %>

<%@ Import Namespace="Sitecore.Extranet.Core.Utility.FormText" %>

<div class="exRegistration">
	<asp:Placeholder ID="phForm" runat="server">
		<sc:FieldRenderer runat="server" ID="contentField" FieldName="Content"  />
		<div class="required">
			<asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
		</div>
		<div>
            <asp:Label AssociatedControlID="txtUser" Text="<%=FormTextUtility.Provider.GetTextByKey("/Register/Username") %>" runat="server"></asp:Label>
		    <asp:TextBox ID="txtUser" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtUser" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            
            <asp:Label AssociatedControlID="txtPass" Text="<%=FormTextUtility.Provider.GetTextByKey("/Register/Password") %>" runat="server"></asp:Label>
		    <asp:TextBox ID="txtPass" TextMode="Password" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtPass" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            
            <asp:Label AssociatedControlID="txtConfirmPass" Text="<%=FormTextUtility.Provider.GetTextByKey("/Register/PasswordConfirm") %>" runat="server"></asp:Label>
		    <asp:TextBox ID="txtConfirmPass" TextMode="Password" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtConfirmPass" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            
            <asp:Label AssociatedControlID="txtEmailAddress" Text="<%=FormTextUtility.Provider.GetTextByKey("/Register/EmailAddress") %>" runat="server"></asp:Label>
		    <asp:TextBox ID="txtEmailAddress" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtEmailAddress" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtEmailAddress" runat="server" ValidationExpressionRegex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" ValidationGroup="ExtranetRegistration"></asp:RegularExpressionValidator>
            
            <asp:Label AssociatedControlID="txtConfirmEmailAddress" Text="<%=FormTextUtility.Provider.GetTextByKey("/Register/EmailAddressConfirm") %>" runat="server"></asp:Label>
		    <asp:TextBox ID="txtConfirmEmailAddress" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtConfirmEmailAddress" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtConfirmEmailAddress" runat="server" ValidationExpressionRegex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" ValidationGroup="ExtranetRegistration"></asp:RegularExpressionValidator>
            
            <asp:Button Text="<%=FormTextUtility.Provider.GetTextByKey("/Login/Register") %>" ValidationGroup="ExtranetRegistration" OnClick="ProcessRegistration" runat="server"/>            
        </div>
		<div>
			<p></p>
			<a href="<%= LoginURL%>">
				<SCExtranet:FormText runat="server" TextKey="/Login/Login" />
			</a>
		</div>
	</asp:Placeholder>
	<asp:PlaceHolder ID="phMessage" Visible="false" runat="server">
		<SCExtranet:FormText runat="server" TextKey="/Register/RegistrationCompleted" />
	</asp:PlaceHolder>
</div>