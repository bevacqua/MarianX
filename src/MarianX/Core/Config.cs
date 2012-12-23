using System.Configuration;

namespace MarianX.Core
{
	public static class Config
	{
		public static bool Diagnostic { get; set; }

		static Config()
		{
			Diagnostic = Bool(Get("Diagnostics")) ?? false;
		}

		public static int Start
		{
			get { return Int(Get("Start")) ?? 1; }
		}

		public static int Lives
		{
			get { return Int(Get("Lives")) ?? 3; }
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

		private static int? Int(string property)
		{
			int result;
			if (int.TryParse(property, out result))
			{
				return result;
			}
			return null;
		}
	}
}