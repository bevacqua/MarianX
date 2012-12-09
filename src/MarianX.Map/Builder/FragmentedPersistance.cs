using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MarianX.World.Platform;

namespace MarianX.Map.Builder
{
	public class FragmentedPersistance : Persistance
	{
		private readonly Func<string, FileStream> getTileFileStream;

		public FragmentedPersistance(Func<string, FileStream> getTileFileStream)
			: base(getTileFileStream)
		{
			this.getTileFileStream = getTileFileStream;
		}

		public override void SaveTileMap(TileType[,] map, string format)
		{
			// TODO: same as base, but split into many map files, save them with
			// some kind of notation, and an index file that allows to gather all of them, for the map loading.
		}
	}
}
