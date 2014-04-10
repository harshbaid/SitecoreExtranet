<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="AccountNavigation.ascx.cs" 
    Inherits="SCExtranet.UI.AccountNavigation" %>

<%@ Register TagPrefix="SCExtranet" Assembly="SCExtranet.UI" %>

<asp:PlaceHolder ID="phAccountNav" Visible="false" runat="server">
	<div class="ExtranetAccountNavigation">
		<SCExtranet:FormText runat="server" TextKey="SCExtranet.Login.Welcome" />&nbsp;<asp:Literal ID="ltlUsername" runat="server"></asp:Literal>,
		<a href="<%= EditAccountURL %>">
			<SCExtranet:FormText runat="server" TextKey="SCExtranet.Login.EditAccountInfo" />
		</a> | <asp:LinkButton ID="Button1" Text="Logout" runat="server" OnClick="ProcessLogout" />       
	</div>
</asp:PlaceHolder>