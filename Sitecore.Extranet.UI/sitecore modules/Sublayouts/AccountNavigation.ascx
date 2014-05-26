<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="AccountNavigation.ascx.cs" 
    Inherits="Sitecore.Extranet.UI.AccountNavigation" %>

<%@ Import Namespace="Sitecore.Extranet.Core.Utility.FormText" %>

<asp:PlaceHolder ID="phAccountNav" Visible="false" runat="server">
	<div class="exAccountNav">
		<%=FormTextUtility.Provider.GetTextByKey("/Login/Welcome") %>&nbsp;<asp:Literal ID="ltlUsername" runat="server"></asp:Literal>,
		<a href="<%= EditAccountURL %>">
			<%=FormTextUtility.Provider.GetTextByKey("/Login/EditAccountInfo") %>
		</a> | <asp:LinkButton ID="Button1" Text="Logout" runat="server" OnClick="ProcessLogout" />       
	</div>
</asp:PlaceHolder>