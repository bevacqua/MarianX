using MarianX.Contents;
using MarianX.Core;

namespace MarianX.Mobiles.NPC
{
	public abstract class Npc : Mobile
	{
		protected Npc(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
			GameCore.Instance.DynamicCollisionDetection.TrackNpc(this);
		}
	}
}