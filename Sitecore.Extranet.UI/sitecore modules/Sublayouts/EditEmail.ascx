<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="EditEmail.ascx.cs" 
    Inherits="Sitecore.Extranet.UI.EditEmail" %>

<%@ Import Namespace="Sitecore.Extranet.Core.Utility.FormText" %>

<div class="exEditAccount">
	<div class="formContainer">
        <h3>
			<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/ChangeEmail") %>
            <asp:HyperLink ID="lnkBack" runat="server">
				<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/Back") %>
            </asp:HyperLink>
        </h3>
		<div class="required">
			<asp:Literal ID="ltlMessageEmail" runat="server" />
		</div>
		<asp:Panel ID="pnlEditEmail" runat="server"> 
            <label><%=FormTextUtility.Provider.GetTextByKey("/EditAccount/EmailAddressOld") %></label>
			<asp:TextBox ID="txtEmailAddressOld" runat="server"/>
			<asp:RequiredFieldValidator ControlToValidate="txtEmailAddressOld" runat="server" ValidationGroup="ExtranetEditEmail"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtEmailAddressOld" runat="server" ValidationExpressionRegex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" ValidationGroup="ExtranetEditEmail"></asp:RegularExpressionValidator>
            
            <label><%=FormTextUtility.Provider.GetTextByKey("/EditAccount/EmailAddressNew") %></label>
            <asp:TextBox ID="txtEmailAddressNew" runat="server"/>
			<asp:RequiredFieldValidator ControlToValidate="txtEmailAddressNew" runat="server" ValidationGroup="ExtranetEditEmail"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtEmailAddressNew" runat="server" ValidationExpressionRegex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" ValidationGroup="ExtranetEditEmail"></asp:RegularExpressionValidator>
            
            <label><%=FormTextUtility.Provider.GetTextByKey("/EditAccount/EmailAddressNewConfirm") %></label>
            <asp:TextBox ID="txtEmailAddressConfirm" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="txtEmailAddressConfirm" runat="server" ValidationGroup="ExtranetEditEmail"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtEmailAddressConfirm" runat="server" ValidationExpressionRegex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" ValidationGroup="ExtranetEditEmail"></asp:RegularExpressionValidator>
                
            <asp:Button ID="btnSubmit" ValidationGroup="ExtranetEditEmail" OnClick="ProcessEditEmail" runat="server" />
		</asp:Panel>
    </div> 
</div>