<%@ Control Language="C#" AutoEventWireup="true" 
	CodeBehind="Registration.ascx.cs" 
	Inherits="SCExtranet.UI.Registration" %>

<div class="ExtranetRegistration">
	<asp:Placeholder ID="phForm" runat="server">
		<sc:FieldRenderer runat="server" ID="contentField" FieldName="Content"  />
		<div class="required">
			<asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
		</div>
		<Genzyme:GenzymeForm ID="RegisterForm" runat="server" SubmitButtonCssClass="link-submit" SubmitButtonContentPath="/Strings/Extranet/Login/Register" ValidationGroup="ExtranetRegistration" OnClick="ProcessRegistration">
			<Genzyme:TextBox ID="txtUser" runat="server" LabelStringPath="/Strings/Extranet/Register/Username" ValidationGroup="ExtranetRegistration" Required="true"  />
			<Genzyme:TextBox ID="txtPass" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/Register/Password" ValidationGroup="ExtranetRegistration" Required="true"  />
			<Genzyme:TextBox ID="txtConfirmPass" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/Register/PasswordConfirm" ValidationGroup="ExtranetRegistration" Required="true"  />
			<Genzyme:TextBox ID="txtEmailAddress" runat="server" LabelStringPath="/Strings/Extranet/Register/EmailAddress" ValidationGroup="ExtranetRegistration" Required="true" 
				Regex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" />
			<Genzyme:TextBox ID="txtConfirmEmailAddress" runat="server" LabelStringPath="/Strings/Extranet/Register/EmailAddressConfirm" ValidationGroup="ExtranetRegistration" Required="true"
				Regex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" />
		</Genzyme:GenzymeForm>
		
		<div >
			<p></p>
			<a href="<%= LoginURL%>">
				<Genzyme:StringContent ID="StringContent1" runat="server" ContentPath="/Strings/Extranet/Login/Login" />
			</a>
		</div>
	</asp:Placeholder>
	<asp:PlaceHolder ID="phMessage" Visible="false" runat="server">
		<Genzyme:StringContent runat="server" ContentPath="/Strings/Extranet/Register/RegistrationCompleted" />
	</asp:PlaceHolder>
	<br />
	<Genzyme:StringContent ID="StringContent2" runat="server" ContentField="FormFooterContent" /> 
</div>