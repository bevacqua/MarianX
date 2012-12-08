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
			"map"
		};

		public TileBackground()
			: base(backgroundAssets)
		{
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (Config.DiagnosticBackground)
			{
				DrawDiagnosticTileGrid(spriteBatch);
				return;
			}
			base.Draw(gameTime, spriteBatch);
		}

		private SquareGrid diagnosticTextureGrid;

		private void DrawDiagnosticTileGrid(SpriteBatch spriteBatch)
		{
			if (diagnosticTextureGrid == null)
			{
				IList<Square> squares = new List<Square>();
				TileMatrix matrix = TileMatrix.Instance;

				int w = Tile.Width - 1;
				int h = Tile.Height - 1;

				foreach (Tile tile in matrix.Tiles)
				{
					int x = tile.Position.X + 1;
					int y = tile.Position.Y + 1;

					Square square = new Square
					{
						Alpha = 0.65f,
						Color = tile.Impassable ? Color.IndianRed : Color.LimeGreen,
						Bounds = new Rectangle(x, y, w, h)
					};
					squares.Add(square);
				}
				diagnosticTextureGrid = new SquareGrid(squares);
			}
			
			spriteBatch.Begin();
			diagnosticTextureGrid.Draw(spriteBatch, Vector2.Zero);
			spriteBatch.End();
		}
	}
}