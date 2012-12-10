using MarianX.Contents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MarianX.Sprites
{
	public class ScrollingBackgroundSprite : Sprite
	{
		private readonly ScrollingBackgroundAsset asset;

		public ScrollingBackgroundSprite(ScrollingBackgroundAsset asset)
			: base(asset.Name)
		{
			this.asset = asset;
		}

		public override void Load(ContentManager content)
		{
			base.Load(content);

			int x = asset.X * ActualSize.Width;
			int y = asset.Y * ActualSize.Height;
			Position = new Vector2(x, y);
		}
	}
}
