using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CopyPasteKiller
{
	public static class XmlUtil
	{
		public static string ConvertToXml(object item)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				xmlSerializer.Serialize(memoryStream, item);
				UTF8Encoding uTF8Encoding = new UTF8Encoding();
				@string = uTF8Encoding.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		public static T FromXml<T>(string xml)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			T result;
			using (StringReader stringReader = new StringReader(xml))
			{
				result = (T)((object)xmlSerializer.Deserialize(stringReader));
			}
			return result;
		}
	}
}
