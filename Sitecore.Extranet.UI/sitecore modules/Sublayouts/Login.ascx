<%@ Control Language="C#" AutoEventWireup="true" 
	CodeBehind="Login.ascx.cs" 
	Inherits="Sitecore.Extranet.UI.Login" %>

<%@ Import Namespace="Sitecore.Extranet.Core.Utility.FormText" %>

<asp:Panel ID="pnlLogin" CssClass="exLogin" runat="server">
    <sc:FieldRenderer runat="server" ID="contentField" FieldName="Content"  />
	<div class="exForm">
        <h2><%=FormTextUtility.Provider.GetTextByKey("/Login/Login") %></h2>
	    <p></p>
	    <div class="required">
            <asp:Literal ID="ltlMessage" runat="server" />
        </div>
        <div>
		    <label><%=FormTextUtility.Provider.GetTextByKey("/Login/Username") %></label>
			<asp:TextBox ID="txtUser" runat="server" />
			<asp:RequiredFieldValidator ControlToValidate="txtUser" runat="server" ValidationGroup="ExtranetLogin"></asp:RequiredFieldValidator>
            
            <label><%=FormTextUtility.Provider.GetTextByKey("/Login/Password") %></label>
			<asp:TextBox ID="txtPass" TextMode="Password" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtUser" runat="server" ValidationGroup="ExtranetLogin"></asp:RequiredFieldValidator>
            
            <asp:Button ID="btnSubmit" ValidationGroup="ExtranetLogin" OnClick="ProcessLogin" runat="server"/>            
        </div>
        <div>
		    <p></p>
            <a href="<%= ForgotPasswordURL%>">
				<%=FormTextUtility.Provider.GetTextByKey("/Login/ForgotPassword") %>
            </a> | <asp:HyperLink ID="lnkRegister" runat="server">
				<%=FormTextUtility.Provider.GetTextByKey("/Login/Register") %>
            </asp:HyperLink>
        </div>
	 </div>
</asp:Panel>
<asp:Panel ID="pnlLoggedIn" Visible="false" runat="server">
	<sc:Placeholder Key="logincontent" runat="server" />
</asp:Panel>