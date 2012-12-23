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

		private readonly Random random;
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

		public override int ScreenTop
		{
			get { return info.ScreenTop; }
		}

		public override int ScreenLeft
		{
			get { return info.ScreenLeft; }
		}

		public override int ScreenBottom
		{
			get { return info.ScreenBottom; }
		}

		public override int ScreenRight
		{
			get { return info.ScreenRight; }
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
			random = new Random();

			this.level = level;

			LoadLevelInfo();
			LoadRules();
		}

		private void LoadMetadata(string from, Action<CsvReader> action)
		{
			string path = string.Format(FileFormat, level, from);

			using (TextReader textReader = new StreamReader(path))
			{
				CsvConfiguration config = new CsvConfiguration
				{
					IsStrictMode = false,
					SkipEmptyRecords = true
				};
				using (CsvReader reader = new CsvReader(textReader, config))
				{
					action(reader);
				}
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

				foreach (LevelBuilderRule rule in rules)
				{
					rule.Process(info.Columns, info.Rows);
				}
			};
			LoadMetadata("rules", action);
		}

		protected override TileType SelectTileType(TileType[] tileTypes, int x, int y)
		{
			TileType tile = tileTypes[0];

			foreach (LevelBuilderRule rule in rules)
			{
				if (x >= rule.X && x < rule.Right)
				{
					if (y >= rule.Y && y < rule.Bottom)
					{
						if (rule.Incidence.HasValue)
						{
							var incidence = rule.Incidence.Value;
							if (incidence < random.NextDouble())
							{
								continue;
							}
						}

						tile = ParseTileFromRule(rule, tileTypes);
					}
				}
			}

			return tile;
		}

		private TileType ParseTileFromRule(LevelBuilderRule rule, TileType[] tileTypes)
		{
			int index;

			if (int.TryParse(rule.Tile, out index))
			{
				return tileTypes[index];
			}

			return tileTypes.First(t => t.Type == rule.Tile);
		}
	}
}