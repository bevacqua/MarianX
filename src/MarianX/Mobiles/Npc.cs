using MarianX.Contents;

namespace MarianX.Mobiles
{
	public abstract class Npc : Mobile
	{
		protected Npc(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
		}
	}
}