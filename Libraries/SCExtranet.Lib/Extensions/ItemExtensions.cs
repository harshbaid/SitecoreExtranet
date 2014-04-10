using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;

namespace SCExtranet.Lib.Extensions {
	public static class ItemExtensions {
		
		#region CHILD BY TEMPLATE

		/// <summary>
		/// This returns the first child it finds that has the required template or a null
		/// </summary>
		/// <param name="Parent">
		/// Starting Item
		/// </param>
		/// <param name="Templatename">
		/// this is the template name of the items you want
		/// </param>
		/// <returns>
		/// Returns the first item that matches the templatename or null
		/// </returns>
		public static Item ChildByTemplate(this Item Parent, string TemplateName) {
			IEnumerable<Item> children = ChildrenByTemplate(Parent, TemplateName);
			return (children.Any()) ? children.First() : null;
		}

		public static IEnumerable<Item> ChildrenByTemplate(this Item Parent, string TemplateName) {
			return ChildrenByTemplates(Parent, new List<string> { TemplateName });
		}

		public static Item ChildByTemplates(this Item Parent, IEnumerable<string> TemplateNames) {
			IEnumerable<Item> children = ChildrenByTemplates(Parent, TemplateNames);
			return (children.Any()) ? children.First() : null;
		}

		public static IEnumerable<Item> ChildrenByTemplates(this Item Parent, IEnumerable<string> TemplateNames) {
			IEnumerable<Item> children = from child in Parent.GetChildren().ToArray()
										 where TemplateNames.Contains(child.TemplateName)
										 select child;
			return (children == null || !children.Any()) ? Enumerable.Empty<Item>() : children;
		}

		#endregion

		#region CHILD BY TEMPLATE ID

		/// <summary>
		/// This returns the first child it finds that has the required template id or a null
		/// </summary>
		/// <param name="Parent">
		/// Starting Item
		/// </param>
		/// <param name="Templatename">
		/// this is the template ID of the items you want
		/// </param>
		/// <returns>
		/// Returns the first item that matches the templateID or null
		/// </returns>
		public static Item ChildByTemplateID(this Item Parent, string TemplateID) {
			IEnumerable<Item> children = ChildrenByTemplateID(Parent, TemplateID);
			return (children.Any()) ? children.First() : null;
		}

		public static IEnumerable<Item> ChildrenByTemplateID(this Item Parent, string TemplateID) {
			return ChildrenByTemplateIDs(Parent, new List<string> { TemplateID });
		}

		public static Item ChildByTemplateIDs(this Item Parent, IEnumerable<string> TemplateIDs) {
			IEnumerable<Item> children = ChildrenByTemplateIDs(Parent, TemplateIDs);
			return (children.Any()) ? children.First() : null;
		}

		public static IEnumerable<Item> ChildrenByTemplateIDs(this Item Parent, IEnumerable<string> TemplateIDs) {
			IEnumerable<Item> children = from child in Parent.GetChildren().ToArray()
										 where TemplateIDs.Contains(child.TemplateID.ToString())
										 select child;
			return (children == null || !children.Any()) ? Enumerable.Empty<Item>() : children;
		}

		#endregion
	}
}
