using System.Collections.Generic;
using System.IO;
using MarianX.Core;
using MarianX.Geometry;
using MarianX.Interface;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Backgrounds
{
	public abstract class MapBackground : IGameContent
	{
		private readonly Background background;

		public Vector2 Start { get; private set; }
		public int ScreenTop { get; private set; }
		public int ScreenLeft { get; private set; }
		public int ScreenBottom { get; private set; }
		public int ScreenRight { get; private set; }

		protected MapBackground(string path, string format)
		{
			IList<BackgroundAsset> assetNames = ReadInputFile(path, format);
			background = new Background(assetNames);
		}

		private IList<BackgroundAsset> ReadInputFile(string path, string nameFormat)
		{
			IList<BackgroundAsset> assetNames = new List<BackgroundAsset>();

			using (Stream stream = File.OpenRead(path))
			using (TextReader reader = new StreamReader(stream))
			{
				int width = ReadInt(reader);
				int height = ReadInt(reader);

				int level = ReadInt(reader);

				float xS = ReadFloat(reader);
				float yS = ReadFloat(reader);

				Start = new Vector2(xS, yS);

				ScreenTop = ReadInt(reader);
				ScreenLeft = ReadInt(reader);
				ScreenBottom = ReadInt(reader);
				ScreenRight = ReadInt(reader);

				for (int x = 0; x < width; x++)
				{
					for (int y = 0; y < height; y++)
					{
						string name = string.Format(nameFormat, level, x, y);
						assetNames.Add(new BackgroundAsset
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

		private int ReadInt(TextReader reader)
		{
			string line = reader.ReadLine();
			return int.Parse(line);
		}

		private float ReadFloat(TextReader reader)
		{
			string line = reader.ReadLine();
			return float.Parse(line);
		}

		public void Initialize()
		{
			background.Initialize();
		}

		public void Load(ContentManager content)
		{
			background.Load(content);
		}

		public void UpdateInput(GameTime gameTime)
		{
			background.UpdateInput(gameTime);
		}

		public void UpdateOutput(GameTime gameTime)
		{
			background.UpdateOutput(gameTime);
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (Config.Diagnostic)
			{
				DrawDiagnosticTileGrid(spriteBatch);
				return;
			}
			background.Draw(gameTime, spriteBatch);
		}

		public void Unload()
		{
			background.Unload();
		}

		public void UpdateScreenPosition(Vector2 screenPosition)
		{
			background.UpdateScreenPosition(screenPosition);
		}

		public Vector2 Position
		{
			get { return background.Position; }
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