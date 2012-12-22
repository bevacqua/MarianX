using MarianX.Contents;
using MarianX.Core;
using MarianX.Effects;
using MarianX.Enum;
using MarianX.Physics;
using MarianX.Sprites;
using MarianX.World.Configuration;
using MarianX.World.Physics;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Mobiles
{
	public class Gloop : Npc
	{
		private const string AssetName = "Npc/gloop";

		private static readonly SpriteSheetSettings settings;

		static Gloop()
		{
			settings = new SpriteSheetSettings
			{
				Width = MagicNumbers.GloopFrameWidth,
				Height = MagicNumbers.GloopFrameHeight
			};
		}

		private readonly GloopAnimation animation;

		public override Direction Direction
		{
			get
			{
				return base.Direction;
			}
			set
			{
				if (Direction != Direction.None && Direction == value)
				{
					return;
				}
				base.Direction = value;

				animation.UpdateFace();
				animation.Update();
			}
		}

		private readonly Vector2 startPosition;

		public Gloop(Vector2 startPosition)
			: base(AssetName, settings)
		{
			animation = new GloopAnimation(this);

			BoundingBox = new GloopBoundingBox();
			Move += GameCore.Instance.ViewportManager.NpcMove;

			this.startPosition = startPosition;
		}

		public override void Initialize()
		{
			base.Initialize();

			IdleEffects();
			Speed = Vector2.Zero;
			Direction = Direction.Right;
			Position = startPosition;
		}

		protected void IdleEffects()
		{
			animation.Update(); // invalidate.
		}

		protected override Vector2 CalculateInterpolation(GameTime gameTime)
		{
			if (State == HitBoxState.Airborne)
			{
				return base.CalculateInterpolation(gameTime);
			}
			return Think(gameTime);
		}

		private Vector2 Think(GameTime gameTime, int thoughts = 3)
		{
			FloatRectangle aabb = BoundingBox.Bounds;
			Vector2 interpolation = Movement.InterpolationCalculator.CalculateInterpolation(gameTime);

			if (interpolation == Vector2.Zero)
			{
				return interpolation;
			}

			Vector2 movement = AddMargin(interpolation) + MagicNumbers.GloopVertigoMargin;
			MoveResult predicted = Movement.CollisionDetection.CanMoveInterpolated(aabb, movement, DetectionType.Collision);

			if (!predicted.HasFlag(MoveResult.BlockedOnNegativeX) &&
				!predicted.HasFlag(MoveResult.BlockedOnPositiveX) &&
				 predicted.HasFlag(MoveResult.BlockedOnPositiveY))
			{
				return interpolation;
			}
			else if (thoughts > 0)
			{
				ToggleDirection();

				return Think(gameTime, --thoughts);
			}
			else
			{
				Direction = Direction.None;
				return Vector2.Zero;
			}
		}

		private Vector2 AddMargin(Vector2 interpolation)
		{
			if (Direction == Direction.Right)
			{
				return interpolation + MagicNumbers.GloopInterpolationMargin;
			}
			else
			{
				return interpolation - MagicNumbers.GloopInterpolationMargin;
			}
		}

		private void ToggleDirection()
		{
			if (Direction == Direction.Right)
			{
				Direction = Direction.Left;
			}
			else
			{
				Direction = Direction.Right;
			}
		}
	}
}