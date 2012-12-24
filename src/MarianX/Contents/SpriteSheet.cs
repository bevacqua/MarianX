using System;
using System.Collections.Generic;
using MarianX.Core;
using MarianX.Events;
using MarianX.World.Configuration;
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
		private bool signaledAnimationCompleted;

		public override int ContentWidth
		{
			get { return settings.Width; }
		}

		public override int ContentHeight
		{
			get { return settings.Height; }
		}

		public override float Tilt
		{
			get
			{
				return frameSet.Tilt;
			}
			set
			{
				frameSet.Tilt = value;
			}
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

		public override void UpdateOutput(GameTime gameTime)
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

					if (!signaledAnimationCompleted)
					{
						signaledAnimationCompleted = true;

						InvokeAnimationComplete(new AnimationCompleteArgs
						{
							FrameSet = frameSet,
							GameTime = gameTime
						});
					}
				}
			}

			base.UpdateOutput(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			int offset = settings.Guides ? 1 : 0;
			int x = frame * (settings.Width + offset);
			int y = frameSet.Row * (settings.Height + offset);

			Rectangle sprite = new Rectangle(x, y, settings.Width, settings.Height);
			spriteBatch.Draw(Texture, ScreenPosition, sprite, Tint, Tilt, Vector2.Zero, Scale, frameSet.Effects, 0.0f);
		}

		public void SetFrameSet(FrameSet set, bool allowRedundancy = false)
		{
			if (set == null)
			{
				throw new ArgumentNullException("set");
			}

			if (!allowRedundancy && frameSet == set) // avoid redundancy.
			{
				return;
			}

			frameSet = set;
			frame = set.Start;

			elapsed = TimeSpan.Zero;
			signaledAnimationCompleted = false;
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

		private void InvokeAnimationComplete(AnimationCompleteArgs args)
		{
			if (AnimationComplete != null)
			{
				AnimationComplete(this, args);
			}
		}

		protected event AnimationComplete AnimationComplete;
	}
}