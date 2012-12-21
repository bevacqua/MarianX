using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Map.Interface
{
	public interface IBuilder
	{
		int Level { get; }
		Vector2 StartPosition { get; }
		TileType[] LoadTileTypes(string path);
		TileType[,] CreateTileMap(TileType[] tileTypes);

		int ScreenTop { get; }
		int ScreenLeft { get; }
		int ScreenBottom { get; }
		int ScreenRight { get; }
	}
}