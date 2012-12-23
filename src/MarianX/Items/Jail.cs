using MarianX.Contents;
using MarianX.Mobiles.Player;
using MarianX.Physics;
using MarianX.World.Configuration;
using Microsoft.Xna.Framework.Content;

namespace MarianX.Items
{
	public class Jail : Item
	{
		private const string AssetName = "Items/jail";

		private static readonly SpriteSheetSettings settings;

		static Jail()
		{
			settings = new SpriteSheetSettings
			{
				Width = MagicNumbers.JailFrameWidth,
				Height = MagicNumbers.JailFrameHeight
			};
		}

		private readonly FrameSet frameSet = new FrameSet { Row = 0, Frames = 6 };

		public Jail()
			: base(AssetName, settings)
		{
			BoundingBox = new BoundingBox2(MagicNumbers.JailHitBoxWidth, MagicNumbers.JailHitBoxHeight);
		}

		public override void Load(ContentManager content)
		{
			base.Load(content);
			SetFrameSet(frameSet);
		}

		public override void PickUp(Marian marian)
		{
			marian.BreakOut();
		}
	}
}