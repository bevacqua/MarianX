using System;
using System.IO;
using MarianX.World.Platform;

namespace MarianX.Map.Builder
{
	public class MapBuilder
	{
		public void BuildAndSave()
		{
			TileMatrixBuilder matrixBuilder = new TileMatrixBuilder();
			TileType[] tileTypes = matrixBuilder.LoadTileTypes("graphics/tiles/metadata.csv");
			TileType[,] map = matrixBuilder.CreateTileMap(tileTypes);

			Func<string, FileStream> getTileFileStream = tileType =>
			{
				string path = string.Format("graphics/tiles/{0}.png", tileType);
				return File.OpenRead(path);
			};
			FragmentedPersistance fragmentedPersistance = new FragmentedPersistance(getTileFileStream);
			fragmentedPersistance.SaveFragmentMetadata(map, "map.idx");
			fragmentedPersistance.SaveTileMap(map, "map_{0}_{1}.png");
			fragmentedPersistance.SaveTileMatrix(map, "map.csv");
			Persistance persistance = new Persistance(getTileFileStream);
			persistance.SaveTileMap(map, "map.png"); // full map for preview.
		}
	}
}
