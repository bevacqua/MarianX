using System;
using System.Collections.Generic;
using System.IO;
using MarianX.Map.Builder.Levels;
using MarianX.Map.Imaging;
using MarianX.Map.Interface;
using MarianX.World.Platform;

namespace MarianX.Map.Builder
{
	public class MapBuilder
	{
		private TileType[] tileTypes;

		public void BuildAndSave(IList<IBuilder> levels)
		{
			if (levels == null || levels.Count == 0)
			{
				throw new ArgumentException("levels");
			}
			SetupDirectory();

			tileTypes = levels[0].LoadTileTypes("../data/tiles.csv");

			IList<Action> actions = new List<Action>();

			foreach (Builder level in levels)
			{
				Action saveFullMapPng = BuildLevel(level);
				actions.Add(saveFullMapPng);
			}

			CopyFilesOverToContent();

			foreach (Action action in actions)
			{
				action();
			}
		}

		private Action BuildLevel(IBuilder builder)
		{
			string level = string.Format("level_{0}", builder.Level);

			if (!Directory.Exists(level))
			{
				Directory.CreateDirectory(level);
			}

			TileType[,] map = builder.CreateTileMap(tileTypes);

			Func<string, FileStream> getTileFileStream = tileType =>
			{
				string path = string.Format("../data/tiles/{0}.png", tileType);
				return File.OpenRead(path);
			};

			string indexTarget = string.Concat(level, "/map.idx");
			string csvTarget = string.Concat(level, "/map.csv");
			string fragmentTarget = string.Concat(level, "/map_{0}_{1}.png");

			string metadata = string.Format(LevelBuilder.FileFormat, builder.Level, "{0}");
			string metadataRelative = string.Concat("../", metadata);
			string metadataTarget = string.Concat(level, "/map.{0}");
			string mapTarget = string.Concat(level, "/map.png");

			// metadata actually used for rendering maps.
			FragmentedPersistance fragmentedPersistance = new FragmentedPersistance(getTileFileStream);
			fragmentedPersistance.SaveFragmentMetadata(map, indexTarget, builder);
			fragmentedPersistance.SaveTileMap(map, fragmentTarget);
			fragmentedPersistance.SaveTileMatrix(map, csvTarget);

			// other metadata: just copy over.
			string npcSource = string.Format(metadataRelative, "npc");
			string npcTarget = string.Format(metadataTarget, "npc");

			string itemSource = string.Format(metadataRelative, "item");
			string itemTarget = string.Format(metadataTarget, "item");

			File.Copy(npcSource, npcTarget, true);
			File.Copy(itemSource, itemTarget, true);

			return () =>
			{
				// full map for preview for viewing and debugging.
				Persistance persistance = new Persistance(getTileFileStream);
				persistance.SaveTileMap(map, mapTarget);
			};
		}

		private void SetupDirectory()
		{
			if (!Directory.Exists("Map"))
			{
				Directory.CreateDirectory("Map");
			}

			Directory.SetCurrentDirectory("Map");
			string current = Directory.GetCurrentDirectory();
			DirectoryInfo source = new DirectoryInfo(current);
			DeleteFilesRecursively(source);
		}

		private void CopyFilesOverToContent()
		{
			string current = Directory.GetCurrentDirectory();
			string content = "../../../../MarianXContent/Map";

			DirectoryInfo source = new DirectoryInfo(current);
			DirectoryInfo target = new DirectoryInfo(content);

			if (!Directory.Exists(content))
			{
				Directory.CreateDirectory(content);
			}

			DeleteFilesRecursively(target);
			CopyFilesRecursively(source, target);
		}

		private void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
		{
			foreach (DirectoryInfo directory in source.GetDirectories())
			{
				CopyFilesRecursively(directory, target.CreateSubdirectory(directory.Name));
			}

			foreach (FileInfo file in source.GetFiles())
			{
				string path = Path.Combine(target.FullName, file.Name);
				file.CopyTo(path, true);
			}
		}

		private void DeleteFilesRecursively(DirectoryInfo directoryInfo)
		{
			foreach (FileInfo file in directoryInfo.GetFiles())
			{
				file.Delete();
			}

			foreach (DirectoryInfo subfolder in directoryInfo.GetDirectories())
			{
				DeleteFilesRecursively(subfolder);
			}
		}
	}
}
