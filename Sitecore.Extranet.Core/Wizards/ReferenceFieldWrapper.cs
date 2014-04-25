using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.SecurityModel;
using Sitecore.Extranet.Core.Extensions;
using Sitecore.Links;

namespace Sitecore.Extranet.Core.Wizards {
	/// <summary>
	/// Provides a single interface for Sitecore fields that can reference other Sitecore items
	/// </summary>
	public class ReferenceFieldWrapper {
		#region Constants
		public static readonly HashSet<string> ReferenceTypes = new HashSet<string>() { 
            "internal link", "link", "general link", 
			"tree list queryable", "lookup", "checklist", "reference", "tree", 
			"multilist", "field suite multilist", 
			"treelist", "field suite treelist",
			"treelistex", "field suite treelistex",
            "droplink", "field suite droplink", 
			"droptree", "field suite droptree"
        };
		#endregion Constants

		#region Fields
		private Field _field;
		private bool _isLinkField;
		#endregion Fields

		#region Properties
		/// <summary>
		/// Returns a sequence of all items referenced by this field.
		/// </summary>
		public IEnumerable<Item> ReferencedItems {
			get {
				IEnumerable<Item> ret = new Item[] { };
				if (_isLinkField) {
					if (_field.TypeKey == "internal link") {
						InternalLinkField ilf = _field;
						if (null != ilf.TargetItem) {
							ret = new Item[] { ilf.TargetItem };
						}
					} else {
						LinkField lf = _field;
						if (null != lf.TargetItem) {
							ret = new Item[] { lf.TargetItem };
						}
					}
				} else {
					MultilistField mf = _field;
					ret = mf.GetItems();
				}
				return ret;
			}
		}
		#endregion Properties

		#region Base
		/// <summary>
		/// Construct a ReferenceFieldWrapper from Sitecore Field.
		/// </summary>
		/// <param name="field"></param>
		public ReferenceFieldWrapper(Field field) {

			_field = field;
			_isLinkField = (field.TypeKey == "link" || field.TypeKey == "general link" || field.TypeKey == "internal link");
		}
		#endregion Base

		#region Public Methods
		/// <summary>
		/// Swap the reference to oldItem for a reference to newItem
		/// </summary>
		/// <param name="oldItem"></param>
		/// <param name="newItem"></param>
		public void Switch(Item oldItem, Item newItem) {
			using (new SecurityDisabler()) {
				if (_isLinkField) {
					_field.SetLinkField(newItem, (_field.TypeKey == "internal link"));
				} else {
					MultilistField mf = _field;
					if (!_field.Item.Editing.IsEditing) {
						using (new EditContext(_field.Item)) {
							mf.Replace(oldItem.ID.ToString(), newItem.ID.ToString());
						}
					} else {
						mf.Replace(oldItem.ID.ToString(), newItem.ID.ToString());
					}
				}
			}
		}
		#endregion Public Methods
	}
}
