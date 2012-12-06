using System;
using MarianX.Contents;
using MarianX.Core;
using MarianX.Enum;
using MarianX.Physics;
using MarianX.Sprites;
using MarianX.World.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Mobiles
{
	public class Marian : PlayerMobile
	{
		private const string AssetName = "marian";

		private TimeSpan? lastJumpStarted;
		private Vector2? jumpStartPosition;

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
			}
		}

		public Marian()
			: base(AssetName)
		{
			BoundingBox = new MarianBoundingBox();
		}

		public override void Initialize()
		{
			base.Initialize();
			Position = new Vector2(MagicNumbers.StartX, MagicNumbers.StartY);
		}

		public override void Update(GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();
			UpdateMovement(keyboardState, gameTime);

			base.Update(gameTime);
		}

		protected override MoveResult UpdatePosition(Vector2 interpolation)
		{
			var wasAirborne = State == HitBoxState.Airborne;

			MoveResult result = base.UpdatePosition(interpolation);

			if (wasAirborne)
			{
				if (jumpStartPosition.HasValue && jumpStartPosition.Value.Y < Position.Y)
				{
					jumpStartPosition = null; // avoid repetition.
					FallEffects();
				}
				if (State != HitBoxState.Airborne)
				{
					Direction = Direction.None;
				}
			}
			return result;
		}

		private void UpdateMovement(KeyboardState keyboardState, GameTime gameTime)
		{
			var kb = new KeyboardConfiguration(keyboardState);

			if (State == HitBoxState.Surfaced)
			{
				UpdateMovementSurfaced(kb, gameTime);
			}
			else if (State == HitBoxState.Airborne)
			{
				UpdateMovementAirborne(kb, gameTime);
			}
		}

		private void UpdateMovementSurfaced(KeyboardConfiguration kb, GameTime gameTime)
		{
			if (kb.IsKeyDown(ActionKey.Jump))
			{
				JumpEffects();
				Speed.Y = MagicNumbers.JumpSpeed;
				lastJumpStarted = gameTime.TotalGameTime;
				jumpStartPosition = Position;
			}
			else if (kb.IsKeyDown(ActionKey.Right))
			{
				Direction = Direction.Right;
			}
			else if (kb.IsKeyDown(ActionKey.Left))
			{
				Direction = Direction.Left;
			}
			else
			{
				Direction = Direction.None;
			}
		}

		private void UpdateMovementAirborne(KeyboardConfiguration kb, GameTime gameTime)
		{
			if (kb.IsKeyDown(ActionKey.Jump))
			{
				if (!lastJumpStarted.HasValue) // sanity.
				{
					return;
				}
				if (gameTime.TotalGameTime - lastJumpStarted.Value < MagicNumbers.JumpWindow)
				{
					Speed.Y = MagicNumbers.JumpSpeed / 2;
				}
			}

			if (kb.IsKeyDown(ActionKey.Right))
			{
				Direction = Direction.Right;
			}
			else if (kb.IsKeyDown(ActionKey.Left))
			{
				Direction = Direction.Left;
			}
			else
			{
				Direction = Direction.None;
			}
		}
	}
}