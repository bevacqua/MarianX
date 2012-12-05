using AutoMapper;
using Microsoft.Xna.Framework;

namespace MarianX.World.Platform
{
	public class TileRecord : TileType
	{
		public int X { get; set; }
		public int Y { get; set; }

		static TileRecord()
		{
			Mapper.CreateMap<TileRecord, Tile>().ForMember(
				dest => dest.Position,
				opt => opt.MapFrom(src => new Point(src.X * Tile.Width, src.Y * Tile.Height)));
		}
	}
}