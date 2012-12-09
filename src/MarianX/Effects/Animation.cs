using MarianX.Contents;
using MarianX.Mobiles;
using MarianX.Physics;
using MarianX.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Effects
{
	public class Animation
	{
		private readonly FrameSet idleRight = new FrameSet { Row = 0, Frames = 1 };
		private readonly FrameSet idleLeft = new FrameSet { Row = 0, Frames = 1, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet walkRight = new FrameSet { Row = 1, Frames = 3 };
		private readonly FrameSet walkLeft = new FrameSet { Row = 1, Frames = 3, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet jumpRight = new FrameSet { Row = 2, Frames = 4, Loop = false };
		private readonly FrameSet jumpLeft = new FrameSet { Row = 2, Frames = 4, Loop = false, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet steerRight = new FrameSet { Row = 2, Frames = 1, Start = 3, Loop = false };
		private readonly FrameSet steerLeft = new FrameSet { Row = 2, Frames = 1, Start = 3, Loop = false, Effects = SpriteEffects.FlipHorizontally };

		private bool lastFacedLeft;
		private readonly Mobile mobile;

		public Animation(Mobile mobile)
		{
			this.mobile = mobile;
		}

		public void Load()
		{
			mobile.SetFrameSet(idleRight);
		}

		public void UpdateFace()
		{
			if (mobile.Direction == Direction.Left)
			{
				lastFacedLeft = true;
			}
			else if (mobile.Direction == Direction.Right)
			{
				lastFacedLeft = false;
			}
		}

		public void Update()
		{
			if (mobile.State == HitBoxState.Surfaced)
			{
				if (mobile.Direction == Direction.None)
				{
					mobile.SetFrameSet(lastFacedLeft ? idleLeft : idleRight);
				}
				else
				{
					mobile.SetFrameSet(lastFacedLeft ? walkLeft : walkRight);
				}
			}
			else if (mobile.State == HitBoxState.Airborne)
			{
				mobile.SetFrameSet(lastFacedLeft ? steerLeft : steerRight);
			}
		}

		public void Jump()
		{
			mobile.SetFrameSet(lastFacedLeft ? jumpLeft : jumpRight);
		}
	}
}