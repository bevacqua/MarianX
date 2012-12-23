using System.Collections.Generic;
using MarianX.Contents;
using MarianX.Core;
using Microsoft.Xna.Framework;

namespace MarianX.Diagnostics
{
	public class DiagnosticGame : ContentBasedGame
	{
		private readonly Font font;
		private readonly IDictionary<string, string> messages;

		public DiagnosticGame()
		{
			messages = new Dictionary<string, string>();
			font = new Font("Fonts/Diagnostics");
		}

		public void AddDiagnosticMessage(string title, string message, object[] args)
		{
			if (Config.Diagnostic)
			{
				messages[title] = string.Format(message, args);
			}
		}

		protected override void Initialize()
		{
			AddContent(font);
			base.Initialize();
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			if (!Config.Diagnostic)
			{
				return;
			}

			spriteBatch.Begin();

			Vector2 position = new Vector2(8, 8);

			foreach (var entry in messages)
			{
				string message = string.Concat(entry.Key, " ", entry.Value);

				font.Write(message, position, spriteBatch, Color.DarkViolet);

				position.Y += font.MeasureString(message).Y;
			}

			spriteBatch.End();
		}
	}
}