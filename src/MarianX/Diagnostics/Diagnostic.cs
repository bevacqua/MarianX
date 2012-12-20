using MarianX.Core;
using Microsoft.Xna.Framework;

namespace MarianX.Diagnostics
{
	public static class Diagnostic
	{
		public static void Write(string title, string message, params object[] args)
		{
			GameCore.Instance.AddDiagnosticMessage(title, message, args);
		}

		public static void Write<TEnum>(string title, TEnum @enum) where TEnum : struct
		{
			 Write(title, @enum.ToString());
		}

		public static void Write(string title, Vector2 vector)
		{
			Write(title, "X:{0:+0000.00;-0000.00} Y:{1:+0000.00;-0000.00}", vector.X, vector.Y);
		}
	}
}