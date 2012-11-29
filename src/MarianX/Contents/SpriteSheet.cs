using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Contents
{
	public class SpriteSheet : Sprite
	{
		private readonly SpriteSheetSettings settings;

		private TimeSpan elapsed;
		private int frame;
		private int frameSetIndex;
		private FrameSet frameSet;

		public SpriteSheet(string assetName, SpriteSheetSettings settings)
			: base(assetName)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.settings = settings;
		}

		public override void Initialize()
		{
			SetFrameSet(0);

			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			elapsed += gameTime.ElapsedGameTime;

			if (elapsed >= frameSet.Interval)
			{
				frame++;
				elapsed = TimeSpan.Zero;
			}

			if (frame > frameSet.Count - 1 && frameSet.Loop)
			{
				frame = 0;
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			int offset = settings.Guides ? 1 : 0;
			int x = frame * (settings.Width + offset);
			int y = frameSetIndex * (settings.Height + offset);

			Rectangle sprite = new Rectangle(x, y, settings.Width, settings.Height);
			spriteBatch.Draw(Texture, Position, sprite, Tint, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
		}

		public void SetFrameSet(int index)
		{
			if (index > settings.FrameSets.Count - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			frameSetIndex = index;
			frameSet = settings.FrameSets[index];
			frame = 0;

			elapsed = TimeSpan.Zero;
		}
	}
}