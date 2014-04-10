using System.Collections.Specialized;
using System.Web.UI;
using SCExtranet.Lib.Extensions;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace SCExtranet.Lib.Sublayouts
{
	public class BaseSublayout : System.Web.UI.UserControl
	{
		private Item _DataSourceItem;
		public Item DataSourceItem {
			get {
				if (_DataSourceItem == null)
					_DataSourceItem = Sitecore.Context.Database.GetItem(DataSourceID);
				return _DataSourceItem;
			}
			set {
				_DataSourceItem = value;
			}
		}

		private string _DataSourceID;
		public string DataSourceID {
			get {
				if (string.IsNullOrEmpty(_DataSourceID))
					_DataSourceID = ((Sublayout)Parent).DataSource;
				return _DataSourceID;
			}
		}

		public Item ContextItem {
			get {
				return Sitecore.Context.Item;
			}
		}

		private Item _DataSourceOrContext;
		public Item DataSourceOrContext {
			get {
				if (_DataSourceOrContext == null)
					_DataSourceOrContext = (DataSourceItem != null) ? DataSourceItem : ContextItem;
				return _DataSourceOrContext;
			}
		}

		private NameValueCollection _Parameters;
		public NameValueCollection Parameters {
			get {
				if (_Parameters == null)
					_Parameters = ((Sublayout)Parent).ParameterList();
				return _Parameters;
			}
			set {
				_Parameters = value;
			}
		}

		public virtual Item PreferredDataSource { get { return DataSourceOrContext; } }

		protected void ApplyDataSource() {
			foreach (Control c in Controls)
				RecurseControls(c, PreferredDataSource);
		}

		private void RecurseControls(Control parent, Item dsItem) {
			string typeName = parent.GetType().FullName;
			if (dsItem == null) {
				parent.Visible = false;
			} else if (typeName.StartsWith("Sitecore.Web.UI.WebControls")) {
				if(typeName.EndsWith("FieldRenderer")) {
					Sitecore.Web.UI.WebControls.FieldRenderer f = (FieldRenderer)parent;
					f.Item = dsItem;
				} else {
					Sitecore.Web.UI.WebControls.FieldControl f = (FieldControl)parent;
					f.Item = dsItem;
				}
			}

			foreach (Control c in parent.Controls)
				RecurseControls(c, dsItem);
		}
	}
}
