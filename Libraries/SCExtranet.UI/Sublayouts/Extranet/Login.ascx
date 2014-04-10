<%@ Control Language="C#" AutoEventWireup="true" 
	CodeBehind="Login.ascx.cs" 
	Inherits="SCExtranet.UI.Login" %>

<asp:Panel ID="pnlLogin" CssClass="extranetLogin" runat="server">
    <sc:FieldRenderer runat="server" ID="contentField" FieldName="Content"  />
	<div class="exForm">
        <h2><SCExtranet:FormText runat="server" TextKey="SCExtranet.Login.Login" /></h2>
	    <p></p>
	    <div class="required">
            <asp:Literal ID="ltlMessage" runat="server" />
        </div>
        <div>
		    <Project:ProjectForm ID="LoginForm" runat="server" SubmitButtonCssClass="link-submit" DisableCaptcha="true" SubmitButtonTextKey="SCExtranet.Login.Login" ValidationGroup="ExtranetLogin" OnClick="ProcessLogin">
			    <asp:TextBox ID="txtUser" runat="server" LabelStringPath="/Strings/Extranet/Login/Username" ValidationGroup="ExtranetLogin" Required="true"  />
			    <asp:TextBox ID="txtPass" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/Login/Password" ValidationGroup="ExtranetLogin" Required="true"  />
		    </Project:ProjectForm>
        </div>
        <div>
		    <p></p>
            <a href="<%= ForgotPasswordURL%>">
			    <SCExtranet:FormText runat="server" TextKey="SCExtranet.Login.ForgotPassword" />
            </a> | <asp:HyperLink ID="lnkRegister" runat="server">
			    <SCExtranet:FormText runat="server" TextKey="SCExtranet.Login.Register" />
            </asp:HyperLink>
        </div>
	 </div>
</asp:Panel>
<asp:Panel ID="pnlLoggedIn" Visible="false" runat="server">
	<sc:Placeholder Key="logincontent" runat="server" />
</asp:Panel>