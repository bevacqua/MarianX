using MarianX.Contents;
using MarianX.Diagnostics;
using MarianX.Enum;
using MarianX.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Core
{
	public abstract class PausableGame : DebuggableGame
	{
		private readonly Font titleFont;
		private readonly Font regularFont;

		private bool gameOver = true;
		private KeyboardState oldState;

		public PausableGame()
		{
			titleFont = new Font("Fonts/Title");
			regularFont = new Font("Fonts/Regular");
		}

		public virtual void AddPersistantContent()
		{
			AddContent(titleFont);
			AddContent(regularFont);
		}

		public void GameOver()
		{
			gameOver = true;
			oldState = Keyboard.GetState();
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			if (gameOver)
			{
				DrawGameOver();
			}
		}

		private void DrawGameOver()
		{
			string title = "SIAMO FUORI!";
			string legend = "(premere Enter per ricominciare)";

			Square square = new Square
			{
				Alpha = 0.7f,
				Bounds = GraphicsDevice.Viewport.Bounds,
				Color = Color.DarkRed
			};

			spriteBatch.Begin();
			square.Draw(spriteBatch, Vector2.Zero);
			DrawText(title, titleFont, new Vector2(0, -30), Color.BlanchedAlmond);
			DrawText(legend, regularFont, new Vector2(200, 25), Color.AliceBlue);
			spriteBatch.End();
		}

		private void DrawText(string text, Font font, Vector2 offset, Color color)
		{
			Point p = GraphicsDevice.Viewport.Bounds.Center;
			Vector2 measure = titleFont.MeasureString(text);

			float x = p.X - measure.X / 2;
			float y = p.Y - measure.Y / 2;
			Vector2 position = new Vector2(x, y);

			font.Write(text, position + offset, spriteBatch, color);
		}

		protected override void UpdateInput(GameTime gameTime)
		{
			PausedInput();

			if (gameOver)
			{
				return;
			}
			base.UpdateInput(gameTime);
		}

		private void PausedInput()
		{
			if (!gameOver)
			{
				return;
			}
			KeyboardState keyboardState = Keyboard.GetState();
			KeyboardConfiguration kb = new KeyboardConfiguration(keyboardState);

			if (kb.IsKeyPressed(ActionKey.Resume, oldState))
			{
				gameOver = false;
				Restart();
			}
		}

		protected abstract void Restart();

		protected override void UpdateOutput(GameTime gameTime)
		{
			if (gameOver)
			{
				return;
			}
			base.UpdateOutput(gameTime);
		}
	}
}