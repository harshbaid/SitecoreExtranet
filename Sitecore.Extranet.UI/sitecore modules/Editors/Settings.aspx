﻿<%@ Page Language="C#" AutoEventWireup="true" 
	CodeFile="Settings.aspx.cs" 
	Inherits="Sitecore.Extranet.UI.Settings" %>

<%@ Import Namespace="Sitecore.Web" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link rel="stylesheet" href="/sitecore modules/shell/extranet/editors/css/style.css"></link>
</head>
<body>
    <form id="form1" runat="server">
    <div class="extranetSettings">
		<div class="message">
			<asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
		</div>
		<div class="protectedSites">
			<asp:Repeater ID="rptSites" runat="server">
				<ItemTemplate>
					<div class="siteInfo">
						<div>
							<span>Site Name: </span>
							<%# ((SiteInfo)Container.DataItem).Name %>
						</div>
						<div>
							<span>Extranet User Prefix: </span>
							<%# ((SiteInfo)Container.DataItem).Properties["ExtranetUserPrefix"] %>
						</div>
						<div>
							<span>Extranet Role: </span>
							<%# ((SiteInfo)Container.DataItem).Properties["ExtranetRole"] %>
						</div>
						<div>
							<span>Extranet Provider: </span>
							<%# ((SiteInfo)Container.DataItem).Properties["ExtranetProvider"] %>
						</div>
						<div>
							<span>Login URL: </span>
							<%# ((SiteInfo)Container.DataItem).Properties["loginPage"] %>
						</div>
						<div>
							<span>User Count: </span>
							<%# GetUserCount((SiteInfo)Container.DataItem) %>
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
		</div>
    </div>
    </form>
</body>
</html>
