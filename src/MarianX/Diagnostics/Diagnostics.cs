using MarianX.Core;

namespace MarianX.Diagnostics
{
	public static class Diagnostics
	{
		public static void Write(string message, params object[] args)
		{
			GameCore.Instance.AddDiagnosticMessage(message, args);
		}
	}
}