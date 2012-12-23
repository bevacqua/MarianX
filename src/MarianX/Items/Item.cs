using MarianX.Contents;
using MarianX.Core;
using MarianX.Mobiles;

namespace MarianX.Items
{
	public abstract class Item : Mobile
	{
		protected Item(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
			GameCore.Instance.DynamicCollisionDetection.TrackItem(this);
		}
	}
}