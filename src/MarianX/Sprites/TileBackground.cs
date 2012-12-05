using System.Collections.Generic;
using MarianX.Core;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class TileBackground : ScrollingBackground
	{
		private static readonly IList<string> backgroundAssets = new[]
		{
			"Backgrounds/tilematrix"
		};

		public TileBackground()
			: base(backgroundAssets)
		{
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);
			DrawDiagnosticTileGrid(spriteBatch);
		}

		private void DrawDiagnosticTileGrid(SpriteBatch spriteBatch)
		{
			if (!Config.DiagnosticBackground)
			{
				return;
			}
			spriteBatch.Begin();

			TileMatrix matrix = TileMatrix.Instance;

			int w = Tile.Width - 1;
			int h = Tile.Height - 1;

			foreach (Tile tile in matrix.Tiles)
			{
				int x = tile.Position.X + 1;
				int y = tile.Position.Y + 1;

				Square metadata = new Square
				{
					Alpha = 0.2f,
					Color = tile.Impassable ? Color.IndianRed : Color.LimeGreen,
					Bounds = new Rectangle(x, y, w, h)
				};

				Vector2 position = new Vector2(x, y);
				metadata.Draw(spriteBatch, position);
			}

			spriteBatch.End();
		}
	}
}