using System;
using System.Web;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace ui.Helpers
{
	public static class Options
	{
		public static JsonSerializerOptions JsonOptions()
		{
			var options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
				WriteIndented = true
			};

			return options;
		}
	}
}