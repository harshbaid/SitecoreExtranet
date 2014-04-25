<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="ForgotPassword.ascx.cs" 
    Inherits="Sitecore.Extranet.UI.ForgotPassword" %>

<%@ Import Namespace="Sitecore.Extranet.Core.Utility.FormText" %>

<div class="exForgotPassword">
	<div class="required">
        <asp:Literal ID="ltlMessage" runat="server" />
    </div>
	<asp:PlaceHolder ID="phFPForm" runat="server">
        <asp:Label AssociatedControlID="txtUser" Text="<%=FormTextUtility.Provider.GetTextByKey("/ForgotPassword/Username") %>" runat="server"></asp:Label>
		<asp:TextBox ID="txtUser" runat="server" />
		<asp:RequiredFieldValidator ControlToValidate="txtUser" runat="server" ValidationGroup="ExtranetForgot"></asp:RequiredFieldValidator>
        
        <asp:Button Text="<%=FormTextUtility.Provider.GetTextByKey("/ForgotPassword/ResetPassword") %>" ValidationGroup="ExtranetForgot" OnClick="ProcessSubmit" runat="server"/>            
	</asp:PlaceHolder>
	<div >
		<p></p>
		<a href="<%= LoginURL%>">
			<SCExtranet:FormText runat="server" TextKey="/Login/Login" />
		</a> | <asp:HyperLink ID="lnkRegister" runat="server">
			<SCExtranet:FormText runat="server" TextKey="/Login/Register" />
        </asp:HyperLink>
	</div>
</div>