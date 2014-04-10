<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="EditEmail.ascx.cs" 
    Inherits="SCExtranet.UI.EditEmail" %>

<div class="editAccount">
	<div class="formContainer">
        <h3>
            <Genzyme:StringContent ID="StringContent1" runat="server" ContentPath="/Strings/Extranet/EditAccount/ChangeEmail" />
            <asp:HyperLink ID="lnkBack" runat="server">
                <Genzyme:StringContent ID="StringContent2" runat="server" ContentPath="/Strings/Extranet/EditAccount/Back" DefaultValue="Back to edit account" />
            </asp:HyperLink>
        </h3>
		<div class="required">
			<asp:Literal ID="ltlMessageEmail" runat="server" />
		</div>
		<asp:Panel ID="pnlEditEmail" runat="server">
			<Genzyme:GenzymeForm ID="gzEditEmail" runat="server" SubmitButtonCssClass="link-submit" InvalidFormMessageText="" SubmitButtonContentPath="/Strings/Extranet/EditAccount/ChangeEmail" ValidationGroup="ExtranetEditEmail" OnClick="ProcessEditEmail">
				<asp:TextBox ID="txtEmailAddressOld" runat="server" LabelStringPath="/Strings/Extranet/EditAccount/EmailAddressOld" ValidationGroup="ExtranetEditEmail" Required="true" 
					Regex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" />
				<asp:TextBox ID="txtEmailAddressNew" runat="server" LabelStringPath="/Strings/Extranet/EditAccount/EmailAddressNew" ValidationGroup="ExtranetEditEmail" Required="true" 
					Regex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" />
				<asp:TextBox ID="txtEmailAddressConfirm" runat="server" LabelStringPath="/Strings/Extranet/EditAccount/EmailAddressNewConfirm" ValidationGroup="ExtranetEditEmail" Required="true"
					Regex="^([0-9a-zA-Z]+([_.-]?[0-9a-zA-Z]+)*@[0-9a-zA-Z]+[0-9,a-z,A-Z,.,-]*[.]{1}[a-zA-Z]{2,4})+$" />	
			</Genzyme:GenzymeForm>
		</asp:Panel>
    </div>
	<Genzyme:StringContent ID="StringContent6" runat="server" ContentField="FormFooterContent" />  
</div>