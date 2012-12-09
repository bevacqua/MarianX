using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using MarianX.World.Platform;

namespace MarianX.Map.Builder
{
	public class TileMatrixBuilder
	{
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
			int cols = 33 * 6;
			int rows = 45;

			var map = new TileType[cols, rows];

			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					TileType tile = SelectTileType(tileTypes, x, y, cols, rows);

					map[x, y] = tile;
				}
			}

			return map;
		}

		private TileType SelectTileType(TileType[] tileTypes, int x, int y, int cols, int rows)
		{
			TileType tile = tileTypes[0];

			if (x > 100 && x < 106)
			{
				return tile;
			}
			if (y == rows - 22)
			{
				tile = tileTypes[2];
			}
			else if (y == rows - 21)
			{
				tile = tileTypes[1];
			}
			else if ((x > 35 && x < 55) || (x > 60 && x < 110))
			{
				if (y == rows - 3)
				{
					tile = tileTypes[3];
				}
				else if (y == rows - 4)
				{
					tile = tileTypes[4];
				}
			}

			return tile;
		}
	}
}
