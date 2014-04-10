<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="AccountNavigation.ascx.cs" 
    Inherits="SCExtranet.UI.AccountNavigation" %>

<asp:PlaceHolder ID="phAccountNav" Visible="false" runat="server">
	<div class="ExtranetAccountNavigation">
		<Genzyme:StringContent ID="StringContent2" runat="server" ContentPath="/Strings/Extranet/Login/Welcome" />&nbsp;<asp:Literal ID="ltlUsername" runat="server"></asp:Literal>,
		<a href="<%= EditAccountURL %>">
			<Genzyme:StringContent ID="StringContent1" runat="server" ContentPath="/Strings/Extranet/Login/EditAccountInfo" />
		</a> | <asp:LinkButton ID="Button1" Text="Logout" runat="server" OnClick="ProcessLogout" />       
	</div>
</asp:PlaceHolder>