using MarianX.Contents;
using MarianX.World.Configuration;

namespace MarianX.Mobiles
{
	public class Gloop : Npc
	{
		private static readonly SpriteSheetSettings settings;

		private const string AssetName = "Npc/gloop";

		static Gloop()
		{
			settings = new SpriteSheetSettings
			{
				Width = MagicNumbers.FrameWidth,
				Height = MagicNumbers.FrameHeight
			};
		}

		public Gloop()
			: base(AssetName, settings)
		{
		}
	}
}