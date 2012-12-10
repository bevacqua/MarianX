using System.Collections.Generic;
using System.IO;
using MarianX.Core;
using MarianX.Interface;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class MapBackground : IGameContent
	{
		private readonly ScrollingBackground scrollingBackground;

		public MapBackground(string path, string format)
		{
			IList<ScrollingBackgroundAsset> assetNames = FindAssetNames(path, format);
			scrollingBackground = new ScrollingBackground(assetNames);
		}

		private IList<ScrollingBackgroundAsset> FindAssetNames(string path, string format)
		{
			IList<ScrollingBackgroundAsset> assetNames = new List<ScrollingBackgroundAsset>();

			using (Stream stream = File.OpenRead(path))
			using (TextReader reader = new StreamReader(stream))
			{
				string width = reader.ReadLine();
				string height = reader.ReadLine();

				int w = int.Parse(width);
				int h = int.Parse(height);

				for (int x = 0; x < w; x++)
				{
					for (int y = 0; y < h; y++)
					{
						string name = string.Format(format, x, y);
						assetNames.Add(new ScrollingBackgroundAsset
						{
							Name = name,
							X = x,
							Y = y
						});
					}
				}
			}

			return assetNames;
		}

		public void Initialize()
		{
			scrollingBackground.Initialize();
		}

		public void Load(ContentManager content)
		{
			scrollingBackground.Load(content);
		}

		public void Update(GameTime gameTime)
		{
			scrollingBackground.Update(gameTime);
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (Config.Diagnostic)
			{
				DrawDiagnosticTileGrid(spriteBatch);
				return;
			}
			scrollingBackground.Draw(gameTime, spriteBatch);
		}

		public void Unload()
		{
			scrollingBackground.Unload();
		}

		public void UpdateScreenPosition(Vector2 screenPosition)
		{
			scrollingBackground.UpdateScreenPosition(screenPosition);
		}

		public Vector2 Position
		{
			get { return scrollingBackground.Position; }
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
			diagnosticTextureGrid.Draw(spriteBatch);
			spriteBatch.End();
		}
	}
}