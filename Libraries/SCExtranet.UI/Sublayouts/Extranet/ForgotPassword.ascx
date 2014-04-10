<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="ForgotPassword.ascx.cs" 
    Inherits="SCExtranet.UI.ForgotPassword" %>

<div class="forgotPassword">
	<div class="required">
        <asp:Literal ID="ltlMessage" runat="server" />
    </div>
	<asp:PlaceHolder ID="phFPForm" runat="server">
		<Project:ProjectForm ID="ForgotPasswordForm" runat="server" SubmitButtonCssClass="link-submit" SubmitButtonContentPath="/Strings/Extranet/ForgotPassword/ResetPassword" ValidationGroup="ExtranetForgot" OnClick="ProcessSubmit">
			<asp:TextBox ID="txtUser" runat="server" LabelStringPath="/Strings/Extranet/ForgotPassword/Username" ValidationGroup="ExtranetForgot" Required="true"  />
			
		</Project:ProjectForm>
	</asp:PlaceHolder>
	<div >
		<p></p>
		<a href="<%= LoginURL%>">
			<Project:StringContent ID="StringContent1" runat="server" ContentPath="/Strings/Extranet/Login/Login" />
		</a> | <asp:HyperLink ID="lnkRegister" runat="server">
			<Project:StringContent ID="StringContent2" runat="server" ContentPath="/Strings/Extranet/Login/Register" />
        </asp:HyperLink>
	</div>
	<br />
	<Project:StringContent ID="StringContent3" runat="server" ContentField="FormFooterContent" />  
</div>