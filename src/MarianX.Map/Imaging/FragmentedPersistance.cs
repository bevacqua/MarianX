using System;
using System.IO;
using MarianX.Map.Interface;
using MarianX.World.Platform;

namespace MarianX.Map.Imaging
{
	public class FragmentedPersistance : Persistance
	{
		const int fragmentWidth = 40;
		const int fragmentHeight = 30;

		public FragmentedPersistance(Func<string, FileStream> getTileFileStream)
			: base(getTileFileStream)
		{
		}

		public override void SaveTileMap(TileType[,] map, string format)
		{
			int colTotal = map.GetLength(0);
			int rowTotal = map.GetLength(1);

			int mapWidth = (int)Math.Ceiling(colTotal / (double)fragmentWidth);
			int mapHeight = (int)Math.Ceiling(rowTotal / (double)fragmentHeight);

			for (int i = 0; i < mapWidth; i++)
			{
				int x = fragmentWidth * i;
				int w = fragmentWidth;

				if (i == mapWidth - 1)
				{
					w = colTotal - fragmentWidth * i;
				}

				for (int j = 0; j < mapHeight; j++)
				{
					int y = fragmentHeight * j;
					int h = fragmentHeight;

					if (j == mapHeight - 1)
					{
						h = rowTotal - fragmentHeight * j;
					}

					string path = string.Format(format, i, j);
					BuildBitmap(map, x, y, w, h, path);
				}
			}
		}

		public void SaveFragmentMetadata(TileType[,] map, string path, IBuilder builder)
		{
			int colTotal = map.GetLength(0);
			int rowTotal = map.GetLength(1);

			int mapWidth = (int)Math.Ceiling(colTotal / (double)fragmentWidth);
			int mapHeight = (int)Math.Ceiling(rowTotal / (double)fragmentHeight);

			using (Stream stream = File.OpenWrite(path))
			using (TextWriter writer = new StreamWriter(stream))
			{
				writer.WriteLine(mapWidth);
				writer.WriteLine(mapHeight);
				writer.WriteLine(builder.Level);
				writer.WriteLine(builder.StartPosition.X);
				writer.WriteLine(builder.StartPosition.Y);
				writer.WriteLine(builder.ScreenTop);
				writer.WriteLine(builder.ScreenLeft);
				writer.WriteLine(builder.ScreenBottom);
				writer.WriteLine(builder.ScreenRight);
			}
		}
	}
}
