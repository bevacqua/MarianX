using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CsvHelper;

namespace MarianX.World.Platform
{
	public class TileMatrixMetadata
	{
		/// <summary>
		/// Columns, then Rows.
		/// </summary>
		public Tile[,] Tiles { get; private set; }

		public TileMatrixMetadata(string path)
		{
			using (Stream stream = File.OpenRead(path))
			using (TextReader textReader = new StreamReader(stream))
			using (CsvReader csvReader = new CsvReader(textReader))
			{
				IList<TileRecord> records = csvReader.GetRecords<TileRecord>().ToArray();

				int cols = records.Max(t => t.X) + 1;
				int rows = records.Max(t => t.Y) + 1;

				Tiles = new Tile[cols, rows];

				foreach (TileRecord record in records)
				{
					Tile tile = Mapper.Map<TileRecord, Tile>(record);

					Tiles[record.X, record.Y] = tile;
				}
			}
		}
	}
}