using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Contents
{
	public class SpriteSheet : Sprite
	{
		private readonly SpriteSheetSettings settings;
		private readonly Queue<int> frameSetQueue;

		private TimeSpan elapsed;
		private int frame;
		private FrameSet frameSet;

		public SpriteSheet(string assetName, SpriteSheetSettings settings)
			: base(assetName)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.settings = settings;

			frameSetQueue = new Queue<int>();
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

			if (frame > frameSet.Frames - 1)
			{
				if (frameSetQueue.Count != 0)
				{
					int next = frameSetQueue.Dequeue();
					SetFrameSet(next);
				}
				else if (frameSet.Loop)
				{
					frame = 0;
				}
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			int offset = settings.Guides ? 1 : 0;
			int x = frame * (settings.Width + offset);
			int y = frameSet.Row * (settings.Height + offset);

			Rectangle sprite = new Rectangle(x, y, settings.Width, settings.Height);
			spriteBatch.Draw(Texture, Position, sprite, Tint, 0.0f, Vector2.Zero, Scale, frameSet.Effects, 0.0f);
		}

		public void SetFrameSet(int index)
		{
			if (index > settings.FrameSets.Count - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			frameSet = settings.FrameSets[index];
			frame = 0;

			elapsed = TimeSpan.Zero;
		}

		public void SetFrameSetQueue(int[] indexes, bool clearQueue = true)
		{
			if (indexes.Length == 0)
			{
				throw new ArgumentOutOfRangeException("indexes");
			}
			if (clearQueue)
			{
				frameSetQueue.Clear();
			}
			foreach (int index in indexes)
			{
				frameSetQueue.Enqueue(index);
			}
			int next = frameSetQueue.Dequeue();
			SetFrameSet(next);
		}
	}
}