<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="EditPassword.ascx.cs" 
    Inherits="SCExtranet.UI.EditPassword" %>

<div class="editAccount">
	<div class="formContainer">
        <h3>
            <Genzyme:StringContent ID="StringContent2" runat="server" ContentPath="/Strings/Extranet/EditAccount/ChangePassword" />
            <asp:HyperLink ID="lnkBack" runat="server">
                <Genzyme:StringContent ID="StringContent1" runat="server" ContentPath="/Strings/Extranet/EditAccount/Back" DefaultValue="Back to edit account" />
            </asp:HyperLink>
        </h3>
		<div class="required">
			<asp:Literal ID="ltlMessagePass" runat="server" />
		</div>
		<asp:Panel ID="pnlEditPass" runat="server">
			<Genzyme:GenzymeForm ID="gzEditPass" runat="server" SubmitButtonCssClass="link-submit" InvalidFormMessageText="" SubmitButtonContentPath="/Strings/Extranet/EditAccount/ChangePassword" ValidationGroup="ExtranetEditPass" OnClick="ProcessEditPass">
				<asp:TextBox ID="txtPassOld" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/EditAccount/PasswordOld" ValidationGroup="ExtranetEditPass" Required="true"  />
				<asp:TextBox ID="txtPassNew" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/EditAccount/PasswordNew" ValidationGroup="ExtranetEditPass" Required="true"  />
				<asp:TextBox ID="txtPassConfirm" TextMode="Password" runat="server" LabelStringPath="/Strings/Extranet/EditAccount/PasswordNewConfirm" ValidationGroup="ExtranetEditPass" Required="true"  /> 
			</Genzyme:GenzymeForm>	
		</asp:Panel>
    </div>
	<Genzyme:StringContent ID="StringContent6" runat="server" ContentField="FormFooterContent" />  
</div>