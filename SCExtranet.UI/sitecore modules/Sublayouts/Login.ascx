<%@ Control Language="C#" AutoEventWireup="true" 
	CodeBehind="Login.ascx.cs" 
	Inherits="SCExtranet.UI.Login" %>

<%@ Import Namespace="SCExtranet.Core.Utility.FormText" %>

<asp:Panel ID="pnlLogin" CssClass="exLogin" runat="server">
    <sc:FieldRenderer runat="server" ID="contentField" FieldName="Content"  />
	<div class="exForm">
        <h2><SCExtranet:FormText runat="server" TextKey="/Login/Login" /></h2>
	    <p></p>
	    <div class="required">
            <asp:Literal ID="ltlMessage" runat="server" />
        </div>
        <div>
		    <asp:Label AssociatedControlID="txtUser" Text="<%=FormTextUtility.Provider.GetTextByKey("/Login/Username") %>" runat="server"></asp:Label>
			<asp:TextBox ID="txtUser" runat="server" />
			<asp:RequiredFieldValidator ControlToValidate="txtUser" runat="server" ValidationGroup="ExtranetLogin"></asp:RequiredFieldValidator>
            
            <asp:Label AssociatedControlID="txtPass" Text="<%=FormTextUtility.Provider.GetTextByKey("/Login/Password") %>" runat="server"></asp:Label>
			<asp:TextBox ID="txtPass" TextMode="Password" runat="server" />
		    <asp:RequiredFieldValidator ControlToValidate="txtUser" runat="server" ValidationGroup="ExtranetLogin"></asp:RequiredFieldValidator>
            
            <asp:Button Text="<%=FormTextUtility.Provider.GetTextByKey("/Login/Login") %>" ValidationGroup="ExtranetLogin" OnClick="ProcessLogin" runat="server"/>            
        </div>
        <div>
		    <p></p>
            <a href="<%= ForgotPasswordURL%>">
			    <SCExtranet:FormText runat="server" TextKey="/Login/ForgotPassword" />
            </a> | <asp:HyperLink ID="lnkRegister" runat="server">
			    <SCExtranet:FormText runat="server" TextKey="/Login/Register" />
            </asp:HyperLink>
        </div>
	 </div>
</asp:Panel>
<asp:Panel ID="pnlLoggedIn" Visible="false" runat="server">
	<sc:Placeholder Key="logincontent" runat="server" />
</asp:Panel>