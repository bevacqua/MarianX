using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Map.Builder.Levels
{
	public class LevelBuilder : Builder
	{
		public const string FileFormat = "data/levels/{0}/level.{1}";

		private readonly int level;
		private LevelBuilderInfo info;
		private IList<LevelBuilderRule> rules;

		public override int Level
		{
			get { return level; }
		}

		public override Vector2 StartPosition
		{
			get { return info.Start; }
		}

		protected override int Columns
		{
			get { return info.Columns; }
		}

		protected override int Rows
		{
			get { return info.Rows; }
		}

		public LevelBuilder(int level)
		{
			this.level = level;

			LoadLevelInfo();
			LoadRules();
		}

		private void LoadMetadata(string from, Action<CsvReader> action)
		{
			string path = string.Format(FileFormat, level, from);

			using (TextReader textReader = new StreamReader(path))
			using (CsvReader reader = new CsvReader(textReader, new CsvConfiguration { IsStrictMode = false }))
			{
				action(reader);
			}
		}

		private void LoadLevelInfo()
		{
			Action<CsvReader> action = reader =>
			{
				info = reader.GetRecords<LevelBuilderInfo>().First();
			};
			LoadMetadata("info", action);
		}

		private void LoadRules()
		{
			Action<CsvReader> action = reader =>
			{
				rules = reader.GetRecords<LevelBuilderRule>().ToList();

				foreach (var rule in rules)
				{
					rule.Process(info.Columns, info.Rows);
				}
			};
			LoadMetadata("rules", action);
		}

		protected override TileType SelectTileType(TileType[] tileTypes, int x, int y)
		{
			int index = 0;

			foreach (LevelBuilderRule rule in rules)
			{
				if (x >= rule.X && x < rule.Right)
				{
					if (y >= rule.Y && y < rule.Bottom)
					{
						index = rule.Tile; // don't break, so rules can be overwritten.
					}
				}
			}

			return tileTypes[index];
		}
	}
}