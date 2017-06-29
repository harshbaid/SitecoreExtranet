namespace Sitecore.Extranet.UI.Models
{
    public class UserModel
    {
        public bool LoginPanelVisible { get; internal set; }
        public bool LoggedInPanelVisible { get; internal set; }
        public string Message { get; internal set; }
        public string Username { get; internal set; }

        public string Password { get; internal set; }
    }
}