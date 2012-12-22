using MarianX.Contents;

namespace MarianX.Sprites
{
	public class BackgroundSprite : Sprite
	{
		public BackgroundAsset Asset { get; private set; }

		public BackgroundSprite(BackgroundAsset asset)
			: base(asset.Name)
		{
			Asset = asset;
		}
	}
}
