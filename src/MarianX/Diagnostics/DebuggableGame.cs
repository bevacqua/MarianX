using System.Collections.Generic;
using MarianX.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Core
{
	public class DebuggableGame : ContentBasedGame
	{
		private readonly Font font;
		private readonly IList<string> messages;

		public DebuggableGame()
		{
			messages = new List<string>();
			font = new Font("Fonts/Diagnostic");
		}

		public void AddDiagnosticMessage(string message, object[] args)
		{
			if (Config.Diagnostic)
			{
				messages.Add(string.Format(message, args));
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

			SpriteFont spriteFont = font;
			Vector2 position = new Vector2(8, 8);
			
			foreach (string message in messages)
			{
				spriteBatch.DrawString(spriteFont, message, position, Color.Tomato);

				position.Y += spriteFont.MeasureString(message).Y;
			}

			messages.Clear();

			spriteBatch.End();
		}
	}
}