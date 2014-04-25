<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="EditAccount.ascx.cs" 
    Inherits="Sitecore.Extranet.UI.EditAccount" %>

<%@ Import Namespace="Project.Utility" %>

<div class="exEditAccount">
	<div class="formContainer">
        <div class="required">
			<asp:Literal ID="ltlMessage" runat="server" />
		</div>
        <div>
            <label><SCExtranet:FormText TextKey="/Register/Username" runat="server" />:</label>
            <div><asp:Literal ID="ltlUsername" runat="server"></asp:Literal></div>
        </div>
        <div>
            <label><SCExtranet:FormText TextKey="/Register/Password" runat="server" />:</label>
            <div class="profileDetails">
                <span>******************************</span>
                (<asp:HyperLink ID="lnkPass" runat="server">
			        <SCExtranet:FormText runat="server" TextKey="/EditAccount/ChangePassword" />
		        </asp:HyperLink>)
            </div>
        </div>
        <div>
		    <label><SCExtranet:FormText TextKey="/Register/EmailAddress" runat="server" />:</label>
            <div class="profileDetails">
                <span>
                    <asp:Literal ID="ltlEmail" runat="server"></asp:Literal>
                </span>
                (<asp:HyperLink ID="lnkEmail" runat="server">
			        <SCExtranet:FormText runat="server" TextKey="/EditAccount/ChangeEmail" />
		        </asp:HyperLink>)
            </div>
        </div>
    </div> 
</div>