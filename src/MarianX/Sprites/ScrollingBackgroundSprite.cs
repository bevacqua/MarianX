using MarianX.Contents;

namespace MarianX.Sprites
{
	public class ScrollingBackgroundSprite : Sprite
	{
		public ScrollingBackgroundAsset Asset { get; private set; }

		public ScrollingBackgroundSprite(ScrollingBackgroundAsset asset)
			: base(asset.Name)
		{
			Asset = asset;
		}
	}
}
