using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CsvHelper;
using MarianX.World.Platform;

namespace MarianX.Map.Builder
{
	public class Persistance
	{
		private readonly Func<string, FileStream> getTileFileStream;

		public Persistance(Func<string, FileStream> getTileFileStream)
		{
			this.getTileFileStream = getTileFileStream;
		}

		public void SaveTileMap(TileType[,] map, string path)
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
					
					using (Stream stream = getTileFileStream(tile.Type))
					{
						Image texture = new Bitmap(stream);
						TextureBrush brush = new TextureBrush(texture);
						graphics.FillRectangle(brush, x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);
					}
				}
			}

			if (File.Exists(path))
			{
				File.Delete(path);
			}

			bitmap.Save(path, ImageFormat.Png);
		}

		public void SaveTileMatrix(TileType[,] map, string path)
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

			if (File.Exists(path))
			{
				File.Delete(path);
			}

			using (Stream stream = File.OpenWrite(path))
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
