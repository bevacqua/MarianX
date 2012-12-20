using AutoMapper;

namespace MarianX.World.Platform
{
	public class TileType
	{
		public string Type { get; set; }
		public bool Impassable { get; set; }

		public int SlopeLeft { get; set; }
		public int SlopeRight { get; set; }

		public string Sound { get; set; }

		public bool Deathly { get; set; }
		public bool Clear { get; set; }

		static TileType()
		{
			Mapper.CreateMap<TileType, TileRecord>();
		}
	}
}