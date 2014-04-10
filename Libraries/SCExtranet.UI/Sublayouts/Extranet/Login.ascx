<%@ Control Language="C#" AutoEventWireup="true" 
	CodeBehind="Login.ascx.cs" 
	Inherits="SCExtranet.UI.Login" %>

<asp:Panel ID="pnlLogin" CssClass="extranetLogin" runat="server">
    <sc:FieldRenderer runat="server" ID="contentField" FieldName="Content"  />
	<div class="exForm">
        <h2><Genzyme:StringContent ID="StringContent3" runat="server" ContentPath="/Strings/Extranet/Login/Login" /></h2>
	    <p></p>
	    <div class="required">
            <asp:Literal ID="ltlMessage" runat="server" />
        </div>
        <div>
		    <Genzyme:GenzymeForm ID="LoginForm" runat="server" SubmitButtonCssClass="link-submit" DisableCaptcha="true" SubmitButtonContentPath="/Strings/Extranet/Login/Login" ValidationGroup="ExtranetLogin" OnClick="ProcessLogin">
			    <Genzyme:TextBox ID="txtUser" runat="server" LabelStringPath="/Strings/Extranet/Login/Username" ValidationGroup="ExtranetLogin" Required="true"  />
			    <Genzyme:TextBox ID="txtPass" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/Login/Password" ValidationGroup="ExtranetLogin" Required="true"  />
		    </Genzyme:GenzymeForm>
        </div>
        <div>
		    <p></p>
            <a href="<%= ForgotPasswordURL%>">
			    <Genzyme:StringContent ID="StringContent1" runat="server" ContentPath="/Strings/Extranet/Login/ForgotPassword" />
            </a> | <asp:HyperLink ID="lnkRegister" runat="server">
			    <Genzyme:StringContent ID="StringContent2" runat="server" ContentPath="/Strings/Extranet/Login/Register" />
            </asp:HyperLink>
        </div>
	 </div>
</asp:Panel>
<asp:Panel ID="pnlLoggedIn" Visible="false" runat="server">
	<sc:FieldRenderer runat="server" ID="FieldRenderer1" FieldName="FormConfirmationContent"  />
    <sc:Placeholder Key="logincontent" runat="server" />
</asp:Panel>
<br />
<Genzyme:StringContent ID="StringContent4" runat="server" ContentField="FormFooterContent" /> 