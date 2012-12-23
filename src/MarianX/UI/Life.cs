using MarianX.Contents;
using MarianX.Core;
using MarianX.Mobiles.Player;
using MarianX.World.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.UI
{
	public class Life : SpriteSheet
	{
		private static readonly SpriteSheetSettings settings = new SpriteSheetSettings
		{
			Width = MagicNumbers.FrameWidth,
			Height = MagicNumbers.FrameHeight
		};

		private static readonly FrameSet frameSet = new FrameSet { Frames = 1 };

		public Life(int position)
			: base(Marian.AssetName, settings)
		{
			int right = GameCore.Instance.GraphicsDevice.Viewport.Width;

			int x = right - MagicNumbers.LifeRight - position * MagicNumbers.LifeWidth;
			int y = MagicNumbers.LifeTop;

			StartPosition = new Vector2(x, y);
		}

		protected Vector2 StartPosition { get; set; }

		public override void Initialize()
		{
			base.Initialize();

			Tint = Color.Pink;
			Scale = 0.65f;
			ScreenPosition = StartPosition;
		}

		public override void Load(ContentManager content)
		{
			base.Load(content);
			SetFrameSet(frameSet);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			base.Draw(gameTime, spriteBatch);
			spriteBatch.End();
		}
	}
}