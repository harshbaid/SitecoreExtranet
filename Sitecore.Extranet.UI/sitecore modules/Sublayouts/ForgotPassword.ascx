<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="ForgotPassword.ascx.cs" 
    Inherits="Sitecore.Extranet.UI.ForgotPassword" %>

<%@ Import Namespace="Sitecore.Extranet.Core.Utility.FormText" %>

<div class="exForgotPassword">
	<div class="required">
        <asp:Literal ID="ltlMessage" runat="server" />
    </div>
	<asp:PlaceHolder ID="phFPForm" runat="server">
        <label><%=FormTextUtility.Provider.GetTextByKey("/ForgotPassword/Username") %></label>
		<asp:TextBox ID="txtUser" runat="server" />
		<asp:RequiredFieldValidator ControlToValidate="txtUser" runat="server" ValidationGroup="ExtranetForgot"></asp:RequiredFieldValidator>
        
        <asp:Button ID="btnSubmit" ValidationGroup="ExtranetForgot" OnClick="ProcessSubmit" runat="server"/>            
	</asp:PlaceHolder>
	<div >
		<p></p>
		<a href="<%= LoginURL%>">
			<%=FormTextUtility.Provider.GetTextByKey("/Login/Login") %>
		</a> | <asp:HyperLink ID="lnkRegister" runat="server">
			<%=FormTextUtility.Provider.GetTextByKey("/Login/Register") %>
        </asp:HyperLink>
	</div>
</div>