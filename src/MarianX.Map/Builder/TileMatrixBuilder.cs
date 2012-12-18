using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Map.Builder
{
	public abstract class TileMatrixBuilder
	{
		public abstract int Level { get; }
		public abstract Vector2 StartPosition { get; }
		protected abstract int Columns { get; }
		protected abstract int Rows { get; }

		public TileType[] LoadTileTypes(string path)
		{
			using (Stream stream = File.OpenRead(path))
			using (TextReader textReader = new StreamReader(stream))
			using (CsvReader csvReader = new CsvReader(textReader))
			{
				IEnumerable<TileType> records = csvReader.GetRecords<TileType>();
				return records.ToArray();
			}
		}

		public TileType[,] CreateTileMap(TileType[] tileTypes)
		{
			var map = new TileType[Columns, Rows];

			for (int x = 0; x < Columns; x++)
			{
				for (int y = 0; y < Rows; y++)
				{
					TileType tile = SelectTileType(tileTypes, x, y);

					map[x, y] = tile;
				}
			}

			return map;
		}

		protected abstract TileType SelectTileType(TileType[] tileTypes, int x, int y);
	}
}
