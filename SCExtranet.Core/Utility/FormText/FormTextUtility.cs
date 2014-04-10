using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sitecore.Configuration;

namespace SCExtranet.Core.Utility.FormText {
	public class FormTextUtility {

		private static IFormTextProvider GetProvider() {
			IEnumerable<XmlNode> nodes = from XmlNode n in Factory.GetConfigNode("scextranet").ChildNodes
										 where n.Name.Equals("formText")
										 select n;
			if (!nodes.Any())
				throw new Sitecore.Exceptions.ProviderConfigurationException("Form Text xml node not found");

			XmlAttribute xac = nodes.First().Attributes["providerType"];
			if(xac == null || string.IsNullOrEmpty(xac.Value))
				throw new Sitecore.Exceptions.ProviderConfigurationException("Form Text Provider type not found");

			object o = Sitecore.Reflection.ReflectionUtil.CreateObject(xac.Value);
			if(o == null)
				throw new Sitecore.Exceptions.ProviderConfigurationException(string.Format("Could not instantiate Form Text Provider: {0}", xac.Value));
			if(!(o is IFormTextProvider))
				throw new Sitecore.Exceptions.ProviderConfigurationException("Form Text Provider was not of type SCExtranet.Core.IFormTextProvider");
			
			return (IFormTextProvider)o;
		}

		public static IFormTextProvider Provider {
			get {
				return GetProvider();
			}
		}
	}
}
