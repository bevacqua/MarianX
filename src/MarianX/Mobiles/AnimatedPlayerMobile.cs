using MarianX.Contents;
using MarianX.Sprites;
using MarianX.World.Configuration;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Mobiles
{
	public class AnimatedPlayerMobile : Mobile
	{
		private static readonly SpriteSheetSettings settings;

		static AnimatedPlayerMobile()
		{
			settings = new SpriteSheetSettings
			{
				Width = MagicNumbers.FrameWidth,
				Height = MagicNumbers.FrameHeight
			};
		}

		private readonly FrameSet idleRight = new FrameSet { Row = 0, Frames = 1 };
		private readonly FrameSet idleLeft = new FrameSet { Row = 0, Frames = 1, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet walkRight = new FrameSet { Row = 1, Frames = 3 };
		private readonly FrameSet walkLeft = new FrameSet { Row = 1, Frames = 3, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet jumpRight = new FrameSet { Row = 2, Frames = 4, Loop = false };
		private readonly FrameSet jumpLeft = new FrameSet { Row = 2, Frames = 4, Loop = false, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet steerRight = new FrameSet { Row = 2, Frames = 1, Start = 3, Loop = false };
		private readonly FrameSet steerLeft = new FrameSet { Row = 2, Frames = 1, Start = 3, Loop = false, Effects = SpriteEffects.FlipHorizontally };

		private bool lastFacedLeft;

		public override Direction Direction
		{
			get
			{
				return base.Direction;
			}
			set
			{
				if (Direction == value)
				{
					return;
				}
				base.Direction = value;

				UpdateFace();
				UpdateAnimation(value);
			}
		}

		public AnimatedPlayerMobile(string assetName)
			: base(assetName, settings)
		{
		}

		public override void Load(ContentManager content)
		{
			base.Load(content);
			SetFrameSet(idleRight);
		}

		private void UpdateFace()
		{
			if (Direction == Direction.Left)
			{
				lastFacedLeft = true;
			}
			else if (Direction == Direction.Right)
			{
				lastFacedLeft = false;
			}
		}

		private void UpdateAnimation(Direction value)
		{
			if (State == HitBoxState.Surfaced)
			{
				if (value == Direction.None)
				{
					SetFrameSet(lastFacedLeft ? idleLeft : idleRight);
				}
				else
				{
					SetFrameSet(lastFacedLeft ? walkLeft : walkRight);
				}
			}
			else if (State == HitBoxState.Airborne)
			{
				SetFrameSet(lastFacedLeft ? steerLeft : steerRight);
			}
		}

		protected void JumpAnimation()
		{
			SetFrameSet(lastFacedLeft ? jumpLeft : jumpRight);
		}
	}
}