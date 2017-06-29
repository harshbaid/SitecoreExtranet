<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="EditPassword.ascx.cs" 
    Inherits="Sitecore.Extranet.UI.EditPassword" %>

<%@ Import Namespace="Sitecore.Extranet.Core.Utility.FormText" %>

<div class="exEditAccount">
	<div class="formContainer">
        <h3>
			<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/ChangePassword") %>
            <asp:HyperLink ID="lnkBack" runat="server">
				<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/Back") %>
            </asp:HyperLink>
        </h3>
		<div class="required">
			<asp:Literal ID="ltlMessagePass" runat="server" />
		</div>
		<asp:Panel ID="pnlEditPass" runat="server">
			<label><%=FormTextUtility.Provider.GetTextByKey("/EditAccount/PasswordOld") %></label>
			<asp:TextBox ID="txtPassOld" TextMode="Password" runat="server" />
			<asp:RequiredFieldValidator ControlToValidate="txtPassOld" runat="server" ValidationGroup="ExtranetEditPass"></asp:RequiredFieldValidator>
            
            <label><%=FormTextUtility.Provider.GetTextByKey("/EditAccount/PasswordNew") %></label>
			<asp:TextBox ID="txtPassNew" TextMode="Password" runat="server" />
			<asp:RequiredFieldValidator ControlToValidate="txtPassNew" runat="server" ValidationGroup="ExtranetEditPass"></asp:RequiredFieldValidator>
            
            <label><%=FormTextUtility.Provider.GetTextByKey("/EditAccount/PasswordNewConfirm") %></label>
			<asp:TextBox ID="txtPassConfirm" TextMode="Password" runat="server" /> 
			<asp:RequiredFieldValidator ControlToValidate="txtPassConfirm" runat="server" ValidationGroup="ExtranetEditPass"></asp:RequiredFieldValidator>
            
            <asp:Button ID="btnSubmit" ValidationGroup="ExtranetEditPass" OnClick="ProcessEditPass" runat="server"/>            
		</asp:Panel>
    </div>
</div>