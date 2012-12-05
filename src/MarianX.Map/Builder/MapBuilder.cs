﻿using System;
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
			Persistance persistance = new Persistance(getTileFileStream);
			persistance.SaveTileMap(map, "map.png");
			persistance.SaveTileMatrix(map, "map.csv");
		}
	}
}
