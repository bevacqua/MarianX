using MarianX.Contents;
using MarianX.Core;
using MarianX.Mobiles;
using Microsoft.Xna.Framework;

namespace MarianX.Items
{
	public abstract class Item : Mobile
	{
		public bool Static { get; protected set; }

		public Vector2 StartPosition { get; set; }

		protected Item(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
			GameCore.Instance.DynamicCollisionDetection.TrackItem(this);
			Move += GameCore.Instance.ViewportManager.OnMobileMoved;
		}

		protected override Vector2 CalculateInterpolation(GameTime gameTime)
		{
			if (Static)
			{
				return Vector2.Zero;
			}
			return base.CalculateInterpolation(gameTime);
		}

		public override void Initialize()
		{
			base.Initialize();

			Position = StartPosition;
		}
	}
}