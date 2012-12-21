using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AutoMapper;
using CsvHelper;
using MarianX.World.Platform;

namespace MarianX.Map.Imaging
{
	public class Persistance
	{
		private readonly Func<string, FileStream> getTileFileStream;

		public Persistance(Func<string, FileStream> getTileFileStream)
		{
			this.getTileFileStream = getTileFileStream;
		}

		public virtual void SaveTileMap(TileType[,] map, string path)
		{
			int cols = map.GetLength(0);
			int rows = map.GetLength(1);

			BuildBitmap(map, 0, 0, cols, rows, path);
		}

		protected void BuildBitmap(TileType[,] map, int startX, int startY, int width, int height, string path)
		{
			Bitmap bitmap = new Bitmap(Tile.Width * width, Tile.Height * height);
			Graphics graphics = Graphics.FromImage(bitmap);

			int lengthX = map.GetLength(0); // just for array-bounds sanity check.
			int lengthY = map.GetLength(1); // just for array-bounds sanity check.

			for (int x = startX; x < startX + width && x < lengthX; x++)
			{
				for (int y = startY; y < startY + height && y < lengthY; y++)
				{
					Fill(graphics, map[x, y], x - startX, y - startY);
				}
			}

			if (File.Exists(path))
			{
				File.Delete(path);
			}

			bitmap.Save(path, ImageFormat.Png);
		}

		protected void Fill(Graphics graphics, TileType tile, int x, int y)
		{
			using (Stream stream = getTileFileStream(tile.Type))
			{
				Image texture = new Bitmap(stream);
				TextureBrush brush = new TextureBrush(texture);
				graphics.FillRectangle(brush, x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);
			}
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
					TileRecord record = Mapper.Map<TileType, TileRecord>(type);

					record.X = x;
					record.Y = y;
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
