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
	public abstract class MapBackground : IGameContent
	{
		private readonly ScrollingBackground scrollingBackground;

		public Vector2 Start { get; private set; }

		protected MapBackground(string path, string format)
		{
			IList<ScrollingBackgroundAsset> assetNames = FindAssetNames(path, format);
			scrollingBackground = new ScrollingBackground(assetNames);
		}

		private IList<ScrollingBackgroundAsset> FindAssetNames(string path, string nameFormat)
		{
			IList<ScrollingBackgroundAsset> assetNames = new List<ScrollingBackgroundAsset>();

			using (Stream stream = File.OpenRead(path))
			using (TextReader reader = new StreamReader(stream))
			{
				string width = reader.ReadLine();
				string height = reader.ReadLine();
				string level = reader.ReadLine();
				string startX = reader.ReadLine();
				string startY = reader.ReadLine();

				int w = int.Parse(width);
				int h = int.Parse(height);

				float xS = float.Parse(startX);
				float yS = float.Parse(startY);

				Start = new Vector2(xS, yS);

				for (int x = 0; x < w; x++)
				{
					for (int y = 0; y < h; y++)
					{
						string name = string.Format(nameFormat, level, x, y);
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

		public void UpdateInput(GameTime gameTime)
		{
			scrollingBackground.UpdateInput(gameTime);
		}

		public void UpdateOutput(GameTime gameTime)
		{
			scrollingBackground.UpdateOutput(gameTime);
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

		private ISquareGrid diagnosticTextureGrid;

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
				diagnosticTextureGrid = new FragmentedSquareGrid(squares);
			}

			spriteBatch.Begin();
			diagnosticTextureGrid.Draw(spriteBatch);
			spriteBatch.End();
		}
	}
}