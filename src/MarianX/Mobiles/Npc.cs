using MarianX.Contents;
using MarianX.Core;

namespace MarianX.Mobiles
{
	public abstract class Npc : Mobile
	{
		protected Npc(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
			GameCore.Instance.MobileCollisionDetection.AddNpc(this);
		}
	}
}