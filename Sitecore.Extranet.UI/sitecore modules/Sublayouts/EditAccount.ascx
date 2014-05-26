<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="EditAccount.ascx.cs" 
    Inherits="Sitecore.Extranet.UI.EditAccount" %>

<%@ Import Namespace="Sitecore.Extranet.Core.Utility.FormText" %>

<div class="exEditAccount">
	<div class="formContainer">
        <div class="required">
			<asp:Literal ID="ltlMessage" runat="server" />
		</div>
        <div>
            <label><%=FormTextUtility.Provider.GetTextByKey("/Register/Username") %>:</label>
            <div><asp:Literal ID="ltlUsername" runat="server"></asp:Literal></div>
        </div>
        <div>
            <label><%=FormTextUtility.Provider.GetTextByKey("/Register/Password") %>:</label>
            <div class="profileDetails">
                <span>******************************</span>
                (<asp:HyperLink ID="lnkPass" runat="server">
					<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/ChangePassword") %>
		        </asp:HyperLink>)
            </div>
        </div>
        <div>
		    <label><%=FormTextUtility.Provider.GetTextByKey("/Register/EmailAddress") %>:</label>
            <div class="profileDetails">
                <span>
                    <asp:Literal ID="ltlEmail" runat="server"></asp:Literal>
                </span>
                (<asp:HyperLink ID="lnkEmail" runat="server">
					<%=FormTextUtility.Provider.GetTextByKey("/EditAccount/ChangeEmail") %>
		        </asp:HyperLink>)
            </div>
        </div>
    </div> 
</div>