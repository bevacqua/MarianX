using System.Configuration;

namespace MarianX.Core
{
	public static class Config
	{
		public static bool Diagnostic
		{
			get { return Bool(Get("Diagnostic")) ?? false; }
		}

		public static bool DiagnosticBackground
		{
			get { return Bool(Get("DiagnosticBackground")) ?? false; }
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