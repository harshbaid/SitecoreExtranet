<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="EditPassword.ascx.cs" 
    Inherits="Sitecore.Extranet.UI.EditPassword" %>

<%@ Import Namespace="Sitecore.Extranet.Core.Utility.FormText" %>

<div class="exEditAccount">
	<div class="formContainer">
        <h3>
            <SCExtranet:FormText runat="server" TextKey="/EditAccount/ChangePassword" />
            <asp:HyperLink ID="lnkBack" runat="server">
                <SCExtranet:FormText runat="server" TextKey="/EditAccount/Back" />
            </asp:HyperLink>
        </h3>
		<div class="required">
			<asp:Literal ID="ltlMessagePass" runat="server" />
		</div>
		<asp:Panel ID="pnlEditPass" runat="server">
			<asp:Label AssociatedControlID="txtPassOld" Text="<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/PasswordOld") %>" runat="server"></asp:Label>
			<asp:TextBox ID="txtPassOld" TextMode="Password" runat="server" />
			<asp:RequiredFieldValidator ControlToValidate="txtPassOld" runat="server" ValidationGroup="ExtranetEditPass"></asp:RequiredFieldValidator>
            
            <asp:Label AssociatedControlID="txtPassNew" Text="<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/PasswordNew") %>" runat="server"></asp:Label>
			<asp:TextBox ID="txtPassNew" TextMode="Password" runat="server" />
			<asp:RequiredFieldValidator ControlToValidate="txtPassNew" runat="server" ValidationGroup="ExtranetEditPass"></asp:RequiredFieldValidator>
            
            <asp:Label AssociatedControlID="txtPassConfirm" Text="<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/PasswordNewConfirm") %>" runat="server"></asp:Label>
			<asp:TextBox ID="txtPassConfirm" TextMode="Password" runat="server" /> 
			<asp:RequiredFieldValidator ControlToValidate="txtPassConfirm" runat="server" ValidationGroup="ExtranetEditPass"></asp:RequiredFieldValidator>
            
            <asp:Button Text="<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/ChangePassword") %>" ValidationGroup="ExtranetEditPass" OnClick="ProcessEditPass" runat="server"/>            
		</asp:Panel>
    </div>Z
</div>