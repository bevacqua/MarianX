using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using MarianX.World.Interface;
using MarianX.World.Physics;
using Microsoft.Xna.Framework;

namespace MarianX.World.Platform
{
	public sealed class TileMatrix : TileMatrixMetadata
	{
		private static TileMatrix instance;

		public static TileMatrix Instance
		{
			get { return instance; }
		}

		public static void Use(ILevel metadata)
		{
			string format = "Content/Map/level_{0}/map.csv";
			string path = string.Format(format, metadata.Level);
			instance = new TileMatrix(path, metadata.Start)
			{
				Metadata = metadata
			};
		}

		public ILevel Metadata { get; private set; }

		private TileMatrix(string path, Vector2 start)
			: base(path, start)
		{
		}

		public IList<Tile> Intersect(FloatRectangle bounds)
		{
			IList<Tile> intersection = new List<Tile>();

			// calculate possible tile bounds to improve performance.
			int colStart = Math.Max(0, (int)bounds.Left / Tile.Width - 1);
			int colItems = (int)bounds.Width / Tile.Width + 2;
			int colLength = Math.Min(Tiles.GetLength(0), colStart + colItems + 1);

			int rowStart = Math.Max(0, (int)bounds.Top / Tile.Height - 1);
			int rowItems = (int)bounds.Height / Tile.Height + 2;
			int rowLength = Math.Min(Tiles.GetLength(1), rowStart + rowItems + 1);

			for (int x = colStart; x < colLength; x++)
			{
				for (int y = rowStart; y < rowLength; y++)
				{
					Tile tile = Tiles[x, y];

					var intersects = bounds.Intersects(tile.Bounds);
					if (intersects)
					{
						intersection.Add(tile);
					}
				}
			}

			return intersection;
		}

		public static IList<NpcRecord> GetNpcLocations()
		{
			string format = "content/map/level_{0}/map.npc";
			string path = string.Format(format, Instance.Metadata.Level);
			using(StreamReader streamReader = new StreamReader(path))
			using (CsvReader csvReader = new CsvReader(streamReader))
			{
				return csvReader.GetRecords<NpcRecord>().ToList();
			}
		}

		public static IList<ItemRecord> GetItemMetadata()
		{
			string format = "content/map/level_{0}/map.item";
			string path = string.Format(format, Instance.Metadata.Level);
			using (StreamReader streamReader = new StreamReader(path))
			using (CsvReader csvReader = new CsvReader(streamReader))
			{
				return csvReader.GetRecords<ItemRecord>().ToList();
			}
		}
	}
}