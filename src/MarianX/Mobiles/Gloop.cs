using MarianX.Contents;
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

		public Gloop()
			: base(AssetName, settings)
		{
			BoundingBox = new GloopBoundingBox();
			animation = new GloopAnimation(this);
		}

		public override void Initialize()
		{
			base.Initialize();

			IdleEffects();
			Speed = Vector2.Zero;
			Direction = Direction.Right;
			Position = TileMatrix.Instance.StartPosition;
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

		private Vector2 Think(GameTime gameTime)
		{
			FloatRectangle aabb = BoundingBox.Bounds;
			Vector2 interpolation = Movement.InterpolationCalculator.CalculateInterpolation(gameTime);

			if (interpolation == Vector2.Zero)
			{
				return interpolation;
			}

			Vector2 interpolationPlusMargin = AddMargin(interpolation) + MagicNumbers.GloopVertigoMargin;
			MoveResult predicted = Movement.CollisionDetection.CanMoveInterpolated(aabb, interpolationPlusMargin, DetectionType.Collision);

			if (!predicted.HasFlag(MoveResult.BlockedOnNegativeX) &&
				!predicted.HasFlag(MoveResult.BlockedOnPositiveX) &&
				 predicted.HasFlag(MoveResult.BlockedOnPositiveY))
			{
				return interpolation;
			}
			else
			{
				ToggleDirection();

				return Think(gameTime);
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