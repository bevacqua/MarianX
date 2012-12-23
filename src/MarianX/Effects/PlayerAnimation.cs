using MarianX.Contents;
using MarianX.Enum;
using MarianX.Mobiles;
using MarianX.Physics;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Effects
{
	public class PlayerAnimation : Animation
	{
		private readonly FrameSet idleRight = new FrameSet { Row = 0, Frames = 1 };
		private readonly FrameSet idleLeft = new FrameSet { Row = 0, Frames = 1, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet walkRight = new FrameSet { Row = 1, Frames = 3 };
		private readonly FrameSet walkLeft = new FrameSet { Row = 1, Frames = 3, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet jumpRight = new FrameSet { Row = 2, Frames = 4, Loop = false };
		private readonly FrameSet jumpLeft = new FrameSet { Row = 2, Frames = 4, Loop = false, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet steerRight = new FrameSet { Row = 2, Frames = 1, Start = 3, Loop = false };
		private readonly FrameSet steerLeft = new FrameSet { Row = 2, Frames = 1, Start = 3, Loop = false, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet deathRight = new FrameSet { Row = 3, Frames = 16, Loop = false };
		private readonly FrameSet deathLeft = new FrameSet { Row = 3, Frames = 16, Loop = false, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet cheerRight = new FrameSet { Row = 4, Frames = 14, Loop = false };
		private readonly FrameSet cheerLeft = new FrameSet { Row = 4, Frames = 14, Loop = false, Effects = SpriteEffects.FlipHorizontally };

		public PlayerAnimation(Mobile mobile)
			: base(mobile)
		{
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

		public void Die()
		{
			mobile.SetFrameSet(lastFacedLeft ? deathLeft : deathRight);
		}

		public void Cheer()
		{
			mobile.SetFrameSet(lastFacedLeft ? cheerLeft : cheerRight, true);
		}
	}
}