<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="ForgotPassword.ascx.cs" 
    Inherits="SCExtranet.UI.ForgotPassword" %>

<div class="forgotPassword">
	<div class="required">
        <asp:Literal ID="ltlMessage" runat="server" />
    </div>
	<asp:PlaceHolder ID="phFPForm" runat="server">
		<Project:ProjectForm ID="ForgotPasswordForm" runat="server" SubmitButtonCssClass="link-submit" SubmitButtonTextKey="SCExtranet.ForgotPassword.ResetPassword" ValidationGroup="ExtranetForgot" OnClick="ProcessSubmit">
			<asp:TextBox ID="txtUser" runat="server" LabelStringPath="/Strings/Extranet/ForgotPassword/Username" ValidationGroup="ExtranetForgot" Required="true"  />
			
		</Project:ProjectForm>
	</asp:PlaceHolder>
	<div >
		<p></p>
		<a href="<%= LoginURL%>">
			<SCExtranet:FormText runat="server" TextKey="SCExtranet.Login.Login" />
		</a> | <asp:HyperLink ID="lnkRegister" runat="server">
			<SCExtranet:FormText runat="server" TextKey="SCExtranet.Login.Register" />
        </asp:HyperLink>
	</div>
</div>