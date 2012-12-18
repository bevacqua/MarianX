using System;
using System.IO;
using MarianX.World.Platform;

namespace MarianX.Map.Builder
{
	public class MapBuilder
	{
		private TileType[] tileTypes;

		public void BuildAndSave()
		{
			SetupDirectory();

			LevelOne one = new LevelOne();

			tileTypes = one.LoadTileTypes("../graphics/tiles/metadata.csv");

			BuildLevel(one);

			CopyFilesOverToContent();
		}

		private void BuildLevel(TileMatrixBuilder builder)
		{
			string level = string.Format("level_{0}", builder.Level);

			if (!Directory.Exists(level))
			{
				Directory.CreateDirectory(level);
			}

			TileType[,] map = builder.CreateTileMap(tileTypes);

			Func<string, FileStream> getTileFileStream = tileType =>
			{
				string path = string.Format("../graphics/tiles/{0}.png", tileType);
				return File.OpenRead(path);
			};
			// metadata actually used for rendering maps.
			FragmentedPersistance fragmentedPersistance = new FragmentedPersistance(getTileFileStream);
			fragmentedPersistance.SaveFragmentMetadata(map, string.Concat(level, "/map.idx"), builder.Level);
			fragmentedPersistance.SaveTileMap(map, string.Concat(level, "/map_{0}_{1}.png"));
			fragmentedPersistance.SaveTileMatrix(map, string.Concat(level, "/map.csv"));

			// full map for preview for viewing and debugging.
			Persistance persistance = new Persistance(getTileFileStream);
			persistance.SaveTileMap(map, string.Concat(level, "/map.png"));
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
