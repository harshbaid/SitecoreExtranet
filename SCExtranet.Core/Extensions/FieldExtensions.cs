using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace SCExtranet.Core.Extensions {
	public static class FieldExtensions {
		#region LinkField

		/// If <paramref name="field"/> is of the type <see cref="Sitecore.Data.Fields.LinkField"/> or <see cref="Sitecore.Data.Fields.InternalLinkField"/> 
		/// this will set the link to refer to <paramref name="target"/>
		/// </summary>
		/// <param name="field"></param>
		/// <param name="target"></param>
		/// <param name="setFullPath">When true the path will be <paramref name="target"/>'s full path. When false the path used will be relative to sitecore/content</param>
		public static void SetLinkField(this Field field, Item target, bool setFullPath) {
			var item = field.Item;
			// if the item is not in the editing state, make it so
			if (!item.Editing.IsEditing) {
				using (new EditContext(item)) {
					SetCore(field, target, setFullPath);
				}
			} else {
				SetCore(field, target, setFullPath);
			}
		}

		#endregion LinkField

		#region Private Methods

		private static void SetCore(Field field, Item target, bool setFullPath) {
			string linkValue = setFullPath ? target.Paths.FullPath : string.Format("{0}.aspx", target.Paths.ContentPath);

			if (field.TypeKey == "link" || field.TypeKey == "general link") {
				var link = (LinkField)field;
				link.LinkType = "internal";
				link.Url = linkValue;
				link.Target = string.Empty;
				link.TargetID = target.ID;
			} else if (field.TypeKey == "internal link") {
				var link = (InternalLinkField)field;
				link.Path = linkValue;
			}
		}

		#endregion Private Methods
	}
}
