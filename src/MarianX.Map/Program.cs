using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using CsvHelper;
using MarianX.World.Platform;

namespace MarianX.Map
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			TileType[] tileTypes = LoadTileTypes();
			TileType[,] map = CreateTileMap(tileTypes);
			SaveTileMap(map);
			SaveTileMatrix(map);
		}

		private static TileType[] LoadTileTypes()
		{
			using (Stream stream = File.OpenRead("graphics/tiles/metadata.csv"))
			using (TextReader textReader = new StreamReader(stream))
			using (CsvReader csvReader = new CsvReader(textReader))
			{
				IEnumerable<TileType> records = csvReader.GetRecords<TileType>();
				return records.ToArray();
			}
		}

		private static TileType[,] CreateTileMap(TileType[] tileTypes)
		{
			int cols = 20;
			int rows = 20;
			var map = new TileType[cols, rows];

			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					TileType tile = tileTypes[0];

					if (y == rows - 2)
					{
						tile = tileTypes[2];
					}
					else if (y == rows - 1)
					{
						tile = tileTypes[1];
					}

					map[x, y] = tile;
				}
			}

			return map;
		}

		private static void SaveTileMap(TileType[,] map)
		{
			int cols = map.GetLength(0);
			int rows = map.GetLength(1);

			Bitmap bitmap = new Bitmap(Tile.Width * cols, Tile.Height * rows);
			Graphics graphics = Graphics.FromImage(bitmap);

			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					TileType tile = map[x, y];
					string path = string.Format("graphics/tiles/{0}.png", tile.Type);

					using (Stream stream = File.OpenRead(path))
					{
						Image texture = new Bitmap(stream);
						TextureBrush brush = new TextureBrush(texture);
						graphics.FillRectangle(brush, x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);
					}
				}
			}

			string png = "map.png";

			if (File.Exists(png))
			{
				File.Delete(png);
			}

			bitmap.Save(png, ImageFormat.Png);
		}

		private static void SaveTileMatrix(TileType[,] map)
		{
			IList<TileRecord> records = new List<TileRecord>();

			int cols = map.GetLength(0);
			int rows = map.GetLength(1);

			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					TileType type = map[x, y];
					TileRecord record = new TileRecord
					{
						Impassable = type.Impassable,
						Type = type.Type,
						SlopeLeft = type.SlopeLeft,
						SlopeRight = type.SlopeRight,
						Sound = type.Sound,
						X = x,
						Y = y
					};

					records.Add(record);
				}
			}

			string csv = "map.csv";

			if (File.Exists(csv))
			{
				File.Delete(csv);
			}

			using (Stream stream = File.OpenWrite(csv))
			using (TextWriter textWriter = new StreamWriter(stream))
			using (CsvWriter csvWriter = new CsvWriter(textWriter))
			{
				foreach (TileRecord record in records)
				{
					csvWriter.WriteRecord(record);
				}
			}
		}
	}
}
