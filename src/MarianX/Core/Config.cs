using System.Configuration;

namespace MarianX.Core
{
	public static class Config
	{
		public static bool Diagnostic { get; set; }

		static Config()
		{
			Diagnostic = Bool(Get("Diagnostic")) ?? false;
		}

		private static string Get(string property)
		{
			return ConfigurationManager.AppSettings[property];
		}

		private static bool? Bool(string property)
		{
			bool result;
			if (bool.TryParse(property, out result))
			{
				return result;
			}
			return null;
		}
	}
}