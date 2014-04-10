<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="EditPassword.ascx.cs" 
    Inherits="SCExtranet.UI.EditPassword" %>

<div class="editAccount">
	<div class="formContainer">
        <h3>
            <SCExtranet:FormText runat="server" TextKey="SCExtranet.EditAccount.ChangePassword" />
            <asp:HyperLink ID="lnkBack" runat="server">
                <SCExtranet:FormText runat="server" TextKey="SCExtranet.EditAccount.Back" />
            </asp:HyperLink>
        </h3>
		<div class="required">
			<asp:Literal ID="ltlMessagePass" runat="server" />
		</div>
		<asp:Panel ID="pnlEditPass" runat="server">
			<Project:ProjectForm ID="gzEditPass" runat="server" SubmitButtonCssClass="link-submit" InvalidFormMessageText="" SubmitButtonTextKey="SCExtranet.EditAccount/ChangePassword" ValidationGroup="ExtranetEditPass" OnClick="ProcessEditPass">
				<asp:TextBox ID="txtPassOld" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/EditAccount/PasswordOld" ValidationGroup="ExtranetEditPass" Required="true"  />
				<asp:TextBox ID="txtPassNew" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/EditAccount/PasswordNew" ValidationGroup="ExtranetEditPass" Required="true"  />
				<asp:TextBox ID="txtPassConfirm" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/EditAccount/PasswordNewConfirm" ValidationGroup="ExtranetEditPass" Required="true"  /> 
			</Project:ProjectForm>	
		</asp:Panel>
    </div>Z
</div>