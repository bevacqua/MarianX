using MarianX.Contents;

namespace MarianX.Backgrounds
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
