using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Contents
{
	public class SpriteSheet : Sprite
	{
		private readonly SpriteSheetSettings settings;
		private readonly Queue<FrameSet> frameSetQueue;

		private TimeSpan elapsed;
		private int frame;
		private FrameSet frameSet;

		public override int ContentWidth
		{
			get { return settings.Width; }
		}

		public override int ContentHeight
		{
			get { return settings.Height; }
		}

		public SpriteSheet(string assetName, SpriteSheetSettings settings)
			: base(assetName)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.settings = settings;

			frameSetQueue = new Queue<FrameSet>();
		}

		public override void Update(GameTime gameTime)
		{
			elapsed += gameTime.ElapsedGameTime;

			if (elapsed >= frameSet.Interval)
			{
				frame++;
				elapsed = TimeSpan.Zero;
			}

			if (frame > frameSet.Length - 1)
			{
				if (frameSetQueue.Count != 0)
				{
					FrameSet next = frameSetQueue.Dequeue();
					SetFrameSet(next);
				}
				else if (frameSet.Loop)
				{
					frame = frameSet.Start;
				}
				else
				{
					frame--; // stay on last frame.
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

		public void SetFrameSet(FrameSet set)
		{
			if (set == null)
			{
				throw new ArgumentNullException("set");
			}
			frameSet = set;
			frame = set.Start;

			elapsed = TimeSpan.Zero;
		}

		public void SetFrameSetQueue(FrameSet[] indexes, bool clearQueue = true)
		{
			if (indexes.Length == 0)
			{
				throw new ArgumentOutOfRangeException("indexes");
			}
			if (clearQueue)
			{
				frameSetQueue.Clear();
			}
			foreach (FrameSet set in indexes)
			{
				frameSetQueue.Enqueue(set);
			}
			FrameSet next = frameSetQueue.Dequeue();
			SetFrameSet(next);
		}
	}
}