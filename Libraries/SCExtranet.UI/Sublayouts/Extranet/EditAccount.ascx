<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="EditAccount.ascx.cs" 
    Inherits="SCExtranet.UI.EditAccount" %>

<%@ Import Namespace="Genzyme.Utility" %>

<div class="editAccount">
	<div class="formContainer">
        <div class="required">
			<asp:Literal ID="ltlMessage" runat="server" />
		</div>
        <div>
            <label><Genzyme:StringContent ContentPath="/Strings/Extranet/Register/Username" runat="server" />:</label>
            <div><asp:Literal ID="ltlUsername" runat="server"></asp:Literal></div>
        </div>
        <div>
            <label><Genzyme:StringContent ContentPath="/Strings/Extranet/Register/Password" runat="server" />:</label>
            <div class="profileDetails">
                <span>******************************</span>
                (<asp:HyperLink ID="lnkPass" runat="server">
			        <Genzyme:StringContent ID="StringContent4" runat="server" ContentPath="/Strings/Extranet/EditAccount/ChangePassword" />
		        </asp:HyperLink>)
            </div>
        </div>
        <div>
		    <label><Genzyme:StringContent ContentPath="/Strings/Extranet/Register/EmailAddress" runat="server" />:</label>
            <div class="profileDetails">
                <span>
                    <asp:Literal ID="ltlEmail" runat="server"></asp:Literal>
                </span>
                (<asp:HyperLink ID="lnkEmail" runat="server">
			        <Genzyme:StringContent ID="StringContent3" runat="server" ContentPath="/Strings/Extranet/EditAccount/ChangeEmail" />
		        </asp:HyperLink>)
            </div>
        </div>
    </div>
	<Genzyme:StringContent ID="StringContent6" runat="server" ContentField="FormFooterContent" />  
</div>