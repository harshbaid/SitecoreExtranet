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
            <label><%=FormTextUtility.Provider.GetTextByKey("/Register/Username") %></label>
		    <asp:TextBox ID="txtUser" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtUser" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            
            <label><%=FormTextUtility.Provider.GetTextByKey("/Register/Password") %></label>
		    <asp:TextBox ID="txtPass" TextMode="Password" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtPass" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            
            <label><%=FormTextUtility.Provider.GetTextByKey("/Register/PasswordConfirm") %></label>
		    <asp:TextBox ID="txtConfirmPass" TextMode="Password" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtConfirmPass" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            
            <label><%=FormTextUtility.Provider.GetTextByKey("/Register/EmailAddress") %></label>
		    <asp:TextBox ID="txtEmailAddress" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtEmailAddress" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtEmailAddress" runat="server" ValidationExpressionRegex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" ValidationGroup="ExtranetRegistration"></asp:RegularExpressionValidator>
            
            <label><%=FormTextUtility.Provider.GetTextByKey("/Register/EmailAddressConfirm") %></label>
		    <asp:TextBox ID="txtConfirmEmailAddress" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtConfirmEmailAddress" runat="server" ValidationGroup="ExtranetRegistration"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtConfirmEmailAddress" runat="server" ValidationExpressionRegex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" ValidationGroup="ExtranetRegistration"></asp:RegularExpressionValidator>
            
            <asp:Button id="btnSubmit" ValidationGroup="ExtranetRegistration" OnClick="ProcessRegistration" runat="server"/>            
        </div>
		<div>
			<p></p>
			<a href="<%= LoginURL%>">
				<%=FormTextUtility.Provider.GetTextByKey("/Login/Login") %>
			</a>
		</div>
	</asp:Placeholder>
	<asp:PlaceHolder ID="phMessage" Visible="false" runat="server">
		<%=FormTextUtility.Provider.GetTextByKey("/Register/RegistrationCompleted") %>
	</asp:PlaceHolder>
</div>